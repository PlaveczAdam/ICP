import { InventoryContext } from "./InventoryContextProvider";
import { useContext } from 'react';
import { Box } from '@mui/material';
import Item from "./Item"

function Inventory(props)
{
    const inventoryCTX = useContext(InventoryContext);

    return<Box display="flex" flexWrap="wrap" gap="2px">
        {inventoryCTX.inventory.map((x)=><Item item={x} key={x.id}></Item>)}
    </Box>
}

export default Inventory;