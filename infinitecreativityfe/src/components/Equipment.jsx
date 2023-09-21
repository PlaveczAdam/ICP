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
  const [skills, setSkills] = useState();
  const [skillSlot, setSkillSlot] = useState(-1);

  const inventoryCTX = useContext(InventoryContext);

  async function getEquipment() {
    const res = await fetch(`/api/character/equipment/${props.characterID}`);
    let equipment = await res.json();
    setEquipment(equipment);
  }

  async function getSkills() {
    const res = await fetch(`/api/character/skills/${props.characterID}`);
    let sks = await res.json();
    setSkills(sks.skillHolders);
  }
  async function equipSkill(item) {
    const newSkills = [...skills];
    newSkills[skillSlot] = item;

    const res = await fetch(
      `/api/character/skills/${props.characterID}`,
      { method: "PUT", body:JSON.stringify({skills:newSkills.map(x=>x?.id)}), headers:{"content-type":"application/json"}}
    );
    if (res.ok) {
      setSkills(newSkills);
      inventoryCTX.refresh();
    }
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

  function filterItem(item)
  {
    if(armor)
    {
      return item.armorType === armor
    }else if(weapon)
    {
      return item.itemType === "weapon" 
    }else if(skillSlot !== -1)
    {
      return item.itemType === "skill"
    }
    return false;
  }

  return (
    <Box>
      <Button
        type="btn"
        onClick={() => {
          getEquipment();
          getSkills();
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
        {(equipment && skills) ? (
          <Box
            display="flex"
            alignItems="flex-start"
            flexGrow={1}
            overflow="auto"
          >
            <Box
              display="flex"
              flexGrow={1}
              flexDirection="column"
              alignSelf="stretch"
            >
              <Box flexGrow={1} minHeight={0} overflow="auto">
                <Box
                  display="flex"
                  flexGrow={1}
                  gap={2}
                  flexWrap="wrap"
                  alignItems="flex-start"
                >
                  { inventoryCTX.inventory
                        .filter(filterItem)
                        .map((x) => (
                          <Item
                            item={x}
                            onClick={() => skillSlot===-1? equip(x):equipSkill(x)}
                            key={x.id}
                            interactive
                          ></Item>
                        ))
                   }
                </Box>
              </Box>
              <Box minHeight={125} display="flex">
                {skills.map((x, ind) => (
                  <Box display="flex" flexDirection="column" alignItems="center" key={ind}>
                    <Item
                      item={x}
                      onClick={() => {
                        setArmor("");
                        setWeapon("");
                        setSkillSlot(ind);
                      }}
                      interactive
                    ></Item>
                    <Collapse
                      in={x && ind === skillSlot}
                      orientation="vertical"
                    >
                      <Button onClick={() => equipSkill(null)}>
                        Unequip
                      </Button>
                    </Collapse>
                  </Box>
                ))}
              </Box>
            </Box>
            <Box display="flex" flexDirection="column" gap="3px" minWidth={220}>
              <Box display="flex" flexDirection="row" alignItems="center">
                <Item
                  item={equipment.head}
                  onClick={() => {
                    setArmor("head");
                    setWeapon("");
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
                    setSkillSlot(-1);
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
