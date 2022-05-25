import { CircularProgress } from "@mui/material";
import { PostCard } from "entities/post";
import { PostCreatorFake } from "features/post-creator";
import { useCallback, useEffect, useRef, useState } from "react";
import { getFeed } from "shared/api/post";
import { Filter, PaginatedRequest, PaginatedResult, Post } from "shared/models";
import './index.css'


type appendFunction = {
    (post: Post): void
}
export default function PostsFeed({ defaultFilter, setAppendPost: setAppend }: { defaultFilter?: Filter, setAppendPost?: (func: appendFunction) => void }) {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>();
    const [pageData, setPageData] = useState<PaginatedRequest>({
        sortKey: "createdAt",
        sortDirection: "asc",
        pageSize: 3,
        filters: []
    });
    const [hasMore, setHasMore] = useState<boolean>(true);


    const nextPageTriggerRef = useRef<HTMLDivElement>(null);
    const observer = useRef<IntersectionObserver>();

    const buildFilters = useCallback(() => {
        const filters: Filter[] = [];

        if (defaultFilter) {
            filters.push(defaultFilter);
        }

        return filters;
    }, [defaultFilter])

    const handlePaginationResponse = useCallback((response: PaginatedResult<Post>) => {
        if (response.items.length === 0) {
            setHasMore(false)
            return
        }

        setPageData({
            ...pageData,
            offset: response.offset,
            filters: buildFilters()
        });
        setPosts([...posts, ...response.items]);
        setLoading(false)
    }, [buildFilters, pageData, posts]);

    useEffect(() => {
        if (loading || !hasMore) {
            return;
        }

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && !loading && hasMore) {
                setLoading(true);

                pageData.page = pageData.page === undefined ? 0 : pageData.page + 1;
                setPageData(pageData)

                getFeed(pageData).then(handlePaginationResponse)
            }
        })

        if (nextPageTriggerRef.current) {
            observer.current.observe(nextPageTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, nextPageTriggerRef, pageData, posts])


    useEffect(() => {
        if (!setAppend) { return }

        setAppend((post) => {
            setPosts([post, ...posts])
        })
    })



    return (
        <div className="feed">
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