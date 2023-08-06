import { createContext, useContext, useEffect, useState } from "react";
import { UserContext } from "./UserContextProvider";
import * as signalR from "@microsoft/signalr";

export const NotificationContext = createContext({
  lastNotificationDates: {},
});

function NotificationContextProvider(props) {
  const [lastNotificationDates, setLastNotificationDates] = useState({});
  const userCTX = useContext(UserContext);

  useEffect(() => {
    if (!userCTX.user /* || true */) {
      return;
    }
    console.log("EKEZDTE");
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("/notification")
      .configureLogging(signalR.LogLevel.Trace)
      .build();

    let timeOutRef = null;
    async function start() {
      try {
        await connection.start();
        console.log("SignalR Connected.");
      } catch (err) {
        console.log(err);
        timeOutRef = setTimeout(start, 5000);
      }
    }

    connection.onclose(async (error) => {
      if (error) {
        console.log("oncloseError, ", error);
        await start();
      }
    });
    connection.on("Notification", (notificationType) => {
      setLastNotificationDates((old) => {
        return { ...old, [notificationType]: Date.now() };
      });
    });
    // Start the connection.
    start();
    return () => {
      if (timeOutRef) {
        clearTimeout(timeOutRef);
      }
      connection.stop();
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userCTX.user?.id]);

  const value = {
    lastNotificationDates,
  };
  useEffect(()=>{console.log("lnfd: ",lastNotificationDates)},[lastNotificationDates]);
  return (
    <NotificationContext.Provider value={value}>
      {props.children}
    </NotificationContext.Provider>
  );
}

export default NotificationContextProvider;
