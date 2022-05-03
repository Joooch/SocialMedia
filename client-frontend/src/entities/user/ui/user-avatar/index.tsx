import './user-avatar.css';
import { useMemo, useState } from 'react';
import type { User } from 'shared/api/types';
import { Skeleton } from '@mui/material';

const defaultUserImg = "/img/user.png"

export const UserAvatar = (props: { user?: User, size?: number | string }) => {
    const [imageSrc, setImageSrc] = useState<string>();

    

    useMemo(() => {
        setImageSrc("/img/users/" + props.user?.userId + ".webp" || defaultUserImg)
    }, [props.user?.userId])

    if(props.user === undefined){ // render skeleton instead
        return (
            <Skeleton variant="circular" width={props.size} height={props.size} />
        )
    }

    return (
        <img
            className='user-avatar'
            alt={props.user?.firstName ?? "user-avatar"}
            src={imageSrc}
            width={props.size}
            height={props.size}

            onError={() => {
                if (imageSrc !== defaultUserImg) {
                    setImageSrc(defaultUserImg)
                }
            }}
        />
    );
};