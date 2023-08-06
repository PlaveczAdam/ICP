import { useContext } from "react";
import { NotificationContext } from "../components/NotificationContextProvider";

export const notificationTypes = {Message:"Message"};

export default function useNotification(type){
    const context = useContext(NotificationContext);
    return context.lastNotificationDates[type];
}