import {
  Card,
  Box,
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  TextField,
  DialogActions,
} from "@mui/material";
import { useState, useContext } from "react";
import { UserContext } from './UserContextProvider';

function CharacterCreation(props) {
  const [isOpen, setIsOpen] = useState(false);
  const [characterName, setCharacterName] = useState("");
  const userCTX = useContext(UserContext);

  async function handleSave() {
    await fetch("/api/character", {
      method: "POST",
      headers: { "content-type": "application/json" },
      body:JSON.stringify({name:characterName})
    });
    userCTX.refresh();
  }
  return (
    <Card variant="outlined">
      <Box minHeight={110}>
        <Button onClick={() => setIsOpen(true)} sx={{height:"180px"}}>Create Character</Button>
        <Dialog open={isOpen} onClose={() => setIsOpen(false)}>
          <DialogTitle>Character Creation</DialogTitle>
          <DialogContent>
            <DialogContentText>Name your character.</DialogContentText>
            <TextField
              autoFocus
              margin="dense"
              id="name"
              label="Character Name"
              fullWidth
              variant="standard"
              value={characterName}
              onChange={(e) => setCharacterName(e.target.value)}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={() => setIsOpen(false)}>Cancel</Button>
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
    </Card>
  );
}

export default CharacterCreation;
