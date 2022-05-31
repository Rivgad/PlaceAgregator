import { BrowserRouter, Outlet, Route, Routes } from "react-router-dom";

import AppHeader from "./features/header/AppHeader";
import PlaceEditPage from "./features/places/edit/Page";
import PlacePage from "./features/places/placePage/PlacePage";
import PlacesPage from "./features/places/PlacesPage";
import MyProfilePage from "./features/profile/MyProfilePage";
import UserProfile from "./features/profile/UserProfile";
import RequireAuth from './features/authentication/RequireAuth';
import AuthPage from "./features/authentication/AuthPage";
import MyPlacesPage from "./features/places/myPlaces/MyPlacesPage";
import BookingsHistoryPage from "./features/bookings/BookingsHistoryPage";
import BookingsPage from "./features/bookings/BookingsPage";

const App = () => {
    return (
        <>
            <BrowserRouter>
                <Routes>
                    <Route path='/'
                        element={
                            <>
                                <AppHeader />
                                <Outlet />
                            </>
                        }
                    >
                        <Route index element={<PlacesPage />} />
                        <Route path="places" element={<PlacesPage />} />
                        <Route path="places/:id" element={<PlacePage />} />
                        <Route path="myPlaces"
                            element={
                                <RequireAuth>
                                    <MyPlacesPage />
                                </RequireAuth>
                            }
                        />
                        <Route path="places/:id/edit"
                            element={
                                <RequireAuth>
                                    <PlaceEditPage />
                                </RequireAuth>
                            }
                        />
                        <Route path="bookings" element={
                            <RequireAuth>
                                <BookingsPage />
                            </RequireAuth>
                        } />
                        <Route path="bookingsHistory" element={
                            <RequireAuth>
                                <BookingsHistoryPage />
                            </RequireAuth>
                        } />
                        <Route path="profile" element={
                            <RequireAuth>
                                <MyProfilePage />
                            </RequireAuth>
                        } />
                        <Route path="profile/:id" element={<UserProfile />} />
                        <Route path="login" element={<AuthPage />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default App;