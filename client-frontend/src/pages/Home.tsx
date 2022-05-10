function HomePage() {
    return (
        <div className="Home">
            <h1 className='center'>Title of Home page</h1>

            <p>foreach example:</p>
            {
                [...Array(200)].map((_, i) => {
                    return (<div key={i}> Hello world {i} </div>)
                })
            }
        </div>
    );
}

export default HomePage;