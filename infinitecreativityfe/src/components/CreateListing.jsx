import { Box, TextField, Button, Collapse } from "@mui/material";
import Item from "./Item";
import { useContext, useState } from "react";
import { InventoryContext } from "./InventoryContextProvider";

function CreateListing(props) {
  const inventoryCTX = useContext(InventoryContext);
  const [item, setItem] = useState();
  const [price, setPrice] = useState(0);
  const [inventoryOpen, setInventoryOpen] = useState(false);

  async function handleCreate() {
    const res = await fetch("/api/listing", {
      method: "POST",
      body: JSON.stringify({ itemId: item.id, price: price }),
      headers: { "Content-Type": "application/json" },
    });
    if (res.ok) {
      setPrice(0);
      setItem();
      inventoryCTX.refresh();
    }
  }
  function handleSubmit(e) {
    e.preventDefault();
    if (!price || !item) {
      return;
    }
    handleCreate();
  }
  function handleSelection() {
    setItem();
    setInventoryOpen(true);
  }

  return (
    <Box>
      <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
        <Item
          item={item}
          onClick={() => {
            handleSelection();
          }}
          interactive
        ></Item>
        <Collapse in={inventoryOpen}>
          <Box display="flex" flexWrap="wrap">
            {inventoryCTX.inventory.map((x) => (
              <Item
                item={x}
                key={x.id}
                interactive
                onClick={() => {
                    setItem(x);
                    setInventoryOpen(false);
                }}
              ></Item>
            ))}
          </Box>
        </Collapse>
        <TextField
          margin="normal"
          required
          fullWidth
          id="price"
          label="Price"
          name="price"
          autoFocus
          value={price}
          onChange={(e) => setPrice(e.target.value)}
          type="number"
        />
        <Button
          disabled={!price || !item}
          type="submit"
          fullWidth
          variant="contained"
          sx={{ mt: 3, mb: 2 }}
        >
          Submit
        </Button>
      </Box>
    </Box>
  );
}

export default CreateListing;
