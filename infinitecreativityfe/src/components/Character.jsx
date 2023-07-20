import { Box, Card } from "@mui/material";
import Quests from "./Quests"

function Character(props) {
  return (
    <Card variant="outlined">
      <Box padding={4} minHeight={110}>
        <Box>Name: {props.character.name}</Box>
        <Box>Purse: {props.character.purse}</Box>
        <Quests characterID={props.character.id}></Quests>
      </Box>
    </Card>
  );
}

export default Character;
