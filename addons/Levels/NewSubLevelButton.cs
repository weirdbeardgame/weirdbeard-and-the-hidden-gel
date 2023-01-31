using Godot;
using System;

[Tool]
public class NewSubLevelButton : Button
{
    public override void _EnterTree()
    {
        Connect("pressed", this, "OnClick");
    }

    public void OnClick()
    {
        LevelEditor.CreateSubLevel.Invoke();
    }

}
