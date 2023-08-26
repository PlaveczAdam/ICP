import { InventoryContext } from "./InventoryContextProvider";
import { useContext, useState, useEffect } from "react";
import { Box, Portal, Collapse } from "@mui/material";
import Item from "./Item";
import { LoadingButton } from "@mui/lab";
import { UserContext } from "./UserContextProvider";
import { ToastContainer, toast, Slide, Flip, cssTransition } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function Inventory(props) {
  const inventoryCTX = useContext(InventoryContext);
  const [selectedItems, setSelectedItems] = useState([]);
  let price = selectedItems.map(x => x.amount ? x.value * x.amount : x.value).reduce((x, y) => x + y, 0);
  const groupedItems = {};
  inventoryCTX.inventory.forEach((x) => {
    const {rarityType, stackableType, amount} = x;
    const key = `${rarityType}-${stackableType}`;
    if(!groupedItems[key]){
      groupedItems[key] = {
        key,
        rarityType, 
        stackableType, 
        totalAmount: 0,
      };
    }
    groupedItems[key].totalAmount += amount;
  });
  
  async function handleDelete() {
    let res = await toast.promise(fetch("/api/item", {
      method: "DELETE",
      body: JSON.stringify({ items: selectedItems.map((x) => x.id) }),
      headers: { "Content-Type": "application/json" },
    }), {
      pending: "Processing...",
      success: `You have gained ${price} gold!`,
      error: "Something went wrong!",
      theme: "dark",
      transition: "slide",
      closeOnClick: true,
      autoClose: 2000,
    });
    if (res.ok) {
      setSelectedItems([]);
      inventoryCTX.refresh();
    }
  }

  return (
    <Box display="flex" flexWrap="wrap" gap="2px">
      <Portal container={() => document.getElementById("inventoryPanel")}>
        <Collapse in appear>
          <LoadingButton
            disabled={selectedItems.length === 0}
            onClick={() => handleDelete()}
          >
            {`Sell Item(s): $${price}`}
          </LoadingButton>
        </Collapse>
      </Portal>
      {inventoryCTX.inventory.map((x) => {
        let selected = selectedItems.includes(x);
        return (
          <Item
            item={x}
            key={x.id}
            selected={selected}
            interactive
            onClick={() => {
              if (selected) {
                setSelectedItems((old) => old.filter((y) => y !== x));
              } else {
                setSelectedItems((old) => [...old, x]);
              }
            }}
          ></Item>
        );
      })}
    </Box>
  );
}

export default Inventory;
