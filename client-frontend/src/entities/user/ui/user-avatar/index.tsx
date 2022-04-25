import './user-avatar.css';
import { useMemo, useState } from 'react';
import type { User } from 'shared/api/types';

const defaultUserImg = "/img/user.png"

export const UserAvatar = (props: { user?: User, src?: string, size?: number | string }) => {
    const [imageSrc, setImageSrc] = useState<string>();

    useMemo(() => {
        setImageSrc(props.src || "/img/users/" + props.user?.userId + ".webp" || defaultUserImg)
    }, [props.src, props.user?.userId])

    return (
        <img
            className='user-avatar'
            alt={props.user?.firstName ?? "user-avatar"}
            src={imageSrc}
            width={props.size ?? 32}
            height={props.size ?? 32}

            onError={() => {
                if (imageSrc !== defaultUserImg) {
                    setImageSrc(defaultUserImg)
                }
            }}
        />
    );
};