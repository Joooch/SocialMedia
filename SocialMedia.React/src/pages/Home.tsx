import PostsFeed from "features/posts-feed/ui";

function HomePage() {
    return (
        <div className="Home">
            <h1 className='center-text'>Posts Feed</h1>

            <PostsFeed />
        </div>
    );
}

export default HomePage;