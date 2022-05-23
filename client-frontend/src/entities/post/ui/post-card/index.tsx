import { Card, CardContent, CardHeader, CardMedia, Divider, ImageList, ImageListItem } from '@mui/material';
import { UserAvatar } from 'entities/user';
import { Link } from 'react-router-dom';
import type { Post } from 'shared/api/types';

export function PostCard({ post }: { post: Post }) {
    return (
        <Card className='postCard'>
            <CardHeader
                avatar={
                    <UserAvatar user={post.userOwner} size={32} />
                }
                title={`${post.userOwner.firstName} ${post.userOwner.lastName}`}
                subheader={new Date(post.createdAt).toLocaleString()}
            />

            <Divider />

                <CardContent>
                    {post.content}
                </CardContent>
            {
                <ImageList sx={{ maxHeight: 450 }} cols={3} variant="masonry">
                    {
                        post.images.map(id => {
                            return (
                                <ImageListItem key={id}>
                                    <img src={"/img/posts/" + id + ".webp"} key={id} alt="" loading="lazy" />
                                </ImageListItem>
                            )
                        })
                    }
                </ImageList>
            }
            

            {/* {titleHref ? <Link to={titleHref}>{data?.title}</Link> : data?.title} */}

        </Card>
    );
};