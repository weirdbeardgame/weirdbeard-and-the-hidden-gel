using Godot;
using Godot.Collections;
using System;
public partial class TestLevelSelect : LevelCommon
{
    [Export] private Array<PackedScene> _levels;
    private Array<string> _levelNames;

    ItemList selector;

    public override void EnterLevel(Player p)
    {
        _levelNames = new Array<string>();
        selector = GetNode<ItemList>("ItemList");

        selector.ItemSelected += SelectLevel;

        base.EnterLevel(p);

        foreach (var level in _levels)
        {
            LevelCommon l = (LevelCommon)level.Instantiate();
            _levelNames.Add(l.LevelName);
            selector.AddItem(l.LevelName);
        }
    }

    public void SelectLevel(long id)
    {
        if (id <= _levelNames.Count)
        {
            SceneManager.ChangeScene(_levelNames[((int)id)], Player);
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
