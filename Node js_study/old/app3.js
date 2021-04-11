// JSON
// Javascript Object Notation

const fs = require("fs");

const data = fs.readFileSync("./test.json", { encoding: "utf-8" });
// JSON.parse() : 제이슨(스트링) 데이터를 다시 변환
// JSON.stringfy() : 데이터를 제이슨(스트링)으로 변환
let arr = JSON.parse(data);
console.log(arr[2]);
let obj = {
    name: "nexpnag",
    sendMsg: "ㅇㅅㅇ"
}
fs.writeFileSync("test.json", JSON.stringify(obj, null, 2));