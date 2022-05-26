import { Box, CircularProgress, TextField } from "@mui/material";
import { PostCard } from "entities/post";
import { useCallback, useEffect, useRef, useState } from "react";
import { getFeed } from "shared/api/post";
import { Filter, PaginatedRequest, PaginatedResult, Post } from "shared/models";
import SearchIcon from '@mui/icons-material/Search';
import SortIcon from '@mui/icons-material/Sort';
import './index.css'


type appendFunction = {
    (post: Post): void
}
export default function PostsFeed({ defaultFilter, setAppendPost: setAppend }: { defaultFilter?: Filter, setAppendPost?: (func: appendFunction) => void }) {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>();
    const [pageData, setPageData] = useState<PaginatedRequest>({
        sortKey: "createdAt",
        sortDirection: "DESC",
        pageSize: 3,
        filters: []
    });
    const [hasMore, setHasMore] = useState<boolean>(true);

    const [searchText, setSearchText] = useState<string>();
    const [sortByDescending, setSortByDescending] = useState<boolean>(true);

    const nextPageTriggerRef = useRef<HTMLDivElement>(null);
    const observer = useRef<IntersectionObserver>();

    const buildFilters = useCallback(() => {
        const filters: Filter[] = [];

        if (defaultFilter) {
            filters.push(defaultFilter);
        }

        if (searchText) {
            filters.push({
                path: "content",
                value: searchText,
            })
        }

        return filters;
    }, [defaultFilter, searchText])


    const handlePaginationResponse = useCallback((response: PaginatedResult<Post>) => {
        if (response.items.length === 0) {
            setHasMore(false)
            return
        }

        setPageData({
            ...pageData,
            offset: response.offset
        });
        setPosts([...posts, ...response.items]);
        setLoading(false)
    }, [pageData, posts]);


    useEffect(() => {
        if (loading || !hasMore) {
            return;
        }

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && !loading && hasMore) {
                setLoading(true);

                pageData.page = pageData.page === undefined ? 0 : pageData.page + 1;
                pageData.filters = buildFilters();
                pageData.sortDirection = sortByDescending ? "DESC" : "ASC";
                setPageData(pageData)

                getFeed(pageData).then(handlePaginationResponse)
            }
        })

        if (nextPageTriggerRef.current) {
            observer.current.observe(nextPageTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, nextPageTriggerRef, pageData, posts, buildFilters])


    useEffect(() => {
        if (!setAppend) { return }

        setAppend((post) => {
            setPosts([post, ...posts])
        })
    }, [posts, setAppend])


    useEffect(() => {
        setPageData({
            ...pageData,
            page: undefined,
            filters: buildFilters()
        })

        setLoading(false)
        setHasMore(true)
        setPosts([])
    }, [defaultFilter])

    const doSearch = useCallback(() => {
        setLoading(false)
        setHasMore(true)
        setPosts([])
        setPageData({
            ...pageData,
            page: undefined,
            filters: buildFilters()
        })

        console.log(pageData)
    }, [pageData, buildFilters])

    useEffect(() => {
        setPageData({
            ...pageData,
            page: undefined,
            filters: buildFilters()
        })

        setLoading(false)
        setHasMore(true)
        setPosts([])
    }, [sortByDescending])


    return (
        <div className="feed">
            <Box color={"primary.main"}>
                <div className="search-bar" hidden={posts.length === 0}>
                    <div className="search-button" onClick={() => setSortByDescending(!sortByDescending)}>
                        <SortIcon className="search-descending" sx={{ transform: sortByDescending ? "" : "rotate(-180deg)" }} />
                    </div>
                    <TextField type="text" placeholder="Search" value={searchText} onChange={(e) => setSearchText(e.target.value)} onKeyDown={e => { if (e.key === "Enter") doSearch() }} size="small" ></TextField>
                    <div className="search-button" onClick={doSearch}>
                        <SearchIcon />
                    </div>
                </div>
            </Box>

            {
                posts.map((post) => {
                    return (
                        <PostCard post={post} key={post.id} />
                    )
                })
            }

            <div ref={nextPageTriggerRef}></div>

            {
                hasMore ?
                    <div className="center-text"> <p><CircularProgress className="center-content" /></p> </div>
                    :
                    <div className="center-text"> <p>No more posts</p> </div>
            }
        </div>
    );
}