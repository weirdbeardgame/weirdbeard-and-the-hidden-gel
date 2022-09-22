using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : Node
{
    [Export]
    Dictionary<string, PackedScene> levels;

    [Export]
    private static LevelCommon currentScene;

    [Export]
    private LevelCommon activeSubScene;

    ResourceInteractiveLoader loader;

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
    public static Action<PackedScene, Player> changeScene;
    public static Action<PackedScene, Player, Exit> changeSceneWithExit;

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
    }

    public void ResetLevel(Player player)
    {
        string sceneName = currentScene.levelName;

        currentScene.ExitLevel();
        GetTree().Root.RemoveChild(currentScene);

        GetTree().Root.AddChild(currentScene);
        GetTree().CurrentScene = currentScene;

        CurrentScene.EnterLevel(player);
    }

    // Play level changing animation.
    // Load new scene and set it as current
    public void SwitchLevel(PackedScene scene, Player player)
    {
        if (currentScene is LevelCommon)
        {
            currentScene = (Level)GetTree().CurrentScene;
        }

        if (levels.ContainsValue(scene))
        {
            LevelCommon sceneToLoad = (LevelCommon)scene.Instance();
            CallDeferred(nameof(CallDefferedSwitch), sceneToLoad, player);
        }
    }

    public void LoadSubScene(PackedScene subscene, Player p, Exit exit)
    {
        activeExit = exit;
        CallDeferred(nameof(CallDeferredSub), (LevelCommon)subscene.Instance(), p);
    }


    void BackgroundLoadScene(Level toLoad, Player player)
    {

    }

    void CallDeferredSub(SubLevel toLoad, Player player)
    {
        currentScene.ExitLevel();
        GetTree().Root.RemoveChild(currentScene);
        activeSubScene = toLoad;
        GetTree().Root.AddChild(activeSubScene);
        activeSubScene.EnterLevel(player);
    }

    void CallDefferedSwitch(Level toLoad, Player player)
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
        currentScene.EnterLevel(player);
    }

    void NewGame()
    {
        SwitchLevel(levels["TestLevel"], null);
        startNewGame -= NewGame;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
