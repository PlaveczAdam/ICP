import { Box } from "@mui/material";
import Listing from './Listing';
import { useEffect, useState } from 'react';

function AllListings(props)
{
    const [listings, setListings] = useState([]);

    async function getListings() {
        const res = await fetch("/api/listing");
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

export default AllListings;