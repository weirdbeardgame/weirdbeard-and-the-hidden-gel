using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : Node
{
    [Export]
    Dictionary<string, PackedScene> levels;

    [Export]
    Level currentScene;

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

    // Play level changing animation.
    // Loat new scene and set it as current
    // This could be called from Game Manager at first but could also be in a hub world
    public void SwitchLevel(string scene)
    {
        if (currentScene != null)
        {
            currentScene.ExitLevel();
            currentScene = null;
        }

        PackedScene sceneToLoad = levels[scene];
        if (currentScene != sceneToLoad.Instance())
        {
            Error err = GetTree().ChangeSceneTo(sceneToLoad);

            if (err != Error.Ok)
            {
                GD.PrintErr(err.ToString());
                throw new Exception(err.ToString());
            }

            if (currentScene != null)
            {
                currentScene = (Level)GetTree().Root.GetNode("TileMap");
                currentScene.EnterLevel();
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
