using Godot;
using Godot.Collections;
using System;

public partial class SceneManager : Node
{
    [Export] Dictionary<string, PackedScene> levels;

    [Export] private static LevelCommon currentScene;

    [Export] private LevelCommon activeSubScene;

    [Export] private PackedScene newGameScene;

    Exit activeExit;

    Node nodeParent;

    public static LevelCommon CurrentScene
    {
        get
        {
            return currentScene;
        }
    }

    public static Action startNewGame;
    public static Action<string, Player> changeScene;
    public static Action<PackedScene, Player, Exit> changeSceneWithExit;
    public static Action resetLev;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (levels == null)
        {
            GD.PrintErr("ERROR: Scene's not in manager");
            throw new Exception("Scene's not in manager");
        }

        startNewGame += NewGame;
        changeScene += SwitchLevel;
        changeSceneWithExit += LoadSubScene;
        resetLev += ResetLevel;
    }

    void ResetLevel()
    {
        currentScene.ResetLevel();
    }

    // Play level changing animation.
    // Load new scene and set it as current
    public void SwitchLevel(string scene, Player Player)
    {
        if (currentScene is LevelCommon)
        {
            currentScene = (LevelCommon)GetTree().CurrentScene;
        }

        if (levels.ContainsKey(scene))
        {
            LevelCommon sceneToLoad = levels[scene].Instantiate<LevelCommon>();
            CallDeferred(nameof(CallDefferedSwitch), sceneToLoad, Player);
        }
    }

    public void LoadSubScene(PackedScene subscene, Player p, Exit exit)
    {
        activeExit = exit;
        CallDeferred(nameof(CallDeferredSub), subscene.Instantiate<LevelCommon>(), p);
    }


    void BackgroundLoadScene(Level toLoad, Player Player)
    {

    }

    void CallDeferredSub(SubLevel toLoad, Player Player)
    {
        currentScene.ExitLevel();
        GetTree().Root.RemoveChild(currentScene);
        activeSubScene = toLoad;
        GetTree().Root.AddChild(activeSubScene);
        activeSubScene.EnterLevel(Player);
    }

    void CallDefferedSwitch(LevelCommon toLoad, Player Player)
    {
        if (currentScene != null)
        {
            currentScene.ExitLevel();
            GetTree().Root.RemoveChild(currentScene);
            currentScene.Free();
        }
        else
        {
            GetTree().Root.RemoveChild(GetTree().CurrentScene);
        }
        currentScene = toLoad;
        GetTree().Root.AddChild(currentScene);
        GetTree().CurrentScene = currentScene;
        currentScene.EnterLevel(Player);
    }

    void NewGame()
    {
        LevelCommon scene = newGameScene.Instantiate<LevelCommon>();
        SwitchLevel(scene.levelName, null);
        startNewGame -= NewGame;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
