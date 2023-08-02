import { Box, Tooltip } from "@mui/material";
import { itemTypeName } from "../utils/OptionNames";
import { itemImages } from "../utils/ImportUtils";
import PriceCheckOutlinedIcon from "@mui/icons-material/PriceCheckOutlined";

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
        position="relative"
        width="50px"
        height="50px"
        display="flex"
        bgcolor="#fff"
        flexGrow={0}
        flexShrink={0}
        sx={[
          {
            background: "rgba(0,105,93,0.7)",
            width: "70px",
            height: "70px",
            borderRadius: "5px",
            border: "2px solid rgba(0,105,93,1)"
          },
          props.interactive && {
            cursor: "pointer",
            "&:hover": { background: "rgba(0,105,93,1)", opacity: 1 },
            transition: (theme) => theme.transitions.create("opacity"),
          },
        ]}
        onClick={() => props.onClick?.()}
      >
        {props.item ? (
          <>
            <Box
              component="img"
              maxWidth="100%"
              maxHeight="100%"
              src={itemImages[props.item.imageName]}
              alt={props.item.name}
            />
            {props.selected && (
              <PriceCheckOutlinedIcon
                sx={{
                  position: "absolute",
                  backgroundColor: "#000000aa",
                  color: "#fff",
                  top: 0,
                  left: 0,
                }}
              ></PriceCheckOutlinedIcon>
            )}
          </>
        ) : null}
      </Box>
    </Tooltip>
  );
}

export default Item;