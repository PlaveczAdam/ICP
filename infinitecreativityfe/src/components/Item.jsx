import { Box } from '@mui/material';

function Item(props)
{
    return <Box width="50px" height="50px">
        {props.item.id}
    </Box>
}

export default Item;