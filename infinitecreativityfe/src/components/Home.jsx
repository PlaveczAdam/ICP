import { Box, Button, Portal } from "@mui/material";

function Home(props) {
  return (
    <Box>
      <Portal container={()=>document.getElementById("sideBarContent")}>
      <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            height: "100%",
            width: "100%",
            alignItems: "center",
          }}
        >
          Home
        </Box>
      </Portal>
      <Box sx={{fontSize: "xx-large", typography: "body1"}}>
        Welcome to Infinite Creativity
      </Box>
      <Box
        sx={{
          backgroundColor: "rgb(10, 10, 10, 0.8)",
          typography: "body",
          textAlign: "left",
          padding: "30px",
          color: "white",
          fontWeight: "600",
          fontSize: "xlarge",
        }}
      >
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec
        dictum ante, ac molestie tellus. Mauris nunc dolor, elementum sed
        sodales nec, hendrerit nec est. Donec nulla enim, efficitur non lorem
        nec, euismod venenatis ligula. Aenean nec faucibus ipsum. Vivamus nec
        tempus neque. Donec mattis pharetra nisi a laoreet. Nam nec magna ac
        urna pellentesque hendrerit. Ut ac elit eget quam cursus hendrerit ut ac
        leo. Nullam nec euismod libero. Orci varius natoque penatibus et magnis
        dis parturient montes, nascetur ridiculus mus. Fusce aliquam dignissim
        metus, condimentum accumsan tellus porta eu. Fusce finibus convallis
        suscipit. Nulla luctus lobortis est quis consequat. Integer id erat
        massa. Sed in nunc eu orci aliquet scelerisque. Nunc tempor blandit
        libero, nec facilisis lacus eleifend semper. Phasellus molestie, ex eu
        tincidunt laoreet, magna velit congue libero, id vulputate purus erat
        nec ipsum. Ut venenatis arcu non dolor bibendum sagittis sed vel diam.
        Curabitur vitae sapien eu urna vulputate porta. Nullam at dolor non
        lacus lobortis malesuada. Aliquam venenatis urna neque, nec commodo
        ligula laoreet a. Maecenas ullamcorper, neque sit amet porta commodo,
        erat justo condimentum ipsum, in laoreet velit tellus in elit. Nam nec
        accumsan mauris. Integer congue nisi at dignissim dignissim. Duis
        suscipit consectetur ex a maximus. Etiam at odio ac ex tincidunt
        imperdiet eu id quam. Nulla facilisi.
      </Box>
    </Box>
  );
}

export default Home;
