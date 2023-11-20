using Godot;
using Godot.Collections;
using System;
public partial class TestLevelSelect : LevelCommon
{
    [Export] Array<PackedScene> _levels;
    Array<string> _levelNames;

    ItemList selector;

    public override void EnterLevel(Player p, LevelType t)
    {
        _levelNames = new Array<string>();
        selector = GetNode<ItemList>("ItemList");

        selector.ItemSelected += SelectLevel;

        base.EnterLevel(p, t);

        foreach (var level in _levels)
        {
            LevelCommon l = (LevelCommon)level.Instantiate();
            _levelNames.Add(l.levelName);
            selector.AddItem(l.levelName);
        }
    }

    public void SelectLevel(long id)
    {
        if (id <= _levelNames.Count)
        {
            SceneManager.changeScene(_levelNames[((int)id)], Player);
        }
        else
        {
            GD.PrintErr("Scene not in manager");
            throw (new Exception("Out of range!"));
        }
    }

    public override void ResetLevel()
    {

    }

    public override void ExitLevel()
    {
        base.ExitLevel();
    }
}
