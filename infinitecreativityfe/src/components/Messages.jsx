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
import { useLayoutEffect } from "react";
import KeyboardDoubleArrowDownIcon from '@mui/icons-material/KeyboardDoubleArrowDown';
import { DateTime } from "luxon";


function Messages(props) {
  const [messages, setMessages] = useState([]);
  const [recipient, setRecipient] = useState("");
  const [isOpen, setIsOpen] = useState(false);
  const [body, setBody] = useState("");
  const [offsetY, setOffsetY] = useState(0);
  const [scrollRef, setScrollRef] = useState(null);
  const userCTX = useContext(UserContext);
  const lastUpdate = useNotification(notificationTypes.Message);

  const scrollDown = () => {
    let dialogContent = document.getElementById("lastMessage");
    if (!dialogContent) {
      return;
    }
    dialogContent.scrollIntoView({ behavior: "smooth" });
  };

  async function getMessages() {
    const res = await fetch("api/messages");
    const msgs = await res.json();
    setMessages(msgs);
    props.onDataPassed(msgs);
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

  function calculateOffset(element) {
    setOffsetY(element.scrollHeight - element.clientHeight - element.scrollTop);
  }
  useLayoutEffect(() => {
    if (scrollRef === null) {
      return;
    }
    function scrollHandler(e) {
      calculateOffset(e.target);
    }
    scrollRef.addEventListener("scroll", scrollHandler, { passive: true });
    return () => {
      scrollRef.removeEventListener("scroll", scrollHandler, { passive: true });
    };
  }, [scrollRef]);

  useLayoutEffect(() => {
    if (offsetY > 99) {
      return;
    }
    window.requestAnimationFrame(scrollDown);
  }, [messages]);
  if (!userCTX.user) {
    return null;
  }

  return (
    <ClickAwayListener
      onClickAway={() => {
        if (props.isOpen !== isOpen) {
          setIsOpen(props.isOpen);
          props.setChecked(props.isOpen);
        }
        else if(props.isOpen === true){
          props.setIsOpen(false);
          props.setChecked(false);
          setIsOpen(false);
          localStorage.setItem(`${userCTX.user.name}Close`, DateTime.utc().toISO());
        }
      }}
      //mouseEvent="onMouseDown"
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
        bgcolor="rgba(20, 20, 20, 0.9)"
      >
        <Box
          bgcolor="#000"
          onClick={(x) => {
            if (props.isOpen !== isOpen) setIsOpen(props.isOpen);
            else {
              props.setIsOpen(true);
              setIsOpen(true);
              localStorage.setItem(`${userCTX.user.name}Open`, DateTime.utc().toISO());
            }
            props.setMsgNumber(0);
            props.setChecked(true);
          }}
          sx={{
            "&:hover": { cursor: "pointer" },
          }}
        >
          Chat
        </Box>
        <Collapse
          in={props.isOpen && isOpen}
          /* timeout={600} */
          onEntered={() => {
            window.requestAnimationFrame(() => scrollDown());
          }}
        >
          <Box
            height={500}
            overflow="auto"
            position="relative"
            ref={setScrollRef}
          >
            {messages?.map((x) => (
              <Message message={x} key={`${x.fromInbox}_${x.id}`}></Message>
            ))}
            <Collapse in={offsetY > 100} sx={{ position: "sticky", bottom: 0}}>
              <Box justifyContent={"space-evenly"} alignItems={"center"} display="flex" sx={{ borderRadius: "10px 10px 0px 0px", background: "#303030", "&:hover": {cursor: "pointer"}}} onClick={scrollDown} bgcolor="black">
                <KeyboardDoubleArrowDownIcon/>
                <Box sx={{fontSize: "small"}}>Jump to latest</Box>
                <KeyboardDoubleArrowDownIcon/>
              </Box>
            </Collapse>
            <Box id="lastMessage"></Box>
          </Box>
          <Box>
            <TextField
              value={recipient}
              placeholder="target"
              onChange={(e) => {
                setRecipient(e.target.value);
              }}
            ></TextField>
            <TextField
            placeholder="message"
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
