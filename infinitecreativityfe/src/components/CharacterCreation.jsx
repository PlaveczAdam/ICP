import {
  Box,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  DialogActions,
} from "@mui/material";
import { useState, useContext, useEffect } from "react";
import { UserContext } from "./UserContextProvider";

function CharacterCreation(props) {
  const [isOpen, setIsOpen] = useState(false);
  const [characterName, setCharacterName] = useState("");
  const userCTX = useContext(UserContext);

  async function handleSave() {
    await fetch("/api/character", {
      method: "POST",
      headers: { "content-type": "application/json" },
      body: JSON.stringify({ name: characterName }),
    });
    userCTX.refresh();
  }

  useEffect(() => {
    setCharacterName("");
  }, [isOpen]);
  return (
    <Box minHeight={110}>
      <Button onClick={() => setIsOpen(true)} sx={{ height: "180px" }}>
        Create Character
      </Button>
      <Dialog open={isOpen} onClose={() => setIsOpen(false)} fullWidth maxWidth="md">
        <DialogTitle>Character Creation</DialogTitle>
        <DialogContent>
          <DialogContentText style={{padding: '10px 0px', color: 'white'}}>Name your character.</DialogContentText>
          <TextField
            autoFocus
            id="name"
            label="Character Name"
            fullWidth
            value={characterName}
            onChange={(e) => setCharacterName(e.target.value)}
            autoComplete="off"
          />
        </DialogContent>
        <DialogActions>
          <Button variant="text" onClick={() => setIsOpen(false)} color="cancelButtonTextColor">Cancel</Button>
          <Button
            onClick={() => {
              handleSave();
              setIsOpen(false);
            }}
            disabled={characterName === ""}
          >
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}

export default CharacterCreation;
