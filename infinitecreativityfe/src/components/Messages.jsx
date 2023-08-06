import {
  Box,
  Button,
  ClickAwayListener,
  Collapse,
  TextField,
} from "@mui/material";
import { useContext, useEffect, useState } from "react";
import Message from "./Message";
import { UserContext } from "./UserContextProvider";
import useNotification, { notificationTypes } from "../hooks/useNotification";
import { toast } from "react-toastify";

function Messages(props) {
  const [messages, setMessages] = useState([]);
  const [recipient, setRecipient] = useState("");
  const [isOpen, setIsOpen] = useState(false);
  const [body, setBody] = useState("");
  const userCTX = useContext(UserContext);
  const lastUpdate = useNotification(notificationTypes.Message);

  async function getMessages() {
    const res = await fetch("api/messages");
    const msgs = await res.json();
    setMessages(msgs);
  }

  async function sendMessage() {
    const res = await fetch("api/messages", {
      method: "POST",
      body: JSON.stringify({ recipientName: recipient, messageBody: body }),
      headers: { "Content-Type": "application/json" },
    });
    if (!res.ok) {
      toast.error("Invalid target.");
      return;
    }
    setBody("");
    getMessages();
  }

  useEffect(() => {
    if (!userCTX.user) {
      return;
    }
    getMessages();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userCTX.user?.id, lastUpdate]);

  if (!userCTX.user) {
    return null;
  }

  return (
    <ClickAwayListener
      onClickAway={() => {
        setIsOpen(false);
      }}
    >
      <Box
        component="form"
        onSubmit={(e) => {
          e.preventDefault();
          sendMessage();
        }}
        position="fixed"
        bottom={0}
        left={0}
        width={500}
        bgcolor="#000"
      >
        <Box
          bgcolor="#000"
          onClick={(x) => {
            setIsOpen(true);
          }}
        >
          Chat
        </Box>
        <Collapse in={isOpen}>
          <Box height={500} overflow="auto">
            {messages.map((x) => (
              <Message message={x} key={`${x.fromInbox}_${x.id}`}></Message>
            ))}
          </Box>
          <Box>
            <TextField
              value={recipient}
              onChange={(e) => {
                setRecipient(e.target.value);
              }}
            ></TextField>
            <TextField
              value={body}
              onChange={(e) => {
                setBody(e.target.value);
              }}
            ></TextField>
          </Box>
          <Button type="submit">Send</Button>
        </Collapse>
      </Box>
    </ClickAwayListener>
  );
}

export default Messages;
