using System;

namespace ConsoleGame;

public class TitleScene : Scene
{
    private ConsoleKey input;

    private string[] titaleArt = 
    {
        @"  _____                                     ",
        @" |  __ \                                    ",
        @" | |  | |_   _ _ __   __ _  ___  ___  _ __  ",
        @" | |  | | | | | '_ \ / _` |/ _ \/ _ \| '_ \ ",
        @" | |__| | |_| | | | | (_| |  __/ (_) | | | |",
        @" |_____/ \__,_|_| |_|\__, |\___|\___/|_| |_|",
        @"                      __/ |                 ",
        @"                     |___/                  ",
        @"  _____                                     ",
        @" |  __ \                                    ",
        @" | |__) |___   __ _ _   _  ___              ",
        @" |  _  // _ \ / _` | | | |/ _ \             ",
        @" | | \ \ (_) | (_| | |_| |  __/             ",
        @" |_|  \_\___/ \__, |\__,_|\___|             ",
        @"               __/ |                        ",
        @"              |___/                         "
    };
    
    private string[] menuOptions = 
    {
        "1. 게임 시작",
        "2. 게임 설명",
        "3. 게임 종료"
    };
    public TitleScene()
    {
        name = "Title";
    }
    public override void Render()
    {
        for(int i = 0 ; i < titaleArt.Length ; ++i)
        {
            Console.WriteLine(titaleArt[i]);
        }
        for(int i = 0 ; i < menuOptions.Length ; ++i)
        {
            Console.WriteLine(menuOptions[i]);
        }
    }

    public override void Input()
    {
        input = Console.ReadKey(true).Key;
    }
    public override void Result()
    {
        switch (input)
        {
            case ConsoleKey.D1:
            Game.ChangeScene("Game");
            break;
            case ConsoleKey.D2:
            Game.ChangeScene("Help");
            break;
            case ConsoleKey.D3:
            //게임종료 기능1
            break;
        }
    }

    public override void Update()
    {
    }
}

//다이어그램?
//슈도코드처럼 해서 작성
//