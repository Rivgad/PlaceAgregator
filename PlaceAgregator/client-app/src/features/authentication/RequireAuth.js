import { useSelector } from "react-redux"
import { Navigate, useLocation } from "react-router-dom"
import { selectIsLoggedIn } from "./authSlice"

function RequireAuth({ children }) {
    const isAuthed = useSelector(selectIsLoggedIn);
    let location = useLocation();
  
    if (!isAuthed) {
      return <Navigate to="/login" state={{ from: location }} replace />;
    }
    return children;
  }

export default RequireAuth;