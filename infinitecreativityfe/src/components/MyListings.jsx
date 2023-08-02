import { Box } from "@mui/material";
import Listing from './Listing';
import { useEffect, useState } from 'react';

function MyListings(props) {
    const [listings, setListings] = useState([]);

    async function getListings() {
        const res = await fetch("/api/listing");
        const l = await res.json();
        setListings(l);
        console.log(l);
    }
    useEffect(() => { getListings() }, []);
    return <Box>
        {listings.map((x) => (
            <Listing listing={x} key={x.id}></Listing>
        ))}
    </Box>
}

export default MyListings;