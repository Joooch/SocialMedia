import { TextField } from "@mui/material";
import CommentIcon from '@mui/icons-material/Comment';
import { useAuth } from "shared/api";
import { UserAvatar } from "entities/user";
import { useRef } from "react";
import { createComment } from "shared/api/comment";
import { Comment } from "shared/models";
import './index.css'

export default function CommentCreator({ postId, onCommentCreated }: { postId: string, onCommentCreated?: (comment: Comment) => void }) {
    const inputRef = useRef<HTMLInputElement>();
    const { user } = useAuth();

    const handleClick = () => {
        const element = inputRef.current!

        let content = element.value;
        element.value = '';

        createComment(content, postId).then(comment => {
            if (onCommentCreated) {
                onCommentCreated(comment);
            }
        });
    }

    return (
        <div className="comment-creator">
            <UserAvatar user={user} size={32} />

            <TextField placeholder="Write a comment..." size="small" inputRef={inputRef} onKeyDown={e => { if (e.key === "Enter") handleClick() }} />
            {/* <Button className="comment-button" color="primary" size="small"> </Button> */}
            <div onClick={handleClick}>
                <CommentIcon className="comment-icon" color="primary" />
            </div>
        </div>
    )
}