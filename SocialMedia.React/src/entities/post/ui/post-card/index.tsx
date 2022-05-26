import { Card, CardContent, CardHeader, Divider, ImageList, ImageListItem } from '@mui/material';
import { UserAvatar } from 'entities/user';
import CommentsFeed from 'features/comments-feed/ui';
import { Post } from 'shared/models';

export function PostCard({ post }: { post: Post }) {
    return (
        <Card className='postCard'>
            <CardHeader
                avatar={
                    <UserAvatar user={post.owner} size={32} />
                }
                title={`${post.owner.firstName} ${post.owner.lastName}`}
                subheader={new Date(post.createdAt).toLocaleString()}
            />

            <Divider />

            <CardContent>
                {post.content}
                {
                    post.images.length === 0 ?
                        <></>
                        :
                        <ImageList sx={{ maxHeight: 450 }} cols={3} gap={2} variant="standard">
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
            </CardContent>

            <CommentsFeed post={post} />

        </Card>
    );
};