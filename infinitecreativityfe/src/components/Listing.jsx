import { Box, Button, TextField} from "@mui/material";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "./UserContextProvider";
import { InventoryContext } from "./InventoryContextProvider";
import { itemImages } from "../utils/ImportUtils";
import { WalletContext } from "./WalletContextProvider";
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
  const [amount, setAmount] = useState(1);

  async function sell() {
    const res = await fetch(
      `/api/listing/sold/${props.listing.id}/${amount}`,
      {
        method: "PUT",
      }
    );
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
        <Box
          display="flex"
          justifyContent="space-between"
          alignItems="flex-start"
        >
          <Box>Seller:</Box>
          <Box>{props.listing.seller.name}</Box>
        </Box>
        <Box display="flex" justifyContent="space-between">
          <Box>Price:</Box>
          <Box>
            <NumberSimplifyer value={props.listing.price}></NumberSimplifyer>
          </Box>
        </Box>
        <Box display="flex" justifyContent="space-between">
          <Box>Item:</Box>
          <Box>{props.listing.item.name}</Box>
        </Box>
        <Box display="flex" justifyContent="space-between">
          <Box sx={{ fontSize: "small" }}>{`${daysPassed} days ago`}</Box>
        </Box>
      </Box>
      <Box
        component="img"
        sx={{
          padding: "3px",
          border: "2px dashed black",
          width: "70px",
          borderRadius: "5px",
        }}
        src={itemImages[props.listing.item.imageName]}
      ></Box>
      <Box flexShrink={1}>
        {userCTX.user.id !== props.listing.seller.id && (
          <>
            <Button
              onClick={() => sell()}
              disabled={props.listing.price * amount > walletCTX.wallet.money || amount > (props.listing.item.amount??1) || amount < 1}
            >
              Buy
            </Button>
            <TextField
              margin="normal"
              required
              fullWidth
              id="amount"
              label="Amount"
              name="amount"
              autoFocus
              InputProps={{ inputProps: { min: "1", max: props.listing.item.amount??1, step: "1" } }}
              type="number"
              value={amount}
              onChange={(e) => setAmount(e.target.value)}
            />
          </>
        )}
        {userCTX.user.id === props.listing.seller.id && (
          <Button onClick={() => cancel()}>Cancel</Button>
        )}
      </Box>
    </Box>
  );
}

export default Listing;
