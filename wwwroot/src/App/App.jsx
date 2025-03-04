import React from 'react';
import { BrowserRouter as Router, Route, Routes } from "react-router";
import './App.css';
import Navbar from '../Components/Navbar/Navbar.jsx';
import Home from '../Sections/Home/Home.jsx';
import Rent from '../Sections/Rent/Rent.jsx';

function App() {

  return (
    <main>
      <Navbar />
      <Router>
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/rent" element={<Rent />}></Route>
        </Routes>
      </Router>
    </main>
  );
}

export default App;
