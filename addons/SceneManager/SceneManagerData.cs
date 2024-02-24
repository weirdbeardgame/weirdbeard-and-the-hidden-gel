using Godot;
using System;

[Tool]
public partial class SceneManagerData : Resource
{
    [Export] private PackedScene _NewGameScene;

    [Export] private Godot.Collections.Dictionary<string, PackedScene> _Levels;

    [Export] private string _CurrentLevel = "Test1";

    [Export] private string _PlayerPath;

    // The actual Player that will be instaniated
    [Export] private PackedScene _PlayerScene;

    public SceneManagerData() => _Levels = new Godot.Collections.Dictionary<string, PackedScene>();
    public SceneManagerData(Godot.Collections.Dictionary<string, PackedScene> lev) => _Levels = lev;

    // Public Getters
    public PackedScene NewGameScene => _NewGameScene;
    public PackedScene PlayerRef => _PlayerScene;
    public Godot.Collections.Dictionary<string, PackedScene> Levels => _Levels;

    // Returns the instantiated packed scene as a Level.
    public LevelCommon Level(string levelName) => _Levels[levelName].Instantiate<LevelCommon>();

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

        if (!_Levels.ContainsKey(Level.LevelName))
        {
            _Levels.Add(Level.LevelName, Scene);
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
        if (_Levels.ContainsKey(SceneName))
        {
            _Levels.Remove(SceneName);
            return true;
        }
        GD.PrintErr("Scene does not exist in manager");
        return false;
    }

    public void SetNewGameScene(string path)
    {
        LevelCommon l = ResourceLoader.Load<PackedScene>(path).Instantiate<LevelCommon>();
        if (_Levels.ContainsKey(l.LevelName))
        {
            _NewGameScene = _Levels[l.LevelName];
        }
        else
        {
            _Levels.Add(l.LevelName, ResourceLoader.Load<PackedScene>(path));
            _NewGameScene = _Levels[l.LevelName];
        }
    }
#endif

};