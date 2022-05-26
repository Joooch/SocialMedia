import { Box, Paper } from '@mui/material';
import { UserAvatar } from 'entities/user';
import { Comment } from 'shared/models';
import './index.css';

export default function CommentCard({ comment }: { comment: Comment }) {
    return (
        <Box className="comment-card" color={"primary.main"} sx={{ p: 1/* , border: '1px dashed' */ }}>
            <div className="comment-avatar">
                <UserAvatar user={comment.owner} size={32} />
            </div>


            <div>
                <div className='comment-card-header'><b>{comment.owner.firstName} {comment.owner.lastName}</b></div>
                <div className="comment-card-content">
                    {comment.content}
                </div>
            </div>
        </Box>
    )
}