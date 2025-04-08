using System;

namespace ConsoleGame;

public class Player
{
    public Vector2 position;
    
    private int curHP;
    public int CurHP { get { return curHP; } }
    private int maxHP;
    public int MaxHP { get { return maxHP; }}
    

    public Player()
    {
        maxHP = 100;
        curHP = maxHP;
    }

}
