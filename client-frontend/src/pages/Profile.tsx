import { fetchProfile } from "entities/profile";
import { useParams } from "react-router-dom";
import { User } from "shared/api";

function ProfilePage() {
    const { id } = useParams();

    if(!id){
        return <></>
    }

    console.log("fetching")
    fetchProfile(id)

    return (
        <div className='profile-page'>

        </div>
    );
}

export default ProfilePage;