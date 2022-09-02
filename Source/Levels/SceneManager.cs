using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : Node
{
    [Export]
    Dictionary<string, PackedScene> levels;

    [Export]
    private Level currentScene;


    public Level CurrentScene
    {
        get
        {
            return currentScene;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (levels == null)
        {
            GD.PrintErr("ERROR: Scene's not in manager");
            throw new Exception("Scene's not in manager");
        }
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
    // Loat new scene and set it as current
    // This could be called from Game Manager at first but could also be in a hub world
    public void SwitchLevel(string scene)
    {
        currentScene = (Level)GetTree().CurrentScene;
        Level sceneToLoad = (Level)levels[scene].Instance();
        CallDeferred(nameof(CallDefferedSwitch), sceneToLoad);

        currentScene = (Level)GetTree().CurrentScene;
    }

    void CallDefferedSwitch(Level toLoad)
    {
        Player player = null;
        if (currentScene.levelName != toLoad.levelName)
        {
            if (player == null)
            {
                player = GameManager.player;
            }

            if (currentScene != null)
            {
                currentScene.RemoveChild(player);
                currentScene.ExitLevel();
                currentScene.Free();
            }

            currentScene = toLoad;
            GetTree().Root.AddChild(currentScene);
            currentScene.EnterLevel(player);
            GetTree().CurrentScene = currentScene;
        }
    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
