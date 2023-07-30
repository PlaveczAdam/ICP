function DoubleDisplay(props)
{
    return props.value.toFixed(props.precision??2);
}

export default DoubleDisplay;