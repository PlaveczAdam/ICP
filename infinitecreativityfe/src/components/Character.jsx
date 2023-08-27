import { Box, Card, Grow, Slide } from "@mui/material";
import Quests from "./Quests";
import Equipment from './Equipment';
import DoubleDisplay from './DoubleDisplay';
import Statistics from "./Statistics";

function Character(props) {
  return (
    <Grow in appear>
    <Card
      variant="outlined"
      sx={{border: '3px solid rgba(7,163,173,0.33)'}}
    >
      <Box padding={4} minHeight={110}>
        <Box>Name: {props.character.name}</Box>
        <Box>Profession: {props.character.profession}</Box>
        <Box>Level: <DoubleDisplay value={props.character.level} precision={2}></DoubleDisplay></Box>
        <Quests characterID={props.character.id}></Quests>
        <Equipment characterID={props.character.id}></Equipment>
        <Statistics characterID={props.character.id}></Statistics>
      </Box>
    </Card>
    </Grow>
  );
}

export default Character;
