import { Box } from "@mui/material";
import Listing from './Listing';
import { useContext, useEffect, useState } from 'react';
import { UserContext } from "./UserContextProvider";

function AllListings(props)
{
    const [listings, setListings] = useState([]);
    const userCTX = useContext(UserContext);

    async function getListings() {
        const queryParams = new URLSearchParams({notSellerId:userCTX.user.id}).toString();
        const res = await fetch(`/api/listing?${queryParams}`);
        const l = await res.json();
        setListings(l);
    }
    useEffect(() => { getListings() }, []);
    return <Box  sx={{
        display: "grid",
        gap: 2,
        gridTemplateColumns: "repeat(auto-fill, minmax(400px, 1fr))",
        flexGrow:1,
        gridAutoRows:"min-content",
      }}>
        {listings.map((x) => (
            <Listing listing={x} key={x.id} getListings={getListings}></Listing>
        ))}
    </Box>
}

export default AllListings;