import { Box, Card, Grow, Slide } from "@mui/material";
import Quests from "./Quests";
import Equipment from './Equipment';
import DoubleDisplay from './DoubleDisplay';

function Character(props) {
  return (
    <Grow in appear>
    <Card
      variant="outlined"
      sx={{border: '3px solid rgba(7,163,173,0.33)'}}
    >
      <Box padding={4} minHeight={110}>
        <Box>Name: {props.character.name}</Box>
        <Box>Health: <DoubleDisplay value={props.character.health} precision={0}></DoubleDisplay></Box>
        <Box>Level: <DoubleDisplay value={props.character.level} precision={2}></DoubleDisplay></Box>
        <Quests characterID={props.character.id}></Quests>
        <Equipment characterID={props.character.id}></Equipment>
      </Box>
    </Card>
    </Grow>
  );
}

export default Character;
