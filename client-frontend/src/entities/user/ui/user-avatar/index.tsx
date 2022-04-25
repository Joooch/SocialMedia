import Avatar from '@mui/material/Avatar';
import type { User } from 'shared/api/types';

export const UserAvatar = (props: {user?: User, src?: string, size?: number | string}) => {
    return (
        <Avatar className='user-avatar' alt={props.user?.firstName ?? "user-avatar"} src={props.src ?? "/img/user.png"} sx={{ width: props.size ?? 64, height: props.size ?? 64 }} />
    );
};