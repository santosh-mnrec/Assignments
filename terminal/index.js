
var http  = require('http'),
    url   = require('url'),
    path  = require('path'),
    fs    = require('fs'),
    io    = require('socket.io'),
    sys   = require('sys'),
    util  = require('util'),
    spawn = require('child_process').spawn,
    express=require("express"),
    app=express();

var server = http.createServer(app);
app.use(express.static(__dirname+"/public"));
server.listen(process.env.PORT || 3000);

var listener = io.listen(server);
app.get("/",function(req,res){
    res.render("index.html");
});
listener.on('connection', function(client){
  var sh = spawn('bash');

  client.on('message', function(data){
    sh.stdin.write(data.toString()+"\n");
   
  });

  sh.stdout.on('data', function(data) {
    
    client.send(data.toString()+"\n");
  });

  sh.stderr.on('data', function(data) {
    client.send(data.toString()+"\n");
  });

  sh.on('exit', function (code) {
    client.send('** Shell exited: '+code+' **');
  });
});

