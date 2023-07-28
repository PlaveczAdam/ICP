import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import {Link} from "react-router-dom";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useState, useContext } from "react";
import { useNavigate } from "react-router-dom";
import { UserContext } from './UserContextProvider';

function Login(props) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();
    const userCTX = useContext(UserContext);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const res = await fetch("/api/player/login", {
            method:"Post",
            headers:{"Content-Type":"application/json"},
            body:JSON.stringify({name:username, password:password}),
        });
        if(res.ok)
        {
          navigate("/");
          userCTX.refresh();
          return;
        }
        alert("Invalid username or Password.");
        console.log(await res.json());
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
            <Typography component="h1" variant="h5">
              Sign in
            </Typography>
            <Box component="form"  onSubmit={handleSubmit} noValidate sx={{ mt: 1, }}>
              <TextField
                margin="normal"
                required
                fullWidth
                id="username"
                label="Username"
                name="username"
                autoComplete="email"
                autoFocus
                value={username}
                onChange={(e)=>setUsername(e.target.value)}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
                value={password}
                onChange={(e)=>setPassword(e.target.value)}
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid item>
                  <Link to="/registration">
                    <Typography style={{color: '#f149f5'}} variant="body2">
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
