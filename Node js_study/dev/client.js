var http = require('http');
var options = {
    host: '127.0.0.1',
    path: '/',
    port: '8080',
    method: 'POST'
};
var req = http.request(options, function(response){
    var std = '';
    response.on('data', function(chunk){
        std += chunk;
    });
    response.on('end', function(chunk){
        console.log(str);
    });
});
req.end();