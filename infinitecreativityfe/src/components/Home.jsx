import { Box, Button, Portal } from "@mui/material";

function Home(props) {
  return (
    <Box>
      <Portal container={() => document.getElementById("sideBarContent")}>
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            height: "100%",
            width: "100%",
            alignItems: "center",
          }}
        >
          Home
        </Box>
      </Portal>
      <Box sx={{ fontSize: "xx-large", typography: "body1" }}>
        Welcome to Infinite Creativity
      </Box>
      <Box
        sx={{
          backgroundColor: "rgb(10, 10, 10, 0.8)",
          typography: "body",
          textAlign: "left",
          padding: "30px",
          color: "white",
          fontWeight: "600",
          fontSize: "xlarge",
        }}
      >
        <p>Welcome to Infinite Creativity - Your Ultimate Character Management App!</p>
        Step into a realm of limitless possibilities with Infinite Creativity,
        your virtual haven for managing characters like never before. 
        Designed to seamlessly blend convenience and innovation, Infinite Creativity
        empowers players to take control of their in-game personas with ease,
        eliminating the need to start the game every time you wish to make
        updates.
      </Box>
    </Box>
  );
}

export default Home;
