import { Box, Tooltip } from "@mui/material";
import { itemTypeName } from "../utils/OptionNames";
import { itemImages } from "../utils/ImportUtils";

function Item(props) {
  const tooltipContent = props.item ? (
    <Box>
      <Box display="grid" gridTemplateColumns="max-content 100px" gap={1}>
        <Box>ItemName:</Box> <Box>{props.item.name}</Box>
        <Box>Description:</Box> <Box>{props.item.description}</Box>
        <Box>Price:</Box> <Box>{props.item.value}</Box>
        <Box>ItemType:</Box> <Box>{props.item.itemType}</Box>
      </Box>
    </Box>
  ) : null;
  return (
    <Tooltip title={tooltipContent} arrow disableInteractive>
      <Box
        width="50px"
        height="50px"
        display="flex"
        bgcolor="#fff"
        flexGrow={0}
        flexShrink={0}
        sx={[
          props.interactive && {
            cursor: "pointer",
            "&:hover": { opacity: 0.8 },
            transition: (theme) => theme.transitions.create("opacity"),
          },
        ]}
        onClick={() => props.onClick?.()}
      >
        {props.item ? (
          <Box
            component="img"
            maxWidth="100%"
            maxHeight="100%"
            src={itemImages[props.item.imageName]}
            alt={props.item.name}
          />
        ) : null}
      </Box>
    </Tooltip>
  );
}

export default Item;
