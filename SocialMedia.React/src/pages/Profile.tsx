import { Container } from "@mui/material";
import { fetchProfile } from "entities/profile";
import ProfileHeader from "entities/profile/ui/profile-header";
import PostsFeed from "features/posts-feed/ui";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Filter, Post, User } from "shared/models";

function ProfilePage() {
    const { id } = useParams();
    if (!id) {
        throw new Error("invalid params");
    }

    const [user, setUser] = useState<User>();
    const [height, setHeight] = useState<number>(64);
    const [profileFilter, setProfileFilter] = useState<Filter>();


    useEffect(() => {
        fetchProfile(id).then(fetchedUser => {
            setUser(fetchedUser)
        })

        setProfileFilter(new Filter("userOwner.userId", id));
    }, [id])

    /* useEffect(() => {
        const onScroll = e => {
          
        };
        window.addEventListener("scroll", onScroll);
    
        return () => window.removeEventListener("scroll", onScroll);
      }, []); */

    return (
        <Container className='profile-page' sx={{ maxHeight: "20px" }}>
            <ProfileHeader user={user} />
            <PostsFeed defaultFilter={profileFilter} />
        </Container>
    );
}

export default ProfilePage;