import { Box, Card, Grow, Slide } from "@mui/material";
import Quests from "./Quests";

function Character(props) {
  return (
    <Grow in appear>
    <Card
      variant="outlined"
      sx={{border: '3px solid rgba(7,163,173,0.33)'}}
    >
      <Box padding={4} minHeight={110}>
        <Box>Name: {props.character.name}</Box>
        <Quests characterID={props.character.id}></Quests>
      </Box>
    </Card>
    </Grow>
  );
}

export default Character;
