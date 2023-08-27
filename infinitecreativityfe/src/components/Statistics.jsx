import {
  Box,
  Button,
  Slide,
  Dialog,
  Toolbar,
  AppBar,
  IconButton,
  Typography,
  LinearProgress,
} from "@mui/material";
import { Radar } from "react-chartjs-2";
import CloseIcon from "@mui/icons-material/Close";
import { forwardRef, useState } from "react";
import DoubleDisplay from "./DoubleDisplay";

const Transition = forwardRef(function Transition(props, ref) {
  return <Slide direction="up" ref={ref} {...props} />;
});

function Statistics(props) {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [stats, setStats] = useState();

  async function getStats() {
    const res = await fetch(`/api/character/${props.characterID}`);
    let sts = await res.json();
    setStats(sts);
  }
  const data = {
    labels: ["Health(Normalized)", "Defense", "Damage", "Intelligent Damage"],
    datasets: [
      {
        label: "Stats",
        data: [stats?.health/100??0, stats?.defense??0, stats?.damage??0, stats?.abilityDamage??0],
        backgroundColor: "rgba(255, 99, 132, 0.2)",
        borderColor: "rgba(255, 99, 132, 1)",
        borderWidth: 1,
      },
    ],
  };
  const options = {
    plugins:{
      legend:{
        display:false
      }
    },
    backgroundColor: "rgba(255, 255, 255, 0.2)",
    elements:{
      line:{
        borderColor:"rgba(255, 255, 255, 1)",
      }
    },
    scales:{
      r:{
        backgroundColor:"rgba(0, 0, 0, 1)",
        grid:{
          color:"rgba(255, 255, 255, 1)"
        },
        angleLines:
        {
          color:"rgba(255, 255, 255, 1)"
        }
      }
    }
  };
  return (
    <Box>
      <Button
        type="btn"
        onClick={() => {
          getStats();
          setIsModalOpen(true);
        }}
      >
        Stats
      </Button>
      <Dialog
        fullScreen
        open={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        TransitionComponent={Transition}
      >
        <AppBar sx={{ position: "relative" }}>
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
              Stats
            </Typography>
          </Toolbar>
        </AppBar>
        {stats ? (
          <Box display="flex" alignItems="center">
            <Box display="flex" alignItems="flex-start" flexDirection="column" flexGrow={1} gap={4}>
              <Box>Health:<DoubleDisplay value={stats.health}></DoubleDisplay></Box>
              <Box>Defense:<DoubleDisplay value={stats.defense}></DoubleDisplay> </Box>
              <Box>Damage:<DoubleDisplay value={stats.damage}></DoubleDisplay> </Box>
              <Box>Intelligent Damage:<DoubleDisplay value={stats.abilityDamage}></DoubleDisplay> </Box>
              <Box>Intelligent Juice:<DoubleDisplay value={stats.abilityResource}></DoubleDisplay> </Box>
              <Box>Intelligente Juicer:<DoubleDisplay value={stats.abilityResourceGain}></DoubleDisplay> </Box>
              <Box>Boot: {stats.movement}</Box>
            </Box>
            <Box height={700} width={700}>
              <Radar data={data} options={options}/>
            </Box>
          </Box>
        ) : (
          <LinearProgress />
        )}
      </Dialog>
    </Box>
  );
}

export default Statistics;
