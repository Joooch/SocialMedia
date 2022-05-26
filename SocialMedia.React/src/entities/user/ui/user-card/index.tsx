import { Button } from '@mui/material'
import { Link } from 'react-router-dom'
import { User } from 'shared/models'
import { UserAvatar } from '../user-avatar'
import './index.css'

export default function UserCard({ user, size }: { user: User, size?: number }) {
    return (
        <Button variant="contained" className="user-card" fullWidth sx={{ maxWidth: "250px" }} >
            <UserAvatar user={user} size={size ?? 48} />
            <Link className="user-link" to={"/profile/" + user.userId} style={{ textDecoration: 'none' }}>
                <div>
                    {user.firstName} {user.lastName}
                </div>
            </Link>
        </Button>
    )
}