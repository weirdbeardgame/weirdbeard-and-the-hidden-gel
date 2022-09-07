using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : Node
{
    [Export]
    Dictionary<string, PackedScene> levels;

    [Export]
    private LevelCommon currentScene;

    ResourceInteractiveLoader loader;

    Node nodeParent;

    public LevelCommon CurrentScene
    {
        get
        {
            return currentScene;
        }
    }

    public static Action startNewGame;
    public static Action<LevelCommon, Player> changeScene;

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
    public void SwitchLevel(LevelCommon scene, Player player)
    {
        if (currentScene is LevelCommon)
        {
            currentScene = (Level)GetTree().CurrentScene;
        }

        LevelCommon sceneToLoad = (LevelCommon)levels[scene.levelName].Instance();
        CallDeferred(nameof(CallDefferedSwitch), sceneToLoad, player);
    }


    void BackgroundLoadScene(Level toLoad, Player player)
    {

    }

    void CallDefferedSwitch(Level toLoad, Player player)
    {
        if (currentScene != null)
        {
            currentScene.RemoveChild(player);
            currentScene.ExitLevel();
            //currentScene.Free();
        }
        else
        {
            GetTree().Root.RemoveChild(GetTree().CurrentScene);
        }
        currentScene = toLoad;
        GetTree().Root.AddChild(currentScene);
        currentScene.EnterLevel(player);
        GetTree().CurrentScene = currentScene;
    }

    void NewGame()
    {
        SwitchLevel((LevelCommon)levels["TestLevel"].Instance(), null);
        startNewGame -= NewGame;
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
