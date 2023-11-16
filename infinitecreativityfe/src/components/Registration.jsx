import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import InputAdornment from '@mui/material/InputAdornment';
import IconButton from '@mui/material/IconButton';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';
import { CircularProgress } from "@mui/material";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function Registration(props) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [isFailed, setIsFailed] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const navigate = useNavigate();

  const handleClickShowPassword = () => setShowPassword((show) => !show);

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (username === '' || password === '') {
      toast.error("Missing Username or Password!", {
        position: "top-center",
        autoClose: 2000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "dark",
      });
      setIsFailed(true);
      setTimeout(() => {
        setIsFailed(false);
      }, 2770)
    }
    else {
      setIsLoading(true);
      const res = await fetch("/api/player", {
        method: "Post",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: username, password: password }),
      });
      if (res.ok) {
        setIsFailed(false);
        setIsLoading(false);
        navigate("/");
        return;
      }
      notify();
      setIsFailed(true);
      setIsLoading(false);
      setTimeout(() => {
        setIsFailed(false);
      }, 2770)
      console.log(await res.json());
    }
  };

  const notify = () => {
    toast.error("Username already exists!", {
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
          <ToastContainer limit={3} />
        </Box>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
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
            type={showPassword ? 'text' : 'password'}
            InputProps={{
              endAdornment: <InputAdornment position="end">
                <IconButton
                  aria-label="toggle password visibility"
                  onClick={handleClickShowPassword}
                  onMouseDown={handleMouseDownPassword}
                  edge={"end"}
                >
                  {showPassword ? <VisibilityOff sx={{color: "rgba(0,105,93,1)"}}/> : <Visibility sx={{color: "rgba(0,105,93,1)"}}/>}
                </IconButton>
              </InputAdornment>
            }}
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
            {isLoading ? <CircularProgress color="inherit" style={{ height: "24px", width: "24px" }} /> : "Create Account"}
          </Button>
        </Box>
      </Box>
    </Container>
  );
}

export default Registration;