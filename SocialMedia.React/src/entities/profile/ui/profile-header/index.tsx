import { Paper } from "@mui/material";
import { UserAvatar } from "entities/user";
import { User } from "shared/models";
import './profile-header.css'

export default function ProfileHeader(props: { user?: User }) {
    return (
        <Paper className="profile-header">
            <UserAvatar user={props.user} size={64} />
        </Paper>
    )
}