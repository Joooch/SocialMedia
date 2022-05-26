import { Divider } from "@mui/material";
import { fetchProfile } from "entities/profile";
import ProfileHeader from "entities/profile/ui/profile-header";
import FriendsFeed from "features/friends-feed/ui";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { User } from "shared/models";

function FriendsPage() {
    const { id } = useParams();
    if (!id) {
        throw new Error("invalid params");
    }

    const [user, setUser] = useState<User>();


    useEffect(() => {
        fetchProfile(id).then(fetchedUser => {
            setUser(fetchedUser)
        })
    }, [id])

    return (
        <div className="Home">
            <ProfileHeader user={user} hideFriendsLabel={true} />

            <Divider />
            <h2 className='center-text'>Friends List</h2>
            <FriendsFeed userId={id} />
        </div>
    );
}

export default FriendsPage;