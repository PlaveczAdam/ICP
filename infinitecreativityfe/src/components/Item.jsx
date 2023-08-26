import { Box, Tooltip } from "@mui/material";
import { itemImages } from "../utils/ImportUtils";
import PriceCheckOutlinedIcon from "@mui/icons-material/PriceCheckOutlined";
import StarRoundedIcon from "@mui/icons-material/StarRounded";
function Item(props) {
  const tooltipContent = props.item ? (
    <Box>
      <Box display="grid" gridTemplateColumns="max-content 100px" gap={1}>
        <Box>ItemName:</Box> <Box>{props.item.name}</Box>
        <Box>Description:</Box> <Box>{props.item.description}</Box>
        <Box>Price:</Box> <Box>{props.item.value}</Box>
        <Box>ItemType:</Box> <Box>{props.item.itemType}</Box>
        <Box>Rarity:</Box> <Box>{props.item.rarity}</Box>
        <Box>Amount:</Box> <Box>{props.item.amount}</Box>
      </Box>
    </Box>
  ) : null;
  return (
    <Tooltip title={tooltipContent} arrow disableInteractive>
      {props.item ? (
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
              background:
                props.item.rarity === "superRare"
                  ? "rgba(243, 150, 25, 0.6)"
                  : props.item.rarity === "rare"
                  ? "rgba(11, 140, 226, 0.66)"
                  : props.item.rarity === "common"
                  ? "rgba(0, 190, 30, 0.6)"
                  : "rgba(143, 143, 143, 0.6)",
              width: "70px",
              height: "70px",
              borderRadius: "5px",
              border: "2px solid",
              borderColor:
                props.item.rarity === "superRare"
                  ? "rgba(255, 150, 25, 1)"
                  : props.item.rarity === "rare"
                  ? "rgba(11, 140, 255, 1)"
                  : props.item.rarity === "common"
                  ? "rgba(0, 210, 30, 1)"
                  : "rgba(170,170,170, 1)",
            },
            props.interactive && {
              cursor: "pointer",
              "&:hover": {
                background:
                  props.item.rarity === "superRare"
                    ? "rgba(255, 150, 25, 1)"
                    : props.item.rarity === "rare"
                    ? "rgba(11, 140, 255, 1)"
                    : props.item.rarity === "common"
                    ? "rgba(0, 210, 30, 1)"
                    : "rgba(170,170,170, 1)",
                boxShadow:
                  props.item.rarity === "superRare"
                    ? "0px 0px 20px rgba(255, 150, 25, 1)"
                    : props.item.rarity === "rare"
                    ? "0px 0px 20px rgba(11, 140, 255, 1)"
                    : props.item.rarity === "common"
                    ? "0px 0px 20px rgba(0, 210, 30, 1)"
                    : "0px 0px 20px rgba(170,170,170, 1)",
              },
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
              {props.item.isEquipped && (
                <StarRoundedIcon
                  sx={{
                    position: "absolute",
                    bottom: 0,
                    right: 0,
                    color: "white",
                  }}
                ></StarRoundedIcon>
              )}
              {props.item.amount > 0 && (
                <Box
                  style={{
                    display: "flex",
                    alignItems: "flex-end",
                    fontWeight: 800,
                    color: "#000",
                    minWidth: "25px",
                    minHeight: "15px",
                    borderRadius: "5px 0px 0px 0px",
                    justifyContent: "center",
                    position: "absolute",
                    bottom: 0,
                    right: 0,
                    fontSize: "small",
                    background:
                      props.item.rarity === "superRare"
                        ? "rgba(255, 150, 25, 1)"
                        : props.item.rarity === "rare"
                        ? "rgba(11, 140, 255, 1)"
                        : props.item.rarity === "common"
                        ? "rgba(0, 210, 30, 1)"
                        : "rgba(170,170,170, 1)", //props.item.rarity === "superRare" ? "rgba(243, 150, 25, 1)" : props.item.rarity === "rare" ? "rgba(11, 140, 226, 1)" : props.item.rarity === "common" ? "rgba(0, 190, 30, 1)" : "rgba(143, 143, 143, 1)"
                  }}
                >
                  {props.item.amount}
                </Box>
              )}
            </>
          ) : null}
        </Box>
      ) : (
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
              background: "transparent",
              width: "70px",
              height: "70px",
              borderRadius: "5px",
              border: "2px solid",
              borderColor: "gray",
            },
            props.interactive && {
              cursor: "pointer",
              "&:hover": {
                background: "rgba(170,170,170, 1)",
              },
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
              {props.item.isEquipped && (
                <StarRoundedIcon
                  sx={{
                    position: "absolute",
                    bottom: 0,
                    right: 0,
                    color: "white",
                  }}
                ></StarRoundedIcon>
              )}
            </>
          ) : null}
        </Box>
      )}
    </Tooltip>
  );
}

export default Item;
