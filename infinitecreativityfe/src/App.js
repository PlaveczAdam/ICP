import "./App.css";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import { useContext } from "react";

import Login from "./components/Login";
import Registration from "./components/Registration";
import Home from "./components/Home";
import Account from './components/Account';
import { UserContext } from "./components/UserContextProvider";
import { Box } from "@mui/material";

function App() {
  const userCTX = useContext(UserContext);
  async function handleLogOut()
  {
    await fetch("/api/player/logout");
    userCTX.refresh();
  }

  return (
    <div className="App">
      <header className="App-header">
        <Router>
          <Link to="/">Home</Link>
          {userCTX.user && <Link to="/account">Account</Link>}
          {!userCTX.user && <Link to="/login">Login</Link>}
          {!userCTX.user && <Link to="/registration">Registration</Link>}
          {userCTX.user && <Link to="/" onClick={()=>handleLogOut()}>Log Out</Link>}
          {userCTX.user && <Box>You are logged in as: {userCTX.user.name}</Box>}
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
