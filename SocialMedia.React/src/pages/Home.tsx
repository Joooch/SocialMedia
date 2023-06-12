import { PostCreatorFake } from "features/post-creator";
import PostsFeed from "features/posts-feed/ui";
import { useCallback, useEffect, useRef } from "react";
import { useCookies } from "react-cookie";
import { Post } from "shared/models";

type appendPostFunction = {
    (post: Post): void
}

function HomePage() {

    const [cookies, setCookie, removeCookie] = useCookies(["Token"]);
    const appendPostToFeed = useRef<appendPostFunction>();

    const onPostCreated = useCallback((post: Post) => {
        if (appendPostToFeed.current) {
            appendPostToFeed.current(post);
        }
    }, [])

    return (
        <div className="Home">
            <h1 className='center-text'>Posts Feed</h1>

            <PostCreatorFake onPosted={onPostCreated} />
            <PostsFeed setAppendPost={(func) => appendPostToFeed.current = func} />
        </div>
    );
}

export default HomePage;