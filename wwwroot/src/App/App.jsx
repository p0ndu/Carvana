import React from 'react';
import { BrowserRouter as Router, Route, Routes } from "react-router";
import './App.css';
import Navbar from '../Components/Navbar/Navbar.jsx';
import Home from '../Sections/Home/Home.jsx';
import Rent from '../Sections/Rent/Rent.jsx';
import AboutUs from '../Sections/AboutUs/AboutUs.jsx';
import Login from '../Sections/Login/Login.jsx';
import SignUp from '../Sections/SignUp/SignUp.jsx';
import Checkout from '../Sections/Checkout/Checkout.jsx';
import Profile from '../Sections/Profile/Profile.jsx';
import Cookies from 'js-cookie';

function App() {
  var user = Cookies.get('user');
  var logged_in = false;

  if (user) {
    console.log("User is logged in");
    logged_in = true;
  }
  else {
    console.log("User is not logged in");
  }

  return (
    <main>
      <Navbar logged_in={logged_in} />
      <Router>
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/car-rent" element={<Rent />}></Route>
          <Route path="/about-us" element={<AboutUs />}></Route>
          <Route path="/log-in" element={<Login />}></Route>
          <Route path="/sign-up" element={<SignUp />}></Route>
          <Route path="/checkout" element={<Checkout logged_in={logged_in} />}></Route>
          <Route path="/profile" element={<Profile />}></Route>
        </Routes>
      </Router>
    </main>
  );
}

export default App;
