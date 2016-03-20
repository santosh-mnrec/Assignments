var express = require('express'),
    http    = require('http'),
    app     = express(),
    srv     = http.createServer(app);
app.use(express.static(__dirname + '/html'));
var io = require('socket.io')(srv, { log: false });
io.on('connection', function(socket) {
    socket.on('draw',function(data){
        socket.broadcast.emit('draw',data);
    });
})
srv.listen(5000);