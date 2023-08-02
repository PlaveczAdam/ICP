import { Box } from "@mui/material";

function Listing(props) {
    const currentDate = new Date();
    const parseDate = new Date(props.listing.listingDate);
    const timeDifference = currentDate.getTime() - parseDate.getTime();
    const daysPassed = Math.floor(timeDifference / (1000 * 60 * 60 * 24));
    return <Box sx={{
        border: "2px solid rgb(0, 105,94,1)",
        maxWidth: "300px",
        maxHeight: "300px",
        borderRadius: "5px",
        background: "rgb(0,105,94,0.7)",
        display: "flex",
        flexDirection: "column",
        padding: "5px",
        marginTop: "3px",
    }}>
        
        <Box>
            {`Seller: ${props.listing.seller.name}`}
        </Box>
        <Box>
            {`Price: ${props.listing.price}`}
        </Box>
        <Box>
            {`Item: ${props.listing.item.name}`}
        </Box>
        <Box sx={{fontSize: 'small'}}>
            {`${daysPassed} days ago`}
        </Box>
    </Box>
}

export default Listing;