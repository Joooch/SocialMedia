import { Container } from "@mui/material";
import { fetchProfile } from "entities/profile";
import ProfileHeader from "entities/profile/ui/profile-header";
import PostsFeed from "features/posts-feed/ui";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Filter, User } from "shared/models";

function ProfilePage() {
    const { id } = useParams();
    if (!id) {
        throw new Error("invalid params");
    }

    const [user, setUser] = useState<User>();
    const [profileFilter, setProfileFilter] = useState<Filter>();


    useEffect(() => {
        fetchProfile(id).then(fetchedUser => {
            setUser(fetchedUser)
        })

        setProfileFilter(new Filter("owner.userId", id));
    }, [id])

    return (
        <Container className='profile-page' sx={{ maxHeight: "20px" }}>
            <ProfileHeader user={user} />
            <PostsFeed defaultFilter={profileFilter} />
        </Container>
    );
}

export default ProfilePage;