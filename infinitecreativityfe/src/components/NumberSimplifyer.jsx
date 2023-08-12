import { useMemo } from "react";

//⁰ ¹ ² ³ ⁴ ⁵ ⁶ ⁷ ⁸ ⁹
const letterArray = ["", "k", "k²", "k³", "k⁴", "k⁵", "k⁶", "k⁷", "k⁸", "k⁹", "k¹⁰", "k¹¹"];
const letterNumbers = letterArray.map((x,ind) => ({number:Math.pow(1000,ind), letter:x})).reverse();

export default function NumberSimplifyer(props)
{
    const res = useMemo(()=>{
        if(Math.abs(props.value)<1)
        {
            return props.value;
        }

        const baseNumber = letterNumbers.find((x)=> x.number <= Math.abs(props.value));
        const significantPart = (props.value/baseNumber.number).toFixed();
        return `${significantPart}${baseNumber.letter}`;
    },[props.value]);
    return res;
}

