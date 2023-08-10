import { Box } from "@mui/material";
import { useContext } from "react";
import { UserContext } from "./UserContextProvider";
import Character from "./Character";
import CharacterCreation from "./CharacterCreation";
import { useMemo } from "react";

function Characters(props) {
  const userCTX = useContext(UserContext);

  let sortedCharacters = useMemo(()=>{
    return userCTX.user.characters.slice().sort((a,b)=>a.id-b.id);
  },[userCTX.user.characters]);

  if (!userCTX.user) {
    return null;
  }
  return (
    <Box display="flex" flexGrow="1">
      <Box
        sx={{
          display: "grid",
          gap: 4,
          gridTemplateColumns: "repeat(auto-fill, minmax(240px, 1fr))",
          flexGrow:1,
          gridAutoRows:"min-content"
        }}
      >
        {sortedCharacters.map((x) => (
          <Character character={x} key={x.id}></Character>
        ))}
        <CharacterCreation></CharacterCreation>
      </Box>
    </Box>
  );
}

export default Characters;
