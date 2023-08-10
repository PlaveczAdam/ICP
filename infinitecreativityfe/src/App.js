import "./App.css";
import logo from "./img/logo.png";
import { BrowserRouter as Router, Routes, Route, Link } from "react-router-dom";
import { useContext, useState } from "react";
import Login from "./components/Login";
import Registration from "./components/Registration";
import Home from "./components/Home";
import Account from "./components/Account";
import Characters from "./components/Characters";
import { UserContext } from "./components/UserContextProvider";
import { Box, Button } from "@mui/material";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { purple, teal } from "@mui/material/colors";
import Inventory from "./components/Inventory";
import Market from "./components/Market";
import CreateListing from "./components/CreateListing";
import MyListings from "./components/MyListings";
import AllListings from "./components/AllListings";
import Messages from "./components/Messages";
import { ToastContainer } from "react-toastify";
import { useEffect } from "react";
import useNotification, { notificationTypes } from "./hooks/useNotification";

const theme = createTheme({
  palette: {
    primary: {
      light: teal[800],
      main: teal[800],
      dark: teal[70],
      contrastText: "#fff",
    },
    secondary: {
      light: purple[700],
      main: teal[400],
      dark: teal[700],
      contrastText: "#fafafa",
    },
    white: {
      main: "#fff",
    },
    disabled: {
      main: "rgba(166, 23, 202, 0.4)",
      text: "#fff",
    },
    activeButtonColor: {
      main: purple[700],
    },
    cancelButtonTextColor: {
      main: teal[400],
    },
  },
});

const globalTheme = createTheme(theme, {
  components: {
    MuiFormLabel: {
      styleOverrides: {
        root: {
          color: "white",
          "&.Mui-focused": { color: "white" },
        },
      },
    },
    MuiDialogContent: {
      styleOverrides: {
        root: {
          background: "#202020",
        },
      },
    },
    MuiDialogTitle: {
      styleOverrides: {
        root: {
          background: "rgba(0,105,93,1)",
          color: "white",
        },
      },
    },
    MuiDialogActions: {
      styleOverrides: {
        root: {
          background: "#202020",
        },
      },
    },
    MuiCard: {
      styleOverrides: {
        root: {
          background: "#101010",
          color: "#fafafa",
        },
      },
    },
    MuiOutlinedInput: {
      styleOverrides: {
        notchedOutline: {
          borderColor: "rgba(0,105,93,1)",
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          borderColor: "rgba(0,105,93,1)",
        },
      },
    },
    MuiInputBase: {
      defaultProps: {
        variant: "outlined",
      },
      styleOverrides: {
        root: {
          background: "black",
          borderColor: "teal",
          color: "white",
        },
      },
    },
    MuiLoadingButton: {
      defaultProps: {
        color: "primary",
        variant: "contained",
      },
    },
    MuiDialog: {
      styleOverrides: {
        paperFullScreen: {
          backgroundColor: "#202020",
          color: "white",
        },
      },
    },
    MuiTableCell: {
      styleOverrides: {
        root: {
          color: "#fafafa",
        },
      },
    },
    MuiButton: {
      defaultProps: {
        color: "primary",
        variant: "contained",
      },
      variants: [
        {
          props: { variant: "contained" },
          style: {
            border: "1px solid #101010",
            //borderRadius: '5px',
            margin: "5px",
            minWidth: "120px",
            backgroundColor: theme.palette.primary.light,
            color: theme.palette.primary.dark,
            boxShadow: "-3px 7px 9px -1px rgba(0,0,0,0.54)",
            //borderColor: theme.palette.primary.dark,
            "&:hover": {
              color: theme.palette.secondary.contrastText,
              backgroundColor: theme.palette.secondary.light,
              borderColor: theme.palette.secondary.main,
            },
            "&:disabled": {
              color: theme.palette.disabled.text,
              backgroundColor: theme.palette.disabled.main,
              borderColor: theme.palette.disabled.text,
              backgroundImage:
                "linear-gradient(45deg, purple 25%, #000000 25%, #000000 50%, purple 50%, purple 75%, #000000 75%, #000000 100%)",
              backgroundSize: "50px 50px",
            },
          },
        },
        {
          props: {
            variant: "contained",
            color: "activeButtonColor",
          },
          style: {
            backgroundColor: theme.palette.activeButtonColor.main,
          },
        },
      ],
    },
  },
});

function App() {
  const [activeButton, setActiveButton] = useState("home");
  const userCTX = useContext(UserContext);
  async function handleLogOut() {
    await fetch("/api/player/logout");
    setActiveButton("home");
    userCTX.refresh();
  }

  let notification = useNotification(notificationTypes.QuestUpdate)
  useEffect(()=>{
    userCTX.refresh();
  // eslint-disable-next-line react-hooks/exhaustive-deps
  },[notification]);

  return (
    <ThemeProvider theme={globalTheme}>
      <div className="App">
        <Box>
          <ToastContainer
            position="top-center"
            autoClose={2000}
            hideProgressBar={false}
            closeOnClick={true}
            pauseOnHover={true}
            draggable={true}
            theme="dark"
            style={{userSelect:"none"}}
          />
        </Box>
        <Box className="App-header">
          <Router basename="/">
            <Box
              sx={{
                display: "flex",
                flexGrow: "1",
                width: "100%",
                justifyContent: "center",
              }}
            >
              <Box
                sx={{
                  display: "flex",
                  position: "fixed",
                  alignItems: "center",
                  maxHeight: "70px",
                  width: "100%",
                  backgroundColor: "rgba(0,105,93,0.4)",
                  backdropFilter: "blur(7px)",
                  zIndex: 1000,
                  className: "navbarBox",
                  boxShadow: "2px 6px 11px 2px #101010",
                }}
              >
                <Box sx={{ flexShrink: "1" }}>
                  <Link to="/">
                    <img src={logo} alt="logo" width="70" height="70" />
                  </Link>
                </Box>
                <Box
                  sx={{
                    paddingLeft: "20px",
                    display: "flex",
                    flexGrow: "1",
                    alignItems: "flex-start",
                  }}
                >
                  <Button
                    color={
                      activeButton === "home" ? "activeButtonColor" : "primary"
                    }
                    component={Link}
                    to="/"
                    onClick={() => {
                      setActiveButton("home");
                    }}
                  >
                    Home
                  </Button>
                  {userCTX.user && (
                    <Button
                      color={
                        activeButton === "account"
                          ? "activeButtonColor"
                          : "primary"
                      }
                      component={Link}
                      to="/account"
                      onClick={() => {
                        setActiveButton("account");
                      }}
                    >
                      Account
                    </Button>
                  )}
                  {userCTX.user && (
                    <Button
                      color={
                        activeButton === "market"
                          ? "activeButtonColor"
                          : "primary"
                      }
                      component={Link}
                      to="/market/allListings"
                      onClick={() => {
                        setActiveButton("market");
                      }}
                    >
                      Market
                    </Button>
                  )}
                </Box>
                <Box sx={{ flexShrink: "1" }}>
                  {userCTX.user && (
                    <Box
                      sx={{
                        display: "flex",
                        flexWrap: "wrap",
                        alignItems: "center",
                      }}
                    >
                      <Box sx={{ paddingRight: "10px" }}>
                        {userCTX.user.name}
                      </Box>
                      <Button
                        component={Link}
                        to="/"
                        onClick={() => handleLogOut()}
                      >
                        Log Out
                      </Button>
                    </Box>
                  )}
                  {!userCTX.user && (
                    <Button component={Link} to="/login">
                      Login
                    </Button>
                  )}
                  {!userCTX.user && (
                    <Button component={Link} to="/registration">
                      Registration
                    </Button>
                  )}
                </Box>
              </Box>
              <Box
                sx={{
                  paddingTop: "70px",
                  className: "contentBox",
                  display: "flex",
                  flexDirection: "row",
                  paddingLeft: "300px",
                  width: "100vw",
                  height: "100vh",
                  overflow: "hidden",
                  minHeight: 0,
                  boxSizing: "border-box",
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    flexDirection: "column",
                    boxShadow: "9px 1px 21px 1px rgba(0,0,0,0.69);",
                    paddingTop: "20px",
                    backdropFilter: "blur(7px)",
                    background: "rgba(0,105,93,0.4)",
                    whiteSpace: "nowrap",
                    position: "fixed",
                    height: "100vh",
                    width: "300px",
                    left: 0,
                  }}
                >
                  <Box id="sideBarContent"></Box>
                </Box>
                <Box
                  display="flex"
                  flexGrow="1"
                  padding="16px"
                  overflow="auto"
                  minHeight="0"
                  justifyContent="center"
                >
                  <Routes>
                    <Route
                      path="/registration"
                      element={<Registration />}
                    ></Route>
                    <Route path="/market" element={<Market></Market>}>
                      <Route
                        path="createListing"
                        element={<CreateListing></CreateListing>}
                      ></Route>
                      <Route
                        path="allListings"
                        element={<AllListings></AllListings>}
                      ></Route>
                      <Route
                        path="myListings"
                        element={<MyListings></MyListings>}
                      ></Route>
                    </Route>
                    <Route path="/account" element={<Account />}>
                      <Route path="characters" element={<Characters />}></Route>
                      <Route path="inventory" element={<Inventory />}></Route>
                    </Route>
                    <Route path="/login" element={<Login />}></Route>
                    <Route path="/" element={<Home />}></Route>
                  </Routes>
                </Box>
              </Box>
            </Box>
          </Router>
          <Messages></Messages>
        </Box>
      </div>
    </ThemeProvider>
  );
}

export default App;
