using System;
using System.Diagnostics.Contracts;

namespace ConsoleGame;

public abstract class Scene
{
    public string name;

    public abstract void Render();
    public abstract void Input();
    public abstract void Update();
    public abstract void Result();

    public virtual void Enter() {}
    public virtual void Exit() {}
}
