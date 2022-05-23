import { BrowserRouter, Outlet, Route, Routes } from "react-router-dom";

import BookingsPage from "./features/bookings/BookingsPage";
import AppFooter from "./features/footer/AppFooter";
import AppHeader from "./features/header/AppHeader";
import PlaceEditPage from "./features/places/edit/Page";
import MyPlacesPage from "./features/places/MyPlacesPage";
import PlacePage from "./features/places/placePage/PlacePage";
import PlacesPage from "./features/places/PlacesPage";
import MyProfilePage from "./features/profile/MyProfilePage";
import UserProfile from "./features/profile/UserProfile";

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
                                <AppFooter />
                            </>
                        }
                    >
                        <Route path="places" element={<PlacesPage />} />
                        <Route path="places/:id" element={<PlacePage />} />
                        <Route path="myPlaces" element={<MyPlacesPage />} />
                        <Route path="places/:id/edit" element={<PlaceEditPage />} />
                        <Route path="bookings" element={<BookingsPage />} />
                        <Route path="profile" element={<MyProfilePage />} />
                        <Route path="profile/:id" element={<UserProfile />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default App;