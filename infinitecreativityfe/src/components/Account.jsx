import { Box, Portal, Button } from "@mui/material";
import { useContext } from "react";
import { UserContext } from "./UserContextProvider";
import Character from "./Character";
import CharacterCreation from "./CharacterCreation";
import { BrowserRouter as Router, Routes, Route, Link, Outlet } from "react-router-dom";
import InventoryContextProvider from "./InventoryContextProvider";

function Account(props) {
  const userCTX = useContext(UserContext);

  if(!userCTX.user)
  {return null;}

  return (
    <InventoryContextProvider>
    <Box flexGrow="1">
      <Portal container={() => document.getElementById("sideBarContent")}>
        Account
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            height: "100%",
            width: "100%",
            alignItems: "center",
          }}
        >
          <Button
            component={Link}
            sx={{
              justifyContent: "center",
              height: "40px",
              width: "100%",
            }}
            to="characters"
          >
            Characters
          </Button>
          <Button
            sx={{
              justifyContent: "center",
              height: "40px",
              width: "100%",
            }}
            component={Link}
            to="inventory"
          >
            Inventory
          </Button>
        </Box>
      </Portal>
      <Outlet />
    </Box>
    </InventoryContextProvider>
  );
}

export default Account;
