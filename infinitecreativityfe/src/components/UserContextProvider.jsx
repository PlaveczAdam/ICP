import { createContext, useEffect, useState } from "react";

export const UserContext = createContext({user:null, refresh(){}, isLoading:true});

function UserContextProvider(props){
    const [user, setUser] = useState();
    const [isLoading, setIsLoading] = useState(true);

    useEffect(()=>{fetchUser()},[]);
    
    async function fetchUser(){
        const currentPlayer = await fetch("/api/player/current");
        if(currentPlayer.ok)
        {
            setUser(await currentPlayer.json());
        }
        setIsLoading(false);
    };

    const value = {user, isLoading, refresh(){fetchUser()}};

    return (
        <UserContext.Provider value={value}>
            {props.children}
        </UserContext.Provider>
    );
}

export default UserContextProvider;