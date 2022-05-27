import { Box } from "@mui/material";
import PeopleIcon from '@mui/icons-material/People';
import { UserAvatar } from "entities/user";
import { User } from "shared/models";
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import { Link } from "react-router-dom";
import { getFriendsCount, getFriendStatus, sendFriendRequest } from "shared/api/friends/lib";
import { useEffect, useState } from "react";
import { FriendStatus } from "shared/models/friendStatus";
import './index.css'

export default function ProfileHeader({ user, hideFriendsLabel }: { user?: User, hideFriendsLabel?: boolean }) {

    const [countFriends, setCountFriends] = useState<number>(0);
    const [friendStatus, setfriendStatus] = useState<FriendStatus>();

    useEffect(() => {
        if (user) {
            getFriendsCount(user.userId).then(setCountFriends)
            getFriendStatus(user.userId).then(setfriendStatus)
        }
    }, [user])

    console.log("friend status:", friendStatus)

    const addFriend = async () => {
        await sendFriendRequest(user!.userId);
    }

    return (
        <div className="profile-header">
            <div>
                <div className="profile-avatar-center">
                    <UserAvatar user={user} size={128} />
                </div>
                <h1>{user?.firstName} {user?.lastName}</h1>
            </div>

            <Box className="profile-friends" sx={{ visibility: hideFriendsLabel ? 'hidden' : '' }}>
                <Link className="profile-friends-label" to={"friends"}>{countFriends} friends</Link>

                <Box color="primary.main">
                    {
                        friendStatus === FriendStatus.NotFriend
                            ?
                            (<div className="profile-friends-button" onClick={addFriend}>
                                <PersonAddIcon />
                            </div>)
                            :

                            friendStatus === FriendStatus.Pending
                                ?
                                (<>{/* pending */}</>)
                                :
                                (<div className="profile-friends-button">
                                    <PeopleIcon />
                                </div>)

                    }
                </Box>
            </Box>

        </div>
    )
}