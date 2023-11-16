const { createProxyMiddleware } = require("http-proxy-middleware");
const express = require("express");

const app = express();

app.use(
  "/api",
  createProxyMiddleware({
    target: "http://127.0.0.1:5152",
    changeOrigin: true,
  })
);

app.use(
  "/notification",
  createProxyMiddleware({
    target: "ws://127.0.0.1:5152",
    ws: true,
    changeOrigin: true,
  })
);

app.use(
  "/",
  createProxyMiddleware({
    target: "http://127.0.0.1:3000",
    changeOrigin: true,
  })
);

app.listen(3001);
