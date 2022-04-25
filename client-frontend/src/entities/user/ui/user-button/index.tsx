import type { UserProps } from '../types'
import Button from '@mui/material/Button'
import { UserAvatar } from '../user-avatar';
import { Link } from 'react-router-dom';

export const UserButton: React.FC<UserProps> = ({ user }) => {
    if (user == null) {
        return <></>;
    }
    return (
        <div className="user-button">
            <Button variant="text" color="primary" component={Link} to={"/profile/" + user.userId}>
                <UserAvatar user={user} />
                {user.firstName + " " + user.lastName}
            </Button>
        </div>
    );
};