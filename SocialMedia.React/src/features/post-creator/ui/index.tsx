import { Box, Button, Container, Divider, ImageList, ImageListItem, Modal, Paper, TextField } from "@mui/material";
import { UserAvatar } from "entities/user";
import { useEffect, useRef, useState } from "react";
import { useAuth } from "shared/api";
import { createPost, uploadImage } from "shared/api/post";
import AddPhotoAlternateIcon from '@mui/icons-material/AddPhotoAlternate';
import { Post, User } from "shared/models";
import './style.css';

export function PostCreatorPopupMenu({ user, onPosted }: { user: User, onPosted: (post: Post) => void }) {
    const inputRef = useRef<HTMLInputElement>();
    const fileInputRef = useRef<HTMLInputElement>(null);
    const [imageList, setImageList] = useState<string[]>([]);
    const [uploading, setUploading] = useState<boolean>(false);
    const [content, setContent] = useState<string>();

    useEffect(() => {
        inputRef.current?.focus();
    }, [])

    const doUploadFile = () => {
        fileInputRef?.current?.click();
    }
    const onSelectImage = async (event: React.ChangeEvent<HTMLInputElement>) => {
        const files = event.target.files;
        if (!files || files.length === 0) {
            return;
        }

        setUploading(true);

        try {
            const tempList = imageList;
            const filesArray = Array.from(files);

            for (let i = 0; i < filesArray.length; i++) {
                if (tempList.length >= 3) {
                    break;
                }
                const image = filesArray[i]
                const dto = await uploadImage(image);

                tempList.push(dto.imageId);
                setImageList(tempList);
            }
        }
        catch { }

        setUploading(false);
    }

    const makePost = async () => {
        onPosted(await createPost(content ?? "", imageList));
    }


    return (
        <div>
            <div className="header">
                <h2>Create Post</h2>
            </div>

            <Divider sx={{ marginBottom: 3 }} />

            <div className="content">
                <TextField
                    placeholder="Write something..."
                    multiline
                    rows={6}
                    fullWidth
                    inputRef={inputRef}
                    onChange={(e) => setContent(e.target.value)}
                />

                <ImageList sx={{ maxHeight: 450 }} cols={3} variant="masonry">
                    {
                        imageList.map(id => {
                            return (
                                <ImageListItem key={id}>
                                    <img src={"/img/posts/" + id + ".webp"} key={id} alt="" loading="lazy" />
                                </ImageListItem>
                            )
                        })
                    }
                </ImageList>


                <Paper className="center-text">
                    <input
                        type="file"
                        multiple
                        accept="image/png, image/gif, image/jpeg"
                        ref={fileInputRef}
                        onChange={onSelectImage}
                        title="imageSelector"
                        style={{ display: 'none' }}
                    />
                    <Button onClick={doUploadFile} disabled={uploading} fullWidth>
                        <AddPhotoAlternateIcon />
                        Add Image
                    </Button>
                </Paper>

                <Paper sx={{ marginTop: "10px" }}>
                    <Button onClick={makePost} fullWidth>
                        Post
                    </Button>
                </Paper>
            </div>
        </div >
    )
}

export function PostCreatorFake({ onPosted }: { onPosted: (post: Post) => void }) {
    const [open, setOpen] = useState(false);
    const { user } = useAuth();

    const handleClose = () => setOpen(false);
    const handleOpen = (event: React.MouseEvent<HTMLElement>) => {
        event.preventDefault();
        setOpen(true);
    }
    const handleOnPost = (post: Post) => {
        setOpen(false);
        onPosted(post);
    }

    return (
        <div>
            <Modal open={open} onClose={handleClose}>
                <Box className="center-of-screen post-creator-popup" sx={{
                    backgroundColor: "background.paper",
                }} width="md">
                    <PostCreatorPopupMenu user={user!} onPosted={handleOnPost} />
                </Box>
            </Modal>
            <div className="center-content">
                <div className="post-creator-fake">
                    <div>
                        <UserAvatar user={user} size={32} />
                        <TextField placeholder="Write something..." onClick={handleOpen} disabled></TextField>
                    </div>

                    <Container className="center-content" sx={{
                        "margin-top": "5px"
                    }}>
                        <Button variant="contained" onClick={handleOpen}>Add Photo</Button>
                    </Container>
                </div>
            </div>
        </div>
    )
}