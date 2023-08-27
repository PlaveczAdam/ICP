import {
  Button,
  Box,
  Table,
  TableRow,
  TableHead,
  TableCell,
  TableBody,
} from "@mui/material";
import { useState, forwardRef, useMemo, useContext, useEffect } from "react";
import Dialog from "@mui/material/Dialog";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import CloseIcon from "@mui/icons-material/Close";
import Slide from "@mui/material/Slide";
import LinearProgress from "@mui/material/LinearProgress";
import ScrollTop from "./ToTopButton";
import { UserContext } from "./UserContextProvider";
import useNotification, { notificationTypes } from "../hooks/useNotification";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { DataGrid } from "@mui/x-data-grid";
import Item from "./Item";
import DoubleDisplay from "./DoubleDisplay";

const Transition = forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function Quests(props) {
  const userCTX = useContext(UserContext);
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

  function rearrangeQuests(qss) {
    return qss.sort((a, b) => a.isDone - b.isDone);
  }
  const orderedQuests = useMemo(() => {
    if (!quests) {
      return undefined;
    }
    const nq = [...quests];
    return rearrangeQuests(nq);
  }, [quests]);

  async function takeQuest() {
    const res = await fetch(`/api/quest/${props.characterID}`, {
      method: "POST",
    });

    if (!res.ok) {
      //toast later
      notify();
      getQuests();
      return;
    }

    const newQuest = await res.json();
    const newQuests = [...quests, newQuest];
    setQuests(newQuests);
  }
  const questUpdate = useNotification(notificationTypes.QuestUpdate);

  const notify = () => {
    toast.error("Unable to take quests at the time. Please try again later!", {
      position: "top-center",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });
  };

  useEffect(() => {
    if (!isModalOpen) {
      return;
    }
    getQuests();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isModalOpen, questUpdate]);

  const columns = useMemo(() => {
    return [
      { field: "name", headerName: "Name", flex:1 },
      { field: "description", headerName: "Description", flex:1 },
      {
        field: "progression",
        headerName: "Progression",
        width: 200,
        renderCell: (params) => (
          <Box display="flex" alignItems="center"minHeight={50}>
            <Box width={100} >
              <LinearProgress variant="determinate" value={params.value} />
            </Box>
            <Box minWidth={60}><DoubleDisplay value={params.value}></DoubleDisplay>%</Box>
          </Box>
        ),
      },
      {
        field: "reward",
        headerName: "Reward",
        flex:1,
        valueGetter: (params) => {
          return {
            isDone: params.row.isDone,
            levelReward: params.row.levelReward,
            cashReward: params.row.cashReward,
            rewards: params.row.rewards,
          };
        },
        renderCell: (params) => (
          <>
            {!params.value.isDone ? (
              <Box>
                <Box>
                  {params.value.rewards.map((x) => (
                    <Item item={x} key={x.id}></Item>
                  ))}
                </Box>

                <Box>GÓD: {params.value.cashReward}</Box>
                <Box>XP: {params.value.levelReward.toFixed(2)}</Box>
              </Box>
            ) : (
              <Box>Claimed</Box>
            )}
          </>
        ),
      },
    ];
  }, []);
  return (
    <Box>
      <Button
        type="btn"
        onClick={() => {
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
            <Button
              onClick={() => takeQuest()}
              sx={{ boxShadow: "-3px 7px 9px -1px rgba(0,0,0,0.54)" }}
              disabled={
                quests?.filter((x) => !x.isDone).length >=
                userCTX.user.questSlot
              }
            >
              Take Quest
            </Button>
          </Toolbar>
        </AppBar>

        {quests ? (
          <DataGrid rows={orderedQuests} columns={columns} getRowHeight={() => 'auto'}/>
        ) : (
          <LinearProgress />
        )}
        {/* <ScrollTop toId="dialogContent"></ScrollTop> */}
      </Dialog>
    </Box>
  );
}

export default Quests;
