using Godot;
using System;
using System.Collections.Generic;

public class SceneManager : Node
{
    [Export]
    Dictionary<string, PackedScene> levels;

    [Export]
    Node currentScene;

    public Node CurrentScene
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

    public void SwitchLevel(string scene)
    {
        PackedScene sceneToLoad = levels[scene];
        if (currentScene != sceneToLoad.Instance())
        {
            // Play level changing animation.
            // Loat new scene and set it as current
            // This could be called from Game Manager at first but could also be in a hub world
            currentScene = sceneToLoad.Instance();
            Error err = GetTree().ChangeScene(sceneToLoad.ResourcePath);

            if (err != Error.Ok)
            {
                GD.PrintErr(err.ToString());
                throw new Exception(err.ToString());
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
