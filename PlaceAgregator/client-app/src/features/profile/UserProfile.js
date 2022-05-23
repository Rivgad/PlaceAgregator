import { useParams } from "react-router-dom";

const UserProfile = () => {
    let { id: userId } = useParams();

    return (
        <>
            Страница профиля пользователя с Id = {userId}
        </>
    )
}

export default UserProfile;