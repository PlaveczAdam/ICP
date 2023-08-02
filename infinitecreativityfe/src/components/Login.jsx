import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import { Link } from "react-router-dom";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import { CircularProgress } from "@mui/material";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { UserContext } from './UserContextProvider';
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
function Login(props) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [isFailed, setIsFailed] = useState(false);
  const navigate = useNavigate();
  const userCTX = useContext(UserContext);
  
  const handleSubmit = async (event) => {
    event.preventDefault();
    setIsLoading(true);
    setIsFailed(false);
    const res = await fetch("/api/player/login", {
      method: "Post",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ name: username, password: password }),
    });
    if (res.ok) {
      setIsLoading(false);
      navigate("/");
      userCTX.refresh();
      return;
    }
    notify();
    setIsFailed(true);
    setIsLoading(false);
    setTimeout(() => {
      setIsFailed(false);
    }, 2770)
    console.log(await res.json());
  };

  const notify = () => {
    toast.error("Invalid Username or Password!", {
      position: "top-center",
      autoClose: 2000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
      theme: "dark",
    });
  };

  return (
    <Container component="main" width="100%">
      <Box
        sx={{
          boxShadow: 3,
          borderRadius: 2,
          px: 4,
          py: 6,
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          bgcolor: 'rgba(10, 10, 10, 0.85)',

        }}
      >
        <Box>
          <ToastContainer position="absolute" />
        </Box>
        <Typography component="h1" variant="h5">
          Sign In
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1, }}>
          <TextField
            error={isFailed}
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            autoComplete="email"
            autoFocus
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <TextField
            error={isFailed}
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            {isLoading ? <CircularProgress color="inherit" style={{ height: "24px", width: "24px" }} /> : "Sign In"}
          </Button>
          <Grid container>
            <Grid item>
              <Link to="/registration">
                <Typography style={{ color: '#f149f5' }} variant="body2">
                  {"Don't have an account? Sign Up"}
                </Typography>
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}

export default Login;
