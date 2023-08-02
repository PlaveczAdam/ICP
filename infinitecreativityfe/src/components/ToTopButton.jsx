import { Box } from "@mui/material";
import up from "../img/up.png";
function TopButton(props) {
  const scrollUp = () => {
    let dialogContent = document.getElementById(props.toId);
    dialogContent.scrollIntoView({ behavior: "smooth" });
  };

  return (
    <>
      {<Box
        component="img"
        src={up}
        onClick={scrollUp}
        sx={{
          "&:hover": { cursor: "pointer" },
          right: "20px",
          bottom: "20px",
          borderRadius: "50%",
          border: "5px solid purple",
          backgroundColor: "teal",
          padding: "5px",
          position: "fixed",
          width: "50px",
          height: "50px"
        }}>
      </Box>}
    </>
  );
}
export default TopButton;