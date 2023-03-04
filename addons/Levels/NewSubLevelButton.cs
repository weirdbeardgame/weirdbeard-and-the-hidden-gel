using Godot;
using System;

[Tool]
public partial class NewSubLevelButton : Button
{
    public override void _EnterTree()
    {
        Connect("pressed",new Callable(this,"OnClick"));
    }

    public void OnClick()
    {
        LevelEditor.CreateSubLevel.Invoke();
    }

}
