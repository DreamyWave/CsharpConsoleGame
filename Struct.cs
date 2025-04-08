using System;

namespace ConsoleGame;

public struct Vector2
{
    public int x;
    public int y;

    public Vector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public static bool operator ==(Vector2 left, Vector2 right)
    {
        return left.x == right.x && left.y == right.y;
    }
    public static bool operator !=(Vector2 left, Vector2 right)
    {
        return left.x != right.x || left.y != right.y;
    }

}
