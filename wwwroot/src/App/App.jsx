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

function App() {

  return (
    <main>
      <Navbar />
      <Router>
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/rent" element={<Rent />}></Route>
          <Route path="/about-us" element={<AboutUs />}></Route>
          <Route path="/login" element={<Login />}></Route>
          <Route path="/sign-up" element={<SignUp />}></Route>
          <Route path="/checkout" element={<Checkout />}></Route>
          <Route path="/profile" element={<Profile />}></Route>
        </Routes>
      </Router>
    </main>
  );
}

export default App;
