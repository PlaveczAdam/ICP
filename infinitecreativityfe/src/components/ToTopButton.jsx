import Button from "@mui/material/Button";
import { Box } from "@mui/material";
import { useState, useEffect } from "react";
import up from "../img/up.png";

export default function ToTopButton() {
    const [topButton, setTopButton] = useState(false);

    useEffect(() => {
        const scrollEventListener = () => {
            if (window.scrollY > 500) {
                setTopButton(true);
            } else {
                setTopButton(false);
            }
        };

        window.addEventListener("scroll", scrollEventListener);

        return () => {
            window.removeEventListener("scroll", scrollEventListener);
        };

    }, []);

    const scrollUp = () => {
        window.scrollTo({
            top: 0,
            behavior: "smooth",
        });
    };

    return (
        <Box sx={{ zIndex: 999, background: "red", position: "absolute" }}>
            {topButton && (
                <Button onClick={scrollUp}>
                    <img src={up} alt="toTop" />
                </Button>
            )}
        </Box>
    );
}
