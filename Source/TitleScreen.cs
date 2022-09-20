using Godot;
using System;

public class TitleScreen : Node
{

    // To playback screne transitions
    AnimationPlayer animated;

    StateMachine states;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        states = (StateMachine)GetNode("/root/GameStates/GameState");
        GetNode<Button>("NewGame").GrabFocus();
    }

    public void NewGame()
    {
        states.UpdateState("GAME");
        SceneManager.startNewGame();
    }

    public void LoadGame()
    {

    }

    public void Exit()
    {

    }
}
