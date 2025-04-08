using System;
using System.Security;
using ConsoleGame.Scenes;

namespace ConsoleGame;

public static class Game
{
    private static Dictionary<string, Scene>    mSceneDic; //Scene Collection
    private static Scene                        mCurScene; //The scene we're going to do
    public static string                        prevSceneName;

    private static Player player;
    public static Player Player { get { return player; } }
    
    private static bool mGameRun;
    public static void Run()
    {
        Start();
        while (mGameRun)
        {
            Console.Clear();
            mCurScene.Render();
            mCurScene.Input();
            Console.WriteLine();
            mCurScene.Update();
            mCurScene.Result();
        }
        End();
    }
    
    public static void ChangeScene(string sceneName)
    {
        prevSceneName = mCurScene.name;
        mCurScene.Exit();
        mCurScene = mSceneDic[sceneName];
        mCurScene.Enter();
    }

    private static void Start()
    {
        Console.CursorVisible = false;

        // 게임 설정
        mGameRun = true;
        
        //플레이어 설정
        player = new Player();

        //씬 설정
        mSceneDic = new Dictionary<string, Scene>();
        mSceneDic.Add("Title",  new TitleScene());
        mSceneDic.Add("Help",   new HelpScene());
        mSceneDic.Add("Game",   new GameScene());
        
        mCurScene = mSceneDic["Title"];
    }
    
    private static void End()
    {
        
    }
}
