import { useSelector } from "react-redux"
import { Navigate } from "react-router-dom";
import AuthForm from "./AuthForm"
import { selectIsLoggedIn } from "./authSlice"

const AuthPage = () => {
    const isLoggedIn = useSelector(selectIsLoggedIn);
    if (isLoggedIn)
        return <Navigate to='/' />
    return (
        <AuthForm />
    )
}

export default AuthPage;