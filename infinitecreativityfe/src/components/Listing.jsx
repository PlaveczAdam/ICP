import { Box, Button } from "@mui/material";
import { useContext, useEffect } from "react";
import { UserContext } from "./UserContextProvider";
import { InventoryContext } from "./InventoryContextProvider";

function Listing(props) {
  const userCTX = useContext(UserContext);

  const currentDate = new Date();
  const parseDate = new Date(props.listing.listingDate);
  const timeDifference = currentDate.getTime() - parseDate.getTime();
  const daysPassed = Math.floor(timeDifference / (1000 * 60 * 60 * 24));

  async function sell() {
    const res = await fetch(`/api/listing/sold/${props.listing.id}`, { method: "PUT" });
    if (res.ok) {
      userCTX.refresh();
      props.getListings();
    }
  }
  async function cancel() {
    const res = await fetch(`/api/listing/cancelled/${props.listing.id}`, { method: "PUT" });
    if (res.ok) {
      userCTX.refresh();
      props.getListings();
    }
  }
  if (!userCTX.user) {
    return null;
  }

  return (
    <Box
      sx={{
        border: "2px solid rgb(0, 105,94,1)",
        minWidth: "300px",
        maxHeight: "300px",
        borderRadius: "5px",
        background: "rgb(0,105,94,0.7)",
        display: "flex",
        flexDirection: "row",
        padding: "5px",
        marginTop: "3px",
        alignItems: "center",
        justifyContent: "left",
        textAlign: "left",
        flexWrap: "wrap",
        gap: "20px",
      }}
    >
      <Box flexGrow={1}>
        <Box>{`Seller: ${props.listing.seller.name}`}</Box>
        <Box>{`Price: ${props.listing.price}`}</Box>
        <Box>{`Item: ${props.listing.item.name}`}</Box>
        <Box sx={{ fontSize: "small" }}>{`${daysPassed} days ago`}</Box>
      </Box>
      <Box flexShrink={1}>
        {userCTX.user.id !== props.listing.seller.id && (
          <Button onClick={()=>sell()}>Buy</Button>
        )}
        {userCTX.user.id === props.listing.seller.id && (
          <Button onClick={()=>cancel()}>Cancel</Button>
        )}
      </Box>
    </Box>
  );
}

export default Listing;
