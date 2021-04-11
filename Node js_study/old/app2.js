// Synchronous  vs  Asynchronous
// Block        vs  Non-Block

const fs = require("fs");

//const data = fs.readFileSync("./test.txt", {encoding: "utf-8"})
//console.log(data);
let text = "default";
// fs.readFile("./test.txt",{encoding:"utf-8"}, (err, data)=>{
//     console.log(data);
//     text = data;
// });
// console.log(text);
let startTime = Date.now();
setTimeout(()=>{
    console.log(Date.now()- startTime, "First");
    console.log("5s");
}, 5000);
console.log(Date.now()- startTime, "Second");
