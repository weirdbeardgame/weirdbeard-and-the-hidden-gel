using Godot;
using System;
using System.Collections.Generic;

[Tool]
public partial class LevelEditor : EditorPlugin
{
    Control dock;

    public static Action CreateLevel;
    public static Action CreateSubLevel;

    LevelCommon level;
    List<LevelCommon> levels;

    public override void _EnterTree()
    {
        CreateLevel += CreateNewLevel;
        levels = new List<LevelCommon>();

        dock = GD.Load<PackedScene>("res://addons/Levels/LevelEditor.tscn").Instantiate<Control>();

        var newLevelS = GD.Load<Script>("res://addons/Levels/NewLevelButton.cs");
        var textureLevel = GD.Load<Texture2D>("icon.png");

        AddControlToDock(DockSlot.LeftUl, dock);
    }

    public void CreateNewLevel()
    {
        level = new Level();
        level.LevelName = "New Level";

        levels.Add(level);
    }

    public void CreateNewSubLevel()
    {
        level = new SubLevel();
        level.LevelName = "New Sub Level";

        levels.Add(level);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
}
