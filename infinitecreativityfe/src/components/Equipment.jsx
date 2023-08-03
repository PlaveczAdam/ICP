import {
  Button,
  Box,
  Table,
  TableRow,
  TableHead,
  TableCell,
  TableBody,
  LinearProgress,
  Collapse,
} from "@mui/material";
import { useState, forwardRef, useEffect, useContext } from "react";
import Dialog from "@mui/material/Dialog";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import CloseIcon from "@mui/icons-material/Close";
import Slide from "@mui/material/Slide";
import Item from "./Item";
import { InventoryContext } from "./InventoryContextProvider";

const Transition = forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function Equipment(props) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [equipment, setEquipment] = useState();
  const [armor, setArmor] = useState("");
  const [weapon, setWeapon] = useState("");

  const inventoryCTX = useContext(InventoryContext);

  async function getEquipment() {
    const res = await fetch(`/api/character/equipment/${props.characterID}`);
    let equipment = await res.json();
    setEquipment(equipment);
  }

  async function equip(item) {
    const res = await fetch(
      `/api/character/equipment/${props.characterID}/${item.id}`,
      { method: "PUT" }
    );
    if (res.ok) {
      const key = armor || weapon;
      setEquipment((old) => ({ ...old, [key]: { ...item, isEquipped: true } }));
      inventoryCTX.refresh();
    }
  }

  async function unEquip(item) {
    const res = await fetch(
      `/api/character/unequip/${props.characterID}/${item.id}`,
      { method: "PUT" }
    );
    if (res.ok) {
      const key = armor || weapon;
      setEquipment((old) => ({ ...old, [key]: null }));
      inventoryCTX.refresh();
    }
  }
  return (
    <Box>
      <Button
        type="btn"
        onClick={() => {
          getEquipment();
          setIsModalOpen(true);
        }}
      >
        Equipment
      </Button>
      <Dialog
        fullScreen
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        TransitionComponent={Transition}
      >
        <AppBar sx={{ position: "relative" }}>
          <Toolbar>
            <IconButton
              edge="start"
              color="inherit"
              onClick={() => setIsModalOpen(false)}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Equipment
            </Typography>
          </Toolbar>
        </AppBar>
        {equipment ? (
          <Box display="flex">
            <Box display="flex" flexGrow={1} gap={2} flexWrap="wrap">
              {armor || weapon
                ? inventoryCTX.inventory
                    .filter((x) =>
                      armor
                        ? x.itemType === "armor" && x.armorType === armor
                        : x.itemType === "weapon"
                    )
                    .map((x) => (
                      <Item
                        item={x}
                        onClick={() => equip(x)}
                        key={x.id}
                        interactive
                      ></Item>
                    ))
                : null}
            </Box>
            <Box display="flex" flexDirection="column" gap="3px" minWidth={220}>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.head}
                  onClick={() => {
                    setArmor("head");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.head && armor === "head"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.head)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.shoulder}
                  onClick={() => {
                    setArmor("shoulder");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.shoulder && armor === "shoulder"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.shoulder)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.chest}
                  onClick={() => {
                    setArmor("chest");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.chest && armor === "chest"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.chest)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.hand}
                  onClick={() => {
                    setArmor("hand");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.hand && armor === "hand"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.hand)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.leg}
                  onClick={() => {
                    setArmor("leg");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.leg && armor === "leg"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.leg)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.boot}
                  onClick={() => {
                    setArmor("boot");
                    setWeapon("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.boot && armor === "boot"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.boot)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.weapon}
                  onClick={() => {
                    setWeapon("weapon");
                    setArmor("");
                  }}
                  interactive
                ></Item>
                <Collapse
                  in={equipment.weapon && weapon === "weapon"}
                  orientation="horizontal"
                >
                  <Button onClick={() => unEquip(equipment.weapon)}>
                    Unequip
                  </Button>
                </Collapse>
              </Box>
            </Box>
          </Box>
        ) : (
          <LinearProgress />
        )}
      </Dialog>
    </Box>
  );
}

export default Equipment;
