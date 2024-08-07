using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class SceneManager : EditorPlugin
{

    public static Action ResetLevel;
    public static Action StartNewGame;
    public static Action<string, Player> ChangeScene;
    public static Action<PackedScene, Player, Exit> ChangeSceneWithExit;

    public PackedScene NewGameScene => s_ManagerData.NewGameScene;
    public PackedScene PlayerRef => s_ManagerData.PlayerRef;

    private Control _editorDock;
    private static SceneManagerData s_ManagerData;

    [Export] private static LevelCommon s_currentScene;

    [Export] private string _sceneManagerPath = "res://SceneManagerData.tres";


    // Because Plugin exists outside the SceneTree, we create our own _Tree or refrence to one.
    private static SceneTree s_tree;

    public static LevelCommon s_CurrentScene => s_currentScene;

    private static SceneManager s_sceneManager;

    public static Player s_ActivePlayerRef;
    public static new SceneTree GetTree() => s_tree;

    public static SceneManager Manager
    {
        get
        {
            if (s_sceneManager == null)
            {
                s_sceneManager = new SceneManager();
            }
            return s_sceneManager;
        }
    }

    // Call this from your TitleScreen or other beginning scripts in game.
    // Because SceneManager exists as a plugin it does not exist in SceneTree
    // As such _Ready will not be called.
    public void Init(SceneTree T)
    {
        s_tree = new SceneTree();
        StartNewGame += NewGame;
        ChangeScene += SwitchLevel;
        ChangeSceneWithExit += LoadSubScene;
        ResetLevel += Reset;
        s_tree = T;

        if (ResourceLoader.Exists(_sceneManagerPath))
        {
            s_ManagerData = ResourceLoader.Load<Resource>(_sceneManagerPath) as SceneManagerData;

            s_ActivePlayerRef = CreatePlayer();
        }
    }

    void Reset() => s_currentScene.ResetLevel();

    public void DestroyPlayer(Player p) => p.Dispose();

    public Player CreatePlayer() => s_ManagerData.CreatePlayer();

    public void LoadSubScene(PackedScene subscene, Player p, Exit exit) => CallDeferred(nameof(CallDeferredSub), subscene.Instantiate<LevelCommon>(), p);


    // Play level changing animation.
    // Load new scene and set it as current
    public void SwitchLevel(string scene, Player Player)
    {
        if (s_currentScene is LevelCommon)
        {
            s_currentScene = (LevelCommon)s_tree.CurrentScene;
        }

        if (s_ManagerData.Levels.ContainsKey(scene))
        {
            LevelCommon sceneToLoad = s_ManagerData.Level(scene);
            CallDeferred(nameof(CallDefferedSwitch), sceneToLoad, Player, (int)sceneToLoad.LevelType);
        }
        else
        {
            GD.PrintErr("INVALID LEVEL SELECTED: " + scene);
        }
    }

    public LevelCommon GetLevel(string LevelName)
    {
        if (s_ManagerData.Levels.ContainsKey(LevelName))
        {
            return s_ManagerData.Level(LevelName);
        }
        return null;
    }

    void CallDeferredSub(SubLevel toLoad, Player Player, LevelType type)
    {
        s_CurrentScene.ExitLevel();
        s_CurrentScene.EnterSubLevel(Player, toLoad);
    }

    void CallDefferedSwitch(LevelCommon toLoad, Player Player, LevelType type)
    {
        TitleScreen title;

        if (s_CurrentScene != null)
        {
            s_CurrentScene.ExitLevel();
            s_tree.Root.RemoveChild(s_CurrentScene);
            s_currentScene.Free();
        }
        else if ((title = s_tree.Root.GetNodeOrNull<TitleScreen>("TitleScreen")) != null)
        {
            s_tree.Root.RemoveChild(title);
        }
        s_currentScene = toLoad;
        s_tree.Root.AddChild(s_CurrentScene);
        s_tree.CurrentScene = s_CurrentScene;
        s_CurrentScene.EnterLevel(Player);
    }

    void NewGame()
    {
        LevelCommon scene = s_ManagerData.NewGameScene.Instantiate<LevelCommon>();
        SwitchLevel(scene.LevelName, null);
        StartNewGame -= NewGame;
    }


#if TOOLS
    // Initialization of the plugin goes here.
    public override void _EnterTree()
    {
        base._EnterTree();
        GD.Print("EnterTree");

        if (!ResourceLoader.Exists(_sceneManagerPath))
        {
            GD.Print("No SceneManager Data!");
            s_ManagerData = new SceneManagerData();
        }
        else
        {
            var temp = ResourceLoader.Load<Resource>(_sceneManagerPath);
            GD.Print(temp.GetType());
            s_ManagerData = (SceneManagerData)temp;
        }

        EditorInterface.Singleton.GetResourceFilesystem().FilesystemChanged += s_ManagerData.Refresh;

        _editorDock = GD.Load<PackedScene>("res://addons/SceneManager/LevelDock.tscn").Instantiate<Control>();
        AddControlToDock(DockSlot.LeftUl, _editorDock);
    }

    public bool Add(PackedScene Scene) => s_ManagerData.Add(Scene);

    public void Remove(string SceneName) => s_ManagerData.Remove(SceneName);

    public void Save() => ResourceSaver.Save(s_ManagerData, _sceneManagerPath);

    public void Refresh() => s_ManagerData.Refresh();

    public void SetPlayerRef(string path) => s_ManagerData.SetPlayerRef(path);

    public void SetNewGameScene(string sceneName) => s_ManagerData.SetNewGameScene(sceneName);

    // TODO: Actually implement this. This is to generate a brand new scene and add it to the SceneManager.
    // This needs to open the inbuilt Level Editor!
    public bool New()
    {

        return false;
    }

    public List<string> SceneNames
    {
        get
        {
            if (s_ManagerData != null && s_ManagerData.Levels != null)
            {
                return s_ManagerData.Levels.Keys.ToList<string>();
            }
            return null;
        }
    }

    public void ChangeSceneInEditor(string SceneName)
    {
        if (s_ManagerData.Levels != null)
        {
            EditorInterface.Singleton.OpenSceneFromPath(s_ManagerData.Levels[SceneName].ResourcePath);
        }
    }

    public override void _ExitTree()
    {
        StartNewGame -= NewGame;
        ChangeScene -= SwitchLevel;
        ChangeSceneWithExit -= LoadSubScene;
        ResetLevel -= Reset;
        if (_editorDock != null)
        {
            RemoveControlFromDocks(_editorDock);
            _editorDock.Free();
        }
    }
#endif
}
