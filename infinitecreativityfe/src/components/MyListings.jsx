import { Box } from "@mui/material";
import Listing from './Listing';
import { useContext, useEffect, useState } from 'react';
import { UserContext } from "./UserContextProvider";

function MyListings(props) {
    const [listings, setListings] = useState([]);
    const userCTX = useContext(UserContext);

    async function getListings() {
        const queryParams = new URLSearchParams({sellerId:userCTX.user.id}).toString();
        const res = await fetch(`/api/listing?${queryParams}`);
        const l = await res.json();
        setListings(l);
    }
    useEffect(() => { getListings() }, []);
    return <Box>
    {listings.map((x) => (
        <Listing listing={x} key={x.id} getListings={getListings}></Listing>
    ))}
</Box>
}

export default MyListings;