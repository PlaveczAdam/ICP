import { Box, Button } from "@mui/material";
import { useContext, useEffect } from "react";
import { UserContext } from "./UserContextProvider";
import { InventoryContext } from "./InventoryContextProvider";
import { itemImages } from "../utils/ImportUtils";
import {WalletContext} from "./WalletContextProvider";
import DoubleDisplay from "./DoubleDisplay";
import NumberSimplifyer from "./NumberSimplifyer";


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
        borderRadius: "10px",
        background: "linear-gradient(to bottom right, teal, rgba(0,61,60,1))",
        boxShadow: "0px 20px 20px #101010",
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
      <Box flexGrow={1} padding="5px">
        <Box display="flex" justifyContent="space-between" alignItems="flex-start"><Box>Seller:</Box><Box>{props.listing.seller.name}</Box></Box>
        <Box display="flex" justifyContent="space-between"><Box>Price:</Box><Box><NumberSimplifyer value={props.listing.price}></NumberSimplifyer></Box></Box>
        <Box display="flex" justifyContent="space-between"><Box>Item:</Box><Box>{props.listing.item.name}</Box></Box>
        <Box display="flex" justifyContent="space-between"><Box sx={{ fontSize: "small" }}>{`${daysPassed} days ago`}</Box></Box>
      </Box>
      <Box component="img" sx={{padding: "3px", border: "2px dashed black", width: "70px", borderRadius: "5px"}} src={itemImages[props.listing.item.imageName]}></Box>
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
