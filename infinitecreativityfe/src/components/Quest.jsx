import { Box, TableCell, TableRow } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import LinearProgress from "@mui/material/LinearProgress";
import { useState, useContext } from "react";
import { InventoryContext } from "./InventoryContextProvider";
import Item from "./Item";

function Quest(props) {
  const inventoryCTX = useContext(InventoryContext);

  const [isLoading, setIsLoading] = useState(false);
  async function makeProgress() {
    setIsLoading(true);
    const p = await fetch(
      `/api/quest/${props.quest.id}/${Math.round(Math.random() * 49) + 1}`,
      { method: "PUT" }
    );
    const newQuestState = await p.json();
    if (newQuestState.isDone) {
      inventoryCTX.refresh();
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
        <Box>
          {props.quest.rewards.map((x) => (
            <Item item={x}></Item>
          ))}
          GÓD: {props.quest.cashReward}
        </Box>
      </TableCell>
      <TableCell>
        <LoadingButton
          onClick={() => {
            makeProgress();
          }}
          loading={isLoading}
          disabled={props.quest.progression >= 100}
        >
          <Box>PROGRESS 1-50</Box>
        </LoadingButton>
      </TableCell>
    </TableRow>
  );
}

export default Quest;
