import type { Post } from 'shared/api/types';


export type PostCardProps = {
    data?: Post,
};
// todo: material ui

export const PostCard = ({ data }: PostCardProps) => {
    // Можно обработать и получше при желании
    if (!data) return null;

    return (
        <div>
            Test post card div
        </div>
        /*{ <Card
            // Можно обработать и получше при желании
            title={`Task#${cardProps.loading ? "" : data?.id}`}
            className={styles.root}
            {...cardProps}
        >
            {titleHref ? <Link to={titleHref}>{data?.title}</Link> : data?.title}
            {children}
        </Card> }*/
    );
};