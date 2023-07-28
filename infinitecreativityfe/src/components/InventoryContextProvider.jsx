import { createContext, useEffect, useState } from "react";

export const InventoryContext = createContext({inventory:null, refresh(){}, isLoading:true});

function InventoryContextProvider(props){
    const [inventory, setInventory] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(()=>{fetchInventory()},[]);
    
    async function fetchInventory(){
        const currentPlayersInventory = await fetch("/api/item");
        if(currentPlayersInventory.ok)
        {
            setInventory(await currentPlayersInventory.json());
        }else
        {
            setInventory([]);
        }
        setIsLoading(false);
    };

    const value = {inventory, isLoading, refresh(){fetchInventory()}};

    return (
        <InventoryContext.Provider value={value}>
            {props.children}
        </InventoryContext.Provider>
    );
}

export default InventoryContextProvider;