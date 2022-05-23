import { BrowserRouter, Route, Routes } from "react-router-dom";
import App from "./Components/App/App";
import BookingsPage from "./Components/Pages/bookings-gape/bookings-page";
import MyPlacesPage from "./Components/Pages/my-places-page";
import MyProfilePage from "./Components/Pages/my-profile-page/my-profile-page";
import PlaceEditPage from "./Components/Pages/place-edit-page/place-edit-page";
import PlacePage from "./Components/Pages/place-page";
import PlacesPage from "./Components/Places/Places";

const Appp =()=>{
    return (
        <>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<App/>}>
                        <Route path="places" element={<PlacesPage />} />
                        <Route path="places/:id" element={<PlacePage />} />
                        <Route path="myPlaces" element={<MyPlacesPage />} />
                        <Route path="places/:id/edit" element={<PlaceEditPage />} />
                        <Route path="bookings" element={<BookingsPage />} />
                        <Route path="myProfile" element={<MyProfilePage />} />
                        <Route path="profile/:id" element={<MyProfilePage />} />
                    </Route>
                </Routes>
            </BrowserRouter>
        </>
    );
}

export default Appp;