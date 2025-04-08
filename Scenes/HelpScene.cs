using System;

namespace ConsoleGame.Scenes;

public class HelpScene : Scene
{
    private string[] helpText = {
        "로그라이크 게임 설명",
        "",
        "이 게임은 턴제 로그라이크 던전 탐험 게임입니다.",
        "",
        "[ 조작 방법 ]",
        "↑↓←→: 캐릭터 이동",
        "I: 인벤토리 열기",
        "A: 공격하기",
        "P: 물약 사용",
        "ESC: 메뉴",
        "",
        "[ 게임 목표 ]",
        "던전의 가장 깊은 곳에 있는 보물을 찾아 탈출하세요!",
        "다양한 몬스터와 함정을 조심하세요.",
        "",
        "아무 키나 누르면 메인 메뉴로 돌아갑니다."
    };
    
    public HelpScene()
    {
        name = "Help";
    }
    public override void Input()
    {
        Console.ReadKey(true);
    }

    public override void Render()
    {
        foreach(var help in helpText)
            Console.WriteLine(help);
    }

    public override void Result()
    {
        Game.ChangeScene(Game.prevSceneName);
    }

    public override void Update()
    {
    }
}
