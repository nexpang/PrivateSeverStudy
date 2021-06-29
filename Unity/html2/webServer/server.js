const express = require('express');
const http = require('http');
const jwt = require('jsonwebtoken');
const { getMaxListeners } = require('process');
// Oauth, jwt


const app = new express();
const server = http.createServer(app);

app.use(express.json());
app.use(express.urlencoded({extended:true}));

app.post('/login', (req, res)=>{
    let token = jwt.sign({email:"rlarudgur123@gmail.com",level:5},"asd",{expiresIn:"24h"}) // 비밀키는 require 해야됨
    
    res.json({success:true, msg:token});
});
app.post('/save_data', (req, res)=>{
    let token = req.body.token;
    if(token === undefined || token==="")
    {
        res.json({success:false,msg:"잘못된 접근입니다."})
        return;
    }
    try{
        let decoded = jwt.verify(token, "asd"); // 비밀키
        if(decoded){
            console.log(req.body);
            res.json({success:true,msg:'데이터 저장 성공'});
        }else{
            res.json({success:false,msg:"잘못된 접근입니다."});
        }
    }catch(err){
        res.json({success:false,msg:"잘못된 접근입니다."});
    }
});

server.listen(54000, ()=>{
    console.log('Server is running on 54000 port');
});