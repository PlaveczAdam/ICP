import { Box, Input, Button } from "@mui/material";
import { useState } from "react";
import empty from "../../img/empty.png";
import water from "../../img/water.png";
import city from "../../img/city.png";
import tree from "../../img/tree.png";
import { produce } from "immer";

const images = [empty, tree, water, city];
const enumNames = ["empty", "tree", "water", "city"];

function MapGenerator(props) {
  const [colSize, setColSize] = useState(0);
  const [rowSize, setRowSize] = useState(0);
  const [imp, setImp] = useState();
  const [map, setMap] = useState([]);

  const scale = 7 / rowSize;

  function handleMapGenerateClick() {
    let newMap = [];

    for (let i = 0; i < colSize; i++) {
      newMap.push([]);
      for (let j = 0; j < rowSize; j++) {
        newMap[i].push({ row: i, col: j, content: 0 });
      }
    }
    setMap(newMap);
  }

  function handleClick(tile, dir, event) {
    event.preventDefault();
    const newMap = produce(map, (draft) => {
      draft[tile.row][tile.col].content =
        (draft[tile.row][tile.col].content + dir + images.length) % images.length;
    });
    setMap(newMap);
  }

  function handleExport()
  {
    const exp = {map:map.map(x => x.map(y => ({...y, content:enumNames[y.content]})))};
    navigator.clipboard.writeText(JSON.stringify(JSON.stringify(exp)));
  }

  function handleImport()
  {
    const i = JSON.parse(JSON.parse(imp));
    setRowSize(i.map[0].length);
    setColSize(i.map.length);
    const data = i.map.map(x=>x.map(y=>({...y, content:enumNames.indexOf(y.content)})));
    setMap(data);
  }

  return (
    <Box>
      <Input
        value={colSize}
        onChange={(e) => setColSize(e.target.value)}
      ></Input>
      <Input
        value={rowSize}
        onChange={(e) => setRowSize(e.target.value)}
      ></Input>
      <Button onClick={handleMapGenerateClick}>Generate</Button>
      <Button onClick={handleExport}>Export</Button>

      <Input
        value={imp}
        onChange={(e) => setImp(e.target.value)}
      ></Input>
      <Button onClick={handleImport}>Import</Button>

      <Box width="100%" textAlign="left" marginTop={`${70 * scale}px`}>
        {map.map((x) => (
          <Box
            display="flex"
            marginTop={`${-53 * scale}px`}
            gap={`${65 * scale}px`}
            marginLeft={x[0].row % 2 === 0 ? `${92.5 * scale}px` : 0}
            key={x[0].row}
            zIndex={colSize-x[0].row}
          >
            {x.map((y) => (
              <Box key={y.col} flexShrink={0} flexGrow={0} display="flex">
                <img
                  width={`${120 * scale}px`}
                  height={`${104 * scale}px`}
                  src={images[y.content]}
                  onClick={(e) => handleClick(y, 1, e)}
                  onContextMenu={(e) => handleClick(y, -1, e)}
                  draggable="false"
                />
              </Box>
            ))}
          </Box>
        ))}
      </Box>
    </Box>
  );
}

export default MapGenerator;
