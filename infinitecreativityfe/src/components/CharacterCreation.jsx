import {
  Box,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  DialogActions,
  InputLabel,
  FormControl,
  Select,
  MenuItem
} from "@mui/material";
import { useState, useContext, useEffect } from "react";
import { UserContext } from "./UserContextProvider";

function CharacterCreation(props) {
  const [isOpen, setIsOpen] = useState(false);
  const [characterName, setCharacterName] = useState("");
  const userCTX = useContext(UserContext);
  const [profession, setProfession] = useState("");
  const [race, setRace] = useState("");

  async function handleSave() {
    await fetch("/api/character", {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify({ name: characterName , profession: profession, race: race}),
    });
    userCTX.refresh();
  }

  useEffect(() => {
    setCharacterName("");
    setRace("");
    setProfession("");
  }, [isOpen]);
  return (
    <Box minHeight={110} >
      <Button onClick={() => setIsOpen(true)} sx={{ height: "180px" }}>
        Create Character
      </Button>
      <Dialog
        open={isOpen}
        onClose={() => setIsOpen(false)}
        fullWidth
        maxWidth="md"
      >
        <DialogTitle>Character Creation</DialogTitle>
        <DialogContent sx={{display:"flex", gap:4, flexDirection:"column"}}>
          <DialogContentText style={{ color: "white" }}>
          </DialogContentText>
          <TextField
            autoFocus
            id="name"
            label="Character Name"
            fullWidth
            value={characterName}
            onChange={(e) => setCharacterName(e.target.value)}
            autoComplete="off"
          />
          <FormControl fullWidth>
            <InputLabel id="raceSelect">Race</InputLabel>
            <Select
              labelId="raceSelect"
              value={race}
              label="Race"
              onChange={(e)=>setRace(e.target.value)}
            >
              <MenuItem value={"human"}>Hooman</MenuItem>
              <MenuItem value={"notHuman"}>NotHuman</MenuItem>

            </Select>
          </FormControl>

          <FormControl fullWidth>
            <InputLabel id="professionSelect">Profession</InputLabel>
            <Select
              labelId="professionSelect"
              value={profession}
              label="Profession"
              onChange={(e)=>setProfession(e.target.value)}
            >
              <MenuItem value={"warrior"}>Warrior</MenuItem>
              <MenuItem value={"ranger"}>Ranger</MenuItem>
              <MenuItem value={"mage"}>Mage</MenuItem>
              <MenuItem value={"support"}>Support</MenuItem>
            </Select>
          </FormControl>
        </DialogContent>
        <DialogActions>
          <Button
            variant="text"
            onClick={() => setIsOpen(false)}
            color="cancelButtonTextColor"
          >
            Cancel
          </Button>
          <Button
            onClick={() => {
              handleSave();
              setIsOpen(false);
            }}
            disabled={characterName === "" || profession === "" || race === ""}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}

export default CharacterCreation;
