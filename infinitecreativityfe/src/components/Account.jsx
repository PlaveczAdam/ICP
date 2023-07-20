import { Box } from "@mui/material";
import { useContext } from 'react';
import { UserContext } from "./UserContextProvider";
import Character from "./Character";
import CharacterCreation from "./CharacterCreation";

function Account(props){
    const userCTX = useContext(UserContext);
    if(!userCTX.user)
    {
        return null;
    }
    return (
        <Box>
            <Box>
                Characters
            </Box>
            <Box display="flex" gap={4} flexWrap="wrap">
                {userCTX.user.characters.map((x)=><Character character={x} key={x.id}></Character>)}
                <CharacterCreation></CharacterCreation>
            </Box>
        </Box>
    );
}

export default Account;