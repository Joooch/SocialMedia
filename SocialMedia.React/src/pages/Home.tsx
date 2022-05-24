import { PostCard } from "entities/post";
import { PostCreatorFake } from "features/post-creator";
import { useCallback, useEffect, useRef, useState } from "react";
import { Post } from "shared/api";
import { pagedRequestDto } from "shared/api/post";
import { getFeed } from "shared/api/post";

function HomePage() {
    const [posts, setPosts] = useState<Post[]>([]);
    const [loading, setLoading] = useState<boolean>();
    const [pageData, setPageData] = useState<pagedRequestDto>({ pageSize: 3 });
    const [hasMore, setHasMore] = useState<boolean>(true);


    const loadingTriggerRef = useRef<HTMLDivElement>(null);
    const observer = useRef<IntersectionObserver>();

    const handlePaginationResponse = useCallback((response: Post[]) => {

    }, []);

    useEffect(() => {
        if (loading || !hasMore) {
            return;
        }

        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && !loading && hasMore) {
                setLoading(true);

                pageData.page = pageData.page === undefined ? 0 : pageData.page + 1;
                setPageData(pageData)

                getFeed(pageData).then(response => {
                    if (response.items.length === 0) {
                        setHasMore(false)
                        return
                    }

                    setPageData({ ...pageData, offset: response.offset });
                    setPosts([...posts, ...response.items]);
                    setLoading(false)
                })
            }
        })
        if (loadingTriggerRef.current) {
            observer.current.observe(loadingTriggerRef.current);
        }

        return () => observer.current?.disconnect();
    }, [loading, hasMore, handlePaginationResponse, loadingTriggerRef, pageData, posts])


    const onPostCreated = (post: Post) => {
        setPosts([post, ...posts])
    };
    /* useEffect(() => {
        getFeed({ pageSize: 10 }).then((posts) => {
            setPosts(posts.items);
        });
    }, []) */



    return (
        <div className="Home">
            <h1 className='center'>Title of Home page</h1>

            <div className="feed">

                <PostCreatorFake onPosted={onPostCreated} />

                {
                    posts.map((post) => {
                        return (
                            <PostCard post={post} key={post.id} />
                        )
                    })
                }
                <div ref={loadingTriggerRef}></div>
                {
                    hasMore ?
                        <div className="center-text"><p>Loading...</p></div>
                        :
                        <div className="center-text"><p>No more posts</p></div>
                }
            </div>
        </div>
    );
}

export default HomePage;