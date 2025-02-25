import React from 'react';
import './App.css';
import '../Sections/Navbar/Navbar.jsx';
import Navbar from '../Sections/Navbar/Navbar.jsx';
import Home from '../Sections/Home/Home.jsx';

function App() {

  return (
    <div className="App">
      <Navbar />
      <Home />
    </div>
  );
}

export default App;
