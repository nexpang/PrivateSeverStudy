using UnityEngine;

public enum TankCategory 
{
    BlueTank = 0,
    RedTank = 1
}

public class LoginVO
{
    public string type;
    public TankCategory tank;
    public string name;
    public LoginVO(){
        
    }
    public LoginVO(string type, TankCategory tank, string name){
        this.type = type;
        this.tank = tank;
        this.name = name;
    }
}
