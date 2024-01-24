using Godot;
using System;

public partial class TitleScreen : Node
{

    // To playback screne transitions
    AnimationPlayer animated;

    StateMachine states;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        SceneManager.Manager.Init(GetTree());
        states = (StateMachine)GetNode("/root/GameStates/GameState");
        GetNode<Button>("Camera2D/NewGame").GrabFocus();
    }

    public void NewGame()
    {
        states.UpdateState("GAME");
        SceneManager.StartNewGame();
    }

    public void LoadGame()
    {

    }

    public void Exit()
    {

    }
}
