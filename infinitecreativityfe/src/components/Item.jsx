import { Box, Tooltip } from "@mui/material";
import {itemTypeName} from "../utils/OptionNames";

function Item(props) {
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
      <Box width="50px" height="50px">
        {props.item.id}
      </Box>
    </Tooltip>
  );
}

export default Item;
