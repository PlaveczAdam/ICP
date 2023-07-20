import "./App.css";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import { useEffect, useState } from "react";

import Login from "./components/Login";
import Registration from "./components/Registration";
import Home from "./components/Home";
import Account from './components/Account';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Router>
          <Link to="/">Home</Link>
          <Link to="/account">Account</Link>
          <Link to="/login">Login</Link>
          <Link to="/registration">Registration</Link>
          <Routes>
            <Route
              path="/registration"
              element={<Registration></Registration>}
            ></Route>
            <Route path="/account" element={<Account></Account>}></Route>
            <Route path="/login" element={<Login></Login>}></Route>
            <Route path="/" element={<Home></Home>}></Route>
          </Routes>
        </Router>
      </header>
    </div>
  );
}

export default App;
