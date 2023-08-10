import { Box, TableCell, TableRow } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import LinearProgress from "@mui/material/LinearProgress";
import { useState, useContext } from "react";
import { InventoryContext } from "./InventoryContextProvider";
import Item from "./Item";
import {UserContext} from "./UserContextProvider";

function Quest(props) {
  const inventoryCTX = useContext(InventoryContext);
  const userCTX = useContext(UserContext);

  const [isLoading, setIsLoading] = useState(false);
  async function makeProgress() {
    setIsLoading(true);
    const p = await fetch(
      `/api/quest/${props.quest.id}/${100}`,
      { method: "PUT" }
    );
    const newQuestState = await p.json();
    if (newQuestState.isDone) {
      inventoryCTX.refresh();
      userCTX.refresh();
    }
    props.onQuestChange(newQuestState);
    setIsLoading(false);
  }
  return (
    <TableRow>
      <TableCell>{props.quest.name}</TableCell>
      <TableCell>{props.quest.description}</TableCell>
      <TableCell>
        <Box width={100}>
          <LinearProgress
            variant="determinate"
            value={props.quest.progression}
          />
        </Box>
        <Box minWidth={60}>{props.quest.progression}%</Box>
      </TableCell>
      <TableCell>
        {!props.quest.isDone?<Box>
          <Box>
            {props.quest.rewards.map((x) => (
              <Item item={x} key={x.id}></Item>
            ))}
          </Box>

          <Box>GÓD: {props.quest.cashReward}</Box>
          <Box>XP: {props.quest.levelReward.toFixed(2)}</Box>
        </Box>:<Box>Claimed</Box>}
      </TableCell>
      <TableCell>
        <LoadingButton
          onClick={() => {
            makeProgress();
          }}
          loading={isLoading}
          disabled={props.quest.progression >= 100}
        >
          <Box>Complete Quest</Box>
        </LoadingButton>
      </TableCell>
    </TableRow>
  );
}

export default Quest;
