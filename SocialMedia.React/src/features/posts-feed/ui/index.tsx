import { CircularProgress } from "@mui/material";
import { PostCard } from "entities/post";
import { PostCreatorFake } from "features/post-creator";
import { useCallback, useEffect, useRef, useState } from "react";
import { getFeed } from "shared/api/post";
import { Filter, PaginatedRequest, PaginatedResult, Post } from "shared/models";
import './index.css'


export default function PostsFeed({ defaultFilter }: { defaultFilter?: Filter }) {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>();
    const [pageData, setPageData] = useState<PaginatedRequest>({ pageSize: 3 });
    const [hasMore, setHasMore] = useState<boolean>(true);


    const nextPageTriggerRef = useRef<HTMLDivElement>(null);
    const observer = useRef<IntersectionObserver>();


    const handlePaginationResponse = useCallback((response: PaginatedResult<Post>) => {
        if (response.items.length === 0) {
            setHasMore(false)
            return
        }

        setPageData({ ...pageData, offset: response.offset });
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
                setPageData(pageData)

                getFeed(pageData).then(handlePaginationResponse)
            }
        })

        if (nextPageTriggerRef.current) {
            observer.current.observe(nextPageTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, nextPageTriggerRef, pageData, posts])


    const onPostCreated = (post: Post) => {
        setPosts([post, ...posts])
    };

    return (
        <div className="feed">
            <PostCreatorFake onPosted={onPostCreated} />

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