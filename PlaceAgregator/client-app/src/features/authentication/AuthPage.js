import { useSelector } from "react-redux"
import { Navigate, useLocation } from "react-router-dom";
import AuthForm from "./AuthForm"
import { selectIsLoggedIn } from "./authSlice"

const AuthPage = () => {
    const isLoggedIn = useSelector(selectIsLoggedIn);
    let location = useLocation();
    let from = location.state?.from?.pathname || "/";

    if (isLoggedIn)
        return <Navigate to={from} replace={true} />
    return (
        <AuthForm />
    )
}

export default AuthPage;