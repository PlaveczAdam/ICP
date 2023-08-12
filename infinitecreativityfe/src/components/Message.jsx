import { Box } from "@mui/material";
import { DateTime } from "luxon";

function Message(props)
{
    return <Box textAlign="left">
        <span style={{color: "teal"}}>[{DateTime.fromISO(props.message.sendDate).toFormat("HH:mm")}]</span> <strong style={{color: "teal"}}>{props.message.sender.name}</strong>: {props.message.messageBody}
    </Box>
}

export default Message;