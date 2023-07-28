import { Box, TableCell, TableRow } from "@mui/material";
import { LoadingButton } from "@mui/lab";
import LinearProgress from "@mui/material/LinearProgress";
import { useState } from "react";

function Quest(props) {
  const [isLoading, setIsLoading] = useState(false);
  async function makeProgress() {
    setIsLoading(true);
    const p = await fetch(
      `/api/quest/${props.quest.id}/${Math.round(Math.random() * 49) + 1}`,
      { method: "PUT" }
    );
    const newQuestState = await p.json();
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
        <LoadingButton
          onClick={() => {
            makeProgress();
          }}
          loading={isLoading}
          disabled={props.quest.progression >= 100}
        >
          <Box>
            PROGRESS 1-50
          </Box>
        </LoadingButton>
      </TableCell>
    </TableRow>
  );
}

export default Quest;
