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

    const [page, setPage] = useState<number | undefined>();
    const defaultPageRequest = useRef<PaginatedRequest>({
        sortKey: "createdAt",
        sortDirection: "DESC",
        pageSize: 3,
        filters: []
    });

    const [hasMore, setHasMore] = useState<boolean>(true);

    const [searchText, setSearchText] = useState<string>("");
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

        defaultPageRequest.current.offset = new Date();
        setPosts([...posts, ...response.items]);
        setLoading(false)
    }, [posts]);


    useEffect(() => {
        if (loading || !hasMore) {
            return;
        }

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && !loading && hasMore) {
                setLoading(true);

                const nextPage = page === undefined ? 0 : page + 1;
                setPage(nextPage)

                const request = {
                    ...defaultPageRequest.current,
                    sortDirection: sortByDescending ? "DESC" : "ASC",
                    filters: buildFilters(),
                    page: nextPage
                }

                getFeed(request).then(handlePaginationResponse)
            }
        })

        if (nextPageTriggerRef.current) {
            observer.current.observe(nextPageTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, nextPageTriggerRef, posts, buildFilters])


    useEffect(() => {
        if (!setAppend) { return }

        setAppend((post) => {
            setPosts([post, ...posts])
        })
    }, [posts, setAppend])


    const clearPage = useCallback(() => {
        setPage(undefined)

        setLoading(false)
        setHasMore(true)
        setPosts([])
    }, [])
    useEffect(clearPage, [clearPage, defaultFilter, sortByDescending])

    const doSearch = useCallback(() => {
        clearPage( )
    }, [clearPage])

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