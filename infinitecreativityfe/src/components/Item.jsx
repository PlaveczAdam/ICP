import { Box, Tooltip } from "@mui/material";
import {itemTypeName} from "../utils/OptionNames";
import { itemImages } from "../utils/ImportUtils";

function Item(props) {
  console.log(itemImages);
  const tooltipContent = (
    <Box>
      <Box display="grid" gridTemplateColumns="max-content 100px" gap={1}>
        <Box>ItemName:</Box> <Box>{props.item.name}</Box>
        <Box>Description:</Box> <Box>{props.item.description}</Box>
        <Box>Price:</Box> <Box>{props.item.value}</Box>
        <Box>ItemType:</Box> <Box>{itemTypeName[props.item.itemType]}</Box>
      </Box>
    </Box>
  );
  return (
    <Tooltip title={tooltipContent} arrow>
      <Box width="50px" height="50px" display="flex" bgcolor="#fff">
        <img src={itemImages[props.item.imageName]} alt={props.item.name}/>
      </Box>
    </Tooltip>
  );
}

export default Item;
