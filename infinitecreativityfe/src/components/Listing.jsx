import { Box, Button } from "@mui/material";
import { useContext, useEffect } from "react";
import { UserContext } from "./UserContextProvider";
import { InventoryContext } from "./InventoryContextProvider";
import {WalletContext} from "./WalletContextProvider";
import DoubleDisplay from "./DoubleDisplay";

function Listing(props) {
  const userCTX = useContext(UserContext);
  const inventoryCTX = useContext(InventoryContext);
  const walletCTX = useContext(WalletContext);
  const currentDate = new Date();
  const parseDate = new Date(props.listing.listingDate);
  const timeDifference = currentDate.getTime() - parseDate.getTime();
  const daysPassed = Math.floor(timeDifference / (1000 * 60 * 60 * 24));

  async function sell() {
    const res = await fetch(`/api/listing/sold/${props.listing.id}`, {
      method: "PUT",
    });
    if (res.ok) {
      inventoryCTX.refresh();
      walletCTX.refresh();
      props.getListings();
    }
  }
  async function cancel() {
    const res = await fetch(`/api/listing/cancelled/${props.listing.id}`, {
      method: "PUT",
    });
    if (res.ok) {
      inventoryCTX.refresh();
      props.getListings();
    }
  }

  return (
    <Box
      sx={{
        border: "2px solid rgb(0, 105,94,1)",
        borderRadius: "5px",
        background: "rgb(0,105,94,0.7)",
        display: "flex",
        flexDirection: "row",
        padding: "5px",
        marginTop: "3px",
        alignItems: "center",
        justifyContent: "left",
        textAlign: "left",
        gap: "20px",
      }}
    >
      <Box flexGrow={1}>
        <Box>{`Seller: ${props.listing.seller.name}`}</Box>
        <Box>Price: <DoubleDisplay value={props.listing.price} precision={0}></DoubleDisplay></Box>
        <Box>{`Item: ${props.listing.item.name}`}</Box>
        <Box sx={{ fontSize: "small" }}>{`${daysPassed} days ago`}</Box>
      </Box>
      <Box flexShrink={1}>
        {userCTX.user.id !== props.listing.seller.id && (
          <Button onClick={() => sell()} disabled={props.listing.price>walletCTX.wallet.money}>Buy</Button>
        )}
        {userCTX.user.id === props.listing.seller.id && (
          <Button onClick={() => cancel()}>Cancel</Button>
        )}
      </Box>
    </Box>
  );
}

export default Listing;
