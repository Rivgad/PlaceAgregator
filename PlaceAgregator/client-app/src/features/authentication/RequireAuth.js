import { useSelector } from "react-redux"
import { Navigate, useLocation } from "react-router-dom"
import { selectIsLoggedIn, selectUser } from "./authSlice"

function RequireAuth({ children, to = null, roles = null }) {
  const isAuthed = useSelector(selectIsLoggedIn);
  const user = useSelector(selectUser);
  let location = useLocation();

  if (!isAuthed || (roles && user && !user.roles.some(role => roles.includes(role)))) {
    return <Navigate to={to ?? "/login"} state={{ from: location }} replace />;
  }
  return children;
}

export default RequireAuth;