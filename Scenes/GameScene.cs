using System;
using System.Data;

namespace ConsoleGame.Scenes;

public class GameScene : Scene
{
    private char[,] _map;
    private const int Width = 100;
    private const int Height = 100;
    private BSPMapGenerator mMapGenerator;
    
    public GameScene()
    {
        mMapGenerator = new BSPMapGenerator(Width, Height);
    }
    public override void Input()
    {
        
        Console.ReadKey(true);
    }

    public override void Render()
    {
        for(int i = 0; i < Height; ++i)
        {
            for(int j = 0; j < Width; ++j)   
            {
                Console.Write(_map[j, i]);   
            }
            Console.WriteLine();
        }
    }

    public override void Result()
    {

    }

    public override void Update()
    {
    }
    public override void Enter()
    {
        base.Enter();
        mMapGenerator.GenerateMap(ref _map);
    }
}
