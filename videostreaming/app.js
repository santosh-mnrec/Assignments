var express=require("express");
var app = express();
var server = require('http').Server(app);
var io = require('socket.io')(server);


var Log = require("log");
var log = new Log("debug");
var port = process.env.PORT || 3000;
app.use(express.static(__dirname + "/public"));
app.get("/", function (req, res) {
	res.redirect("index.html");
});
app.get("/new", function (req, res) {
	res.redirect("new.html");
});

server.listen(port, function () {
console.log("runnug");
});

io.on("connection", function (socket) {
	socket.on("stream", function (image) {
		socket.broadcast.emit('stream', image);
	});

});