module.exports={devserver:function (configFunction){
    return function(proxy, allowedHost) {
        // Create the default config by calling configFunction with the proxy/allowedHost parameters
        const config = configFunction(proxy, allowedHost);
  
        console.log(config)
        config.open = {target:["http://127.0.0.1:3001"]};
  
        // Return your customised Webpack Development Server config.
        return config;
      };
}}