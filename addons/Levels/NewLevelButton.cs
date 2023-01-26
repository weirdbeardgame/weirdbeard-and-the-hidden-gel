using Godot;
using System;

[Tool]
public class NewLevelButton : Button
{
    public override void _EnterTree()
    {
        Connect("pressed", this, "OnClick");
    }

    public void OnClick()
    {
        LevelEditor.CreateLevel.Invoke();
    }

}
