import { PostCard } from "entities/post";
import { PostCreatorFake } from "features/post-creator";
import { useEffect, useState } from "react";
import { Post } from "shared/api";
import { getFeed } from "shared/api/post";

function HomePage() {
    const [posts, setPosts] = useState<Post[]>([]);

    useEffect(() => {
        getFeed({ pageSize: 10 }).then((posts) => {
            console.log(posts)
            setPosts(posts);
        });
    }, [])

    const handleOnPost = (post: Post) => {
        setPosts([post, ...posts])
    }

    return (
        <div className="Home">
            <h1 className='center'>Title of Home page</h1>

            <div className="feed">
                
                <PostCreatorFake onPosted={handleOnPost} />

                {
                    posts.map((post) => {
                        return (
                            <PostCard post={post} key={post.id} />
                        )
                    })
                }
            </div>
        </div>
    );
}

export default HomePage;