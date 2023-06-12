import FavoriteIcon from '@mui/icons-material/Favorite';
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import ShareIcon from '@mui/icons-material/Share';
import { AvatarGroup, Card, CardActions, CardContent, CardHeader, Chip, Divider, IconButton, ImageList, ImageListItem } from '@mui/material';
import { UserAvatar } from 'entities/user';
import CommentsFeed from 'features/comments-feed/ui';
import { useEffect, useState } from 'react';
import { PostLikes, addLike, deleteLike, getLikes } from 'shared/api/post';
import { Post } from 'shared/models';

export function PostCard({ post }: { post: Post }) {
    const [likes, setLikes] = useState<PostLikes>({
        hasLike: false,
        otherLikes: [],
        totalCount: 0
    });

    const toggleLike = async () => {
        if(likes.hasLike){
            await deleteLike(post.id);
            getLikes(post.id).then(setLikes);
            return;
        }

        await addLike(post.id);
        getLikes(post.id).then(setLikes);
    }
    
    useEffect(() => {
        getLikes(post.id).then(setLikes);
    }, [])

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
                        <ImageList sx={{ textAlign: "center" }} cols={Math.min(post.images.length, 3)} gap={3} variant="standard">
                            {
                                post.images.map(id => {
                                    return (
                                        <ImageListItem key={id} sx={{
                                            maxWidth: 1000
                                        }}>
                                            <img src={"/img/posts/" + id + ".webp"} key={id} alt="" loading="lazy" />
                                        </ImageListItem>
                                    )
                                })
                            }
                        </ImageList>
                }
            </CardContent>
            <CardActions disableSpacing>
                <IconButton onClick={toggleLike} aria-label="like">
                    <Chip
						avatar={likes.hasLike ? <FavoriteIcon /> : <FavoriteBorderIcon />}
						label="Like"
					/>
                </IconButton>
                {likes && (
                    <AvatarGroup
                        max={3}
                        total={likes.totalCount}
                        spacing={"small"}
                        sx={{
                            "& .MuiAvatar-root": {
                                width: 32,
                                height: 32,
                                fontSize: 15,
                            },
                        }}
                    >
                        {
                            likes.otherLikes.map((profile) => {
                                return (
                                    <UserAvatar user={profile} size={35} key={profile.userId}/>
                                )
                            })
                        }
                    </AvatarGroup>
                )}

                <IconButton aria-label="share">
                    <Chip avatar={<ShareIcon/>} label="Share" />
                </IconButton>
            </CardActions>
            
            <CommentsFeed post={post} />

        </Card>
    );
};