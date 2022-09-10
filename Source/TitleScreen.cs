using Godot;
using System;

public class TitleScreen : Node
{

    // To playback screne transitions
    AnimationPlayer animated;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<Button>("Grid/NewGame").GrabFocus();
    }

    public void NewGame()
    {
        SceneManager.startNewGame();
    }

    public void LoadGame()
    {

    }

    public void Stop()
    {

    }
}
