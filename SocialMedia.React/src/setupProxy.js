const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
  "/api/",
  "/img/",
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: 'https://localhost:7042',
    ws: true,
    secure: false
  });

  app.use(appProxy);

  /* const chatProxy = createProxyMiddleware("/websocket/", {
    target: 'ws://localhost:5042/api/chat/webSocket',
    ws: true,
    secure: false,
    changeOrigin: true,
  });

  app.use(chatProxy); */
};