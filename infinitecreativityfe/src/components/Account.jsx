import { Box, Portal, Button, Collapse } from "@mui/material";
import { useContext, useState } from "react";
import { UserContext } from "./UserContextProvider";
import Character from "./Character";
import CharacterCreation from "./CharacterCreation";
import {
  BrowserRouter as Router,
  Routes,
  Route,
  Link,
  Outlet,
} from "react-router-dom";
import InventoryContextProvider from "./InventoryContextProvider";

function Account(props) {
  const userCTX = useContext(UserContext);
  const [checked, setChecked] = useState(false);

  if (!userCTX.user) {
    return null;
  }

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
              onClick={() => setChecked(false)}
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
              onClick={() => setChecked(true)}
            >
              Inventory
            </Button>
            <Box
              sx={{
                "& > :not(style)": {
                  display: "flex",
                  justifyContent: "space-around",
                  height: 120,
                  width: 250,
                },
              }}
            >
              <Box>
                <Collapse in={checked}>Content here</Collapse>
              </Box>
            </Box>
          </Box>
        </Portal>
        <Outlet />
      </Box>
    </InventoryContextProvider>
  );
}

export default Account;
