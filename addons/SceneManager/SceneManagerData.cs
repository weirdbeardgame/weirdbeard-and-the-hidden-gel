using Godot;
using System;

[Tool]
public partial class SceneManagerData : Resource
{
    [Export] private PackedScene _newGameScene;

    [Export] private Godot.Collections.Dictionary<string, PackedScene> _levels;

    [Export] private string _CurrentLevel = "Test1";

    [Export] private string _PlayerPath;

    // The actual Player that will be instaniated
    [Export] private PackedScene _PlayerScene;

    public SceneManagerData() => _levels = new Godot.Collections.Dictionary<string, PackedScene>();
    public SceneManagerData(Godot.Collections.Dictionary<string, PackedScene> lev) => _levels = lev;

    // Public Getters
    public PackedScene NewGameScene => _newGameScene;
    public PackedScene PlayerRef => _PlayerScene;
    public Godot.Collections.Dictionary<string, PackedScene> Levels => _levels;

    // Returns the instantiated packed scene as a Level.
    public LevelCommon Level(string levelName) => _levels[levelName].Instantiate<LevelCommon>();

    // Returns the active Player refrence
    public Player CreatePlayer()
    {
        if (_PlayerScene == null)
        {
            _PlayerScene = ResourceLoader.Load<PackedScene>(_PlayerPath);
        }
        return _PlayerScene.Instantiate<Player>();
    }
#if TOOLS
    public void SetPlayerRef(string path)
    {
        _PlayerPath = path;
        _PlayerScene = ResourceLoader.Load<PackedScene>(path);
    }

    public bool Add(PackedScene Scene)
    {
        LevelCommon Level = Scene.Instantiate<LevelCommon>();

        if (string.IsNullOrEmpty(Level.LevelName))
        {
            GD.PrintErr("Level has no name!");
        }

        if (!_levels.ContainsKey(Level.LevelName))
        {
            _levels.Add(Level.LevelName, Scene);
            return true;
        }
        else
        {
            GD.PrintErr("Scene already exists");
        }
        return false;
    }

    public bool Remove(string SceneName)
    {
        if (_levels.ContainsKey(SceneName))
        {
            _levels.Remove(SceneName);
            return true;
        }
        GD.PrintErr("Scene does not exist in manager");
        return false;
    }

    public void Refresh()
    {
        GD.Print(_levels);
        foreach (var scene in _levels)
        {
            if (!ResourceLoader.Exists(scene.Value.ResourcePath))
            {
                Remove(scene.Key);
            }
        }
    }

    public void SetNewGameScene(string path)
    {
        LevelCommon l = ResourceLoader.Load<PackedScene>(path).Instantiate<LevelCommon>();
        if (_levels.ContainsKey(l.LevelName))
        {
            _newGameScene = _levels[l.LevelName];
        }
        else
        {
            _levels.Add(l.LevelName, ResourceLoader.Load<PackedScene>(path));
            _newGameScene = _levels[l.LevelName];
        }
    }
#endif

};