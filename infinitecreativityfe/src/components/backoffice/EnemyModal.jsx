import {
    Box,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    TextField,
    DialogActions,
    InputLabel,
    FormControl,
    Select,
    MenuItem,
    FormControlLabel,
    Checkbox
} from "@mui/material";
import { useState, useContext, useEffect } from "react";
import { ToSentenceCase } from "./../../utils/StringUtils";

const enemyTypes = ["bean", "trunk"];

function EnemyModal(props) {
    const [enemyState, setEnemyState] = useState({isBoss:false});
    useEffect(() => {
        setEnemyState({isBoss:false});
    }, [props.isOpen]);
    return (
    
        <Dialog
          open={props.isOpen}
          onClose={() => props.onClose()}
          fullWidth
          maxWidth="md"
        >
          <DialogTitle>Submit Enemy</DialogTitle>
          <DialogContent sx={{display:"flex", gap:4, flexDirection:"column"}}>
            <DialogContentText style={{ color: "white" }}>
            </DialogContentText>
            <FormControl fullWidth>
              <InputLabel id="typeSelect">Type</InputLabel>
              <Select
                labelId="typeSelect"
                value={enemyState.type}
                label="Type"
                onChange={(e)=>setEnemyState({...enemyState, type:e.target.value})}
              >
                {enemyTypes.map(x=><MenuItem value={x}>{ToSentenceCase(x)}</MenuItem>)}
              </Select>
            </FormControl>
            <FormControlLabel control={<Checkbox value={enemyState.isBoss} onChange={(e)=>setEnemyState({...enemyState, isBoss:e.target.value})} />} label="isBoss"/>
          </DialogContent>
          <DialogActions>
            <Button
              variant="text"
              onClick={() => props.onClose()}
              color="cancelButtonTextColor"
            >
              Cancel
            </Button>
            <Button
              onClick={() => props.onClose(enemyState)}
              disabled={ !enemyState.type }
            >
              Save
            </Button>
          </DialogActions>
        </Dialog>
    );
}

export default EnemyModal;