using Godot;
using System;

[Tool]
public partial class SceneButton : Button
{
    public int Index;

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnClick;

    }

    void OnClick()
    {
        ItemList.s_IndexUpdate.Invoke(Index);
    }

}
