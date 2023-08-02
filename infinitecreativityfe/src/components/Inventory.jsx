import { InventoryContext } from "./InventoryContextProvider";
import { useContext, useState, useEffect } from "react";
import { Box, Portal, Collapse } from "@mui/material";
import Item from "./Item";
import { LoadingButton } from "@mui/lab";
import { UserContext } from "./UserContextProvider";

function Inventory(props) {
  const inventoryCTX = useContext(InventoryContext);
  const [selectedItems, setSelectedItems] = useState([]);
  let price = selectedItems.map(x => x.value).reduce((x, y) => x + y, 0);

 /*  useEffect(() => {
    inventoryCTX.refresh();
  }, [])
 */
  
  async function handleDelete() {
    let res = await fetch("/api/item", {
      method: "DELETE",
      body: JSON.stringify({ items: selectedItems.map((x) => x.id) }),
      headers: { "Content-Type": "application/json" },
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
