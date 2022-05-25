import './user-avatar.css';
import { useCallback, useMemo, useState } from 'react';
import { Skeleton } from '@mui/material';
import { User } from 'shared/models';
import { Link } from 'react-router-dom';

const defaultUserImg = "/img/user.png"

export const UserAvatar = (props: { user?: User | string, size?: number | string, supressLink?: boolean }) => {
    const [imageSrc, setImageSrc] = useState<string>();

    useMemo(() => {
        if (typeof props.user === "string") {
            setImageSrc(props.user || defaultUserImg)
        } else {
            setImageSrc("/img/users/" + props.user?.userId + ".webp" || defaultUserImg)
        }
    }, [props.user])

    const ImgComponent = useCallback(() => {
        return (
            <img
                className='user-avatar'
                alt={typeof props.user == "string" ? "user avatar" : props.user?.firstName ?? "user-avatar"}
                src={imageSrc}
                width={props.size}
                height={props.size}

                onError={() => {
                    if (imageSrc !== defaultUserImg) {
                        setImageSrc(defaultUserImg)
                    }
                }}
            />
        )
    }, [imageSrc, props.size, props.user])

    if (props.user === undefined) { // render skeleton instead
        return (
            <Skeleton variant="circular" width={props.size} height={props.size} />
        )
    }

    let link: string;

    const user = props.user as User;
    if (user) {
        link = "/profile/" + user.userId;
    } else {
        link = "";
    }

    if (props.supressLink) {
        return <ImgComponent />;
    } else {
        return <Link to={link}> <ImgComponent /> </Link>;
    }
};