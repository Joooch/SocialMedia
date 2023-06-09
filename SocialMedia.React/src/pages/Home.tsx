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

    useEffect(() => {
        console.log("test")
        // var socket = new WebSocket("ws://javascript.ru/ws");
        var socket = new WebSocket("ws://localhost:5042/WebSocket", cookies.Token);
        socket.onopen = function(e){
            console.log("on open")
            this.send(JSON.stringify({"type": "test"}))
        }
        socket.onmessage = function(e){
            console.log("message??")
            console.log(e.data)
        }
        socket.onclose = function(e){
            console.log("on close")
        }
        return () => {
            socket.close();
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