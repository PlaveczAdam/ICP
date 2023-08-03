import { Box, Portal, Button } from "@mui/material";
import InventoryContextProvider from "./InventoryContextProvider";
import { useEffect, useState } from "react";
import { Link, Outlet } from 'react-router-dom';

function Market(props) {
  return (
    <InventoryContextProvider>
      <Portal container={() => document.getElementById("sideBarContent")}>
        Market
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
              to="createListing"
            >
              Create Listing
            </Button>
            <Button
              sx={{
                justifyContent: "center",
                height: "40px",
                width: "100%",
              }}
              component={Link}
              to="allListings"
            >
              All Listings
            </Button>
            <Button
              sx={{
                justifyContent: "center",
                height: "40px",
                width: "100%",
              }}
              component={Link}
              to="myListings"
            >
              My Listings
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
      <Outlet/>
    </InventoryContextProvider>
  );
}

export default Market;
