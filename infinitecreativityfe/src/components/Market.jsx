import { Box, Portal, Button } from "@mui/material";
import InventoryContextProvider from "./InventoryContextProvider";
import { useContext, useEffect, useState } from "react";
import { Link, Outlet } from "react-router-dom";
import { UserContext } from "./UserContextProvider";
import { WalletContext } from "./WalletContextProvider";
import AnimatedNumber from "animated-number-react";

function Market(props) {
  const userCTX = useContext(UserContext);
  const walletCTX = useContext(WalletContext);
  const [activeButton, setActiveButton] = useState("home");

  if (!userCTX.user) {
    return null;
  }

  function formatV(value)
  {
    return value.toFixed(0);
  }
  return (
    <InventoryContextProvider>
      <Portal container={() => document.getElementById("sideBarContent")}>
        Market
        <Box>
          <AnimatedNumber value={walletCTX.wallet.money} duration={2000} formatValue={formatV}></AnimatedNumber>
        </Box>
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
            color={
              activeButton === "create listing"
                ? "activeButtonColor"
                : "primary"
            }
            onClick={() => setActiveButton("create listing")}
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
            color={
              activeButton === "stock"
                ? "activeButtonColor"
                : "primary"
            }
            onClick={() => setActiveButton("stock")}
            component={Link}
            to="allListings"
          >
            Stock
          </Button>
          <Button
            sx={{
              justifyContent: "center",
              height: "40px",
              width: "100%",
            }}
            color={
              activeButton === "my listing"
                ? "activeButtonColor"
                : "primary"
            }
            onClick={() => setActiveButton("my listing")}
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
            <Box id="inventoryPanel"></Box>
          </Box>
        </Box>
      </Portal>
      <Outlet />
    </InventoryContextProvider>
  );
}

export default Market;
