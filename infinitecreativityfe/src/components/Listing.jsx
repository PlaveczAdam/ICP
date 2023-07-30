import { Box } from "@mui/material";

function Listing(props)
{
    return <Box>
        {props.listing.price}
    </Box>
}

export default Listing;