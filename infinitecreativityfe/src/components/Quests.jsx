import {
  Button,
  Box,
  Table,
  TableRow,
  TableHead,
  TableCell,
  TableBody,
} from "@mui/material";
import { useState, forwardRef, useMemo } from "react";
import Dialog from "@mui/material/Dialog";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import CloseIcon from "@mui/icons-material/Close";
import Slide from "@mui/material/Slide";
import LinearProgress from "@mui/material/LinearProgress";
import Quest from "./Quest";
import ScrollTop from "./ToTopButton";


const Transition = forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function Quests(props) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [quests, setQuests] = useState();
  async function getQuests() {
    const res = await fetch(`/api/quest/${props.characterID}`);
    const qs = await res.json();
    setQuests(qs);
  }
  function handleQuestChange(newQuestState) {
    const newQuests = [...quests];
    const idx = newQuests.findIndex((x) => x.id === newQuestState.id);
    newQuests[idx] = newQuestState;
    setQuests(newQuests);
  }
/* 
  function rearrangeQuests(qss){
    let done = qss.filter((x) => x.isDone);
    let notDone = qss.filter((x) => !x.isDone);
    return [...notDone, ...done]
  } */

  function rearrangeQuests(qss){
    return qss.sort((a,b) => a.isDone-b.isDone);
  }
  const orderedQuests = useMemo(()=>{
    if(!quests)
    {
      return undefined;
    }
    const nq = [...quests];
    return rearrangeQuests(nq);
  },[quests])

  async function takeQuest() {
    const res = await fetch(`/api/quest/${props.characterID}`, {
      method: "POST",
    });
    const newQuest = await res.json();
    const newQuests = [...quests, newQuest];
    setQuests(newQuests);
  }
  return (
    <Box>
      <Button
        type="btn"
        onClick={() => {
          getQuests();
          setIsModalOpen(true);
        }}
      >
        Quests
      </Button>
      <Dialog
        fullScreen
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        TransitionComponent={Transition}
      >
        <AppBar id="dialogContent" sx={{ position: "relative" }}>
          <Toolbar>
            <IconButton
              edge="start"
              color="inherit"
              onClick={() => setIsModalOpen(false)}
              aria-label="close"
            >
              <CloseIcon />
            </IconButton>
            <Typography sx={{ ml: 2, flex: 1 }} variant="h6" component="div">
              Quests
            </Typography>
            <Button onClick={() => takeQuest()} sx={{ boxShadow: "-3px 7px 9px -1px rgba(0,0,0,0.54)" }}>
              Take Quest
            </Button>
          </Toolbar>
        </AppBar>

        {quests ? (
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Name</TableCell>
                <TableCell>Description</TableCell>
                <TableCell>Progress</TableCell>
                <TableCell>Reward</TableCell>
                <TableCell></TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {orderedQuests.map((x) => (
                <Quest
                  quest={x}
                  onQuestChange={handleQuestChange}
                  key={x.id}
                ></Quest>
              ))}
            </TableBody>
          </Table>
        ) : (
          <LinearProgress />
        )}
        <ScrollTop toId="dialogContent"></ScrollTop>
      </Dialog>
    </Box>
  );
}

export default Quests;
