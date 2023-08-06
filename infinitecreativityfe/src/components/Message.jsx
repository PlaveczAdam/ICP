import { Box } from "@mui/material";
import { DateTime } from "luxon";

function Message(props)
{
    return <Box textAlign="left">
        [{DateTime.fromISO(props.message.sendDate).toFormat("HH:mm")}] {props.message.sender.name} : {props.message.messageBody}
    </Box>
}

export default Message;