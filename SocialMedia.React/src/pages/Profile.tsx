import { Container } from "@mui/material";
import { fetchProfile } from "entities/profile";
import ProfileHeader from "entities/profile/ui/profile-header";
import { useEffect, useRef, useState } from "react";
import { useParams } from "react-router-dom";
import { User } from "shared/api";

function ProfilePage() {
    const [user, setUser] = useState<User>();
    const [height, setHeight] = useState<number>(64);

    const { id } = useParams();
    if (!id) {
        throw new Error("invalid params");
    }



    console.log("fetching")
    useEffect(() => {
        fetchProfile(id).then(fetchedUser => {
            setUser(fetchedUser)
        })
    }, [id])

    /* useEffect(() => {
        const onScroll = e => {
          
        };
        window.addEventListener("scroll", onScroll);
    
        return () => window.removeEventListener("scroll", onScroll);
      }, []); */

    return (
        <Container className='profile-page' sx={{maxHeight: "20px"}}>
            <ProfileHeader user={user} />
            <div>
                Content
            </div>
        </Container>
    );
}

export default ProfilePage;