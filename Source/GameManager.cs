using Godot;
using System;


// This is literally the thing the Game's State Machine is attached to. 
// The purpose of this class is to track Game State. Player State to some small degree though the Player
// Has a state machine of it's own and as such is mostly independant of this class.
// This will track progress, will handle basic save logic. UI processing if needed
public partial class GameManager : Node
{
    StateMachine gameState;

    public Player Player;

    SceneManager scenes;

    bool isLevelReset = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Player = (Player)GetTree().CurrentScene.GetNode("Player");
        gameState = (StateMachine)GetNode("StateMachine");
        scenes = (SceneManager)GetNode("SceneManager");

        NewGame();
    }

    public void NewGame()
    {
    }

    public override void _Process(double delta)
    {

    }

    public override void _PhysicsProcess(double delta)
    {

    }
}
