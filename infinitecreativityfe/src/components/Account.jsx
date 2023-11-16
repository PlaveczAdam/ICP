import { Box, Portal, Button } from "@mui/material";
import { useContext, useState } from "react";
import { UserContext } from "./UserContextProvider";
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
  const [activeButton, setActiveButton] = useState("home");

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
              color={
                activeButton === "characters"
                  ? "activeButtonColor"
                  : "primary"
              }
              onClick={() => setActiveButton("characters")}
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
              color={
                activeButton === "inventory"
                  ? "activeButtonColor"
                  : "primary"
              }
              onClick={() => setActiveButton("inventory")}
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
              <Box id="inventoryPanel">
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
