using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

[Tool]
public partial class SceneManager : EditorPlugin
{
    [Export] private static LevelCommon s_currentScene;
    public static Action ResetLevel;
    public static Action StartNewGame;
    public static Action<string, Player> ChangeScene;
    public static Action<PackedScene, Player, Exit> ChangeSceneWithExit;

    public PackedScene NewGameScene => s_ManagerData.NewGameScene;
    public PackedScene PlayerRef => s_ManagerData.PlayerRef;

    Control EditorDock;
    private static SceneManagerData s_ManagerData;

    [Export]
    string SceneManagerPath = "res://SceneManagerData.tres";

    public static Player _ActivePlayerRef;


    // Because Plugin exists outside the SceneTree, we create our own Tree or refrence to one.
    SceneTree Tree;

    public static LevelCommon s_CurrentScene => s_currentScene;

    private static SceneManager _SceneManager;

    public static SceneManager Manager
    {
        get
        {
            if (_SceneManager == null)
            {
                _SceneManager = new SceneManager();
            }
            return _SceneManager;
        }
    }

    // Call this from your TitleScreen or other beginning scripts in game.
    // Because SceneManager exists as a plugin it does not exist in SceneTree
    // As such _Ready will not be called.
    public void Init(SceneTree T)
    {
        Tree = new SceneTree();
        StartNewGame += NewGame;
        ChangeScene += SwitchLevel;
        ChangeSceneWithExit += LoadSubScene;
        ResetLevel += Reset;
        Tree = T;

        if (ResourceLoader.Exists(SceneManagerPath))
        {
            s_ManagerData = ResourceLoader.Load<Resource>(SceneManagerPath) as SceneManagerData;

            _ActivePlayerRef = CreatePlayer();

        }
    }

    void Reset() => s_currentScene.ResetLevel();

    public Player CreatePlayer() => s_ManagerData.CreatePlayer();

    public void DestroyPlayer(Player p) => p.Dispose();

    public void LoadSubScene(PackedScene subscene, Player p, Exit exit) => CallDeferred(nameof(CallDeferredSub), subscene.Instantiate<LevelCommon>(), p);


    // Play level changing animation.
    // Load new scene and set it as current
    public void SwitchLevel(string scene, Player Player)
    {
        if (s_currentScene is LevelCommon)
        {
            s_currentScene = (LevelCommon)Tree.CurrentScene;
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

#if TOOLS

    // Initialization of the plugin goes here.
    public override void _EnterTree()
    {
        base._EnterTree();
        GD.Print("EnterTree");

        if (!ResourceLoader.Exists(SceneManagerPath))
        {
            GD.Print("No SceneManager Data!");
            s_ManagerData = new SceneManagerData();
        }
        else
        {
            var temp = ResourceLoader.Load<Resource>(SceneManagerPath);
            GD.Print(temp.GetType());
            s_ManagerData = (SceneManagerData)temp;
        }

        EditorDock = GD.Load<PackedScene>("res://addons/SceneManager/LevelDock.tscn").Instantiate<Control>();
        AddControlToDock(DockSlot.LeftUl, EditorDock);
    }

    public bool Add(PackedScene Scene) => s_ManagerData.Add(Scene);

    public void Remove(string SceneName) => s_ManagerData.Remove(SceneName);

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
        if (EditorDock != null)
        {
            RemoveControlFromDocks(EditorDock);
            EditorDock.Free();
        }
        GD.Print(ResourceSaver.Save(s_ManagerData, SceneManagerPath));
    }
#endif

    public LevelCommon GetLevel(string LevelName)
    {
        return s_ManagerData.Level(LevelName);
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
            Tree.Root.RemoveChild(s_CurrentScene);
            s_currentScene.Free();
        }
        else if ((title = Tree.Root.GetNodeOrNull<TitleScreen>("TitleScreen")) != null)
        {
            Tree.Root.RemoveChild(title);
        }
        s_currentScene = toLoad;
        Tree.Root.AddChild(s_CurrentScene);
        Tree.CurrentScene = s_CurrentScene;
        s_CurrentScene.EnterLevel(Player);
    }

    void NewGame()
    {
        LevelCommon scene = s_ManagerData.NewGameScene.Instantiate<LevelCommon>();
        SwitchLevel(scene.LevelName, null);
        StartNewGame -= NewGame;
    }
}
