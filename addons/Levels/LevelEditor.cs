using Godot;
using System;
using System.Collections.Generic;

[Tool]
public class LevelEditor : EditorPlugin
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

        dock = (Control)GD.Load<PackedScene>("res://addons/Levels/LevelEditor.tscn").Instance();

        var newLevelS = GD.Load<Script>("res://addons/Levels/NewLevelButton.cs");
        var textureLevel = GD.Load<Texture>("icon.png");

        AddControlToDock(DockSlot.LeftUl, dock);
    }

    public void CreateNewLevel()
    {
        level = new Level();
        level.levelName = "New Level";

        levels.Add(level);
    }

    public void CreateNewSubLevel()
    {
        level = new SubLevel();
        level.levelName = "New Sub Level";

        levels.Add(level);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
    }
}
