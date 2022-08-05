using Godot;
using System;


// This is literally the thing the Game's State Machine is attached to. 
// The purpose of this class is to track Game State. Player State to some small degree though the player
// Has a state machine of it's own and as such is mostly independant of this class.
// This will track progress, game over, will handle basic save logic. UI processing if needed
public class GameManager : Node
{
    StateMachine gameState;
    Player player;
    SceneManager scenes;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Player)Owner.GetNode("Player");
        gameState = (StateMachine)GetNode("StateMachine");
        scenes = (SceneManager)GetNode("SceneManager");
        NewGame();
    }

    public void NewGame()
    {
        scenes.SwitchLevel("TestLevel");
        gameState.UpdateState("GAME");
    }

    public override void _Process(float delta)
    {
        if (player.GamaOvar())
        {
            gameState.UpdateState("GAMEOVER");
        }
    }

    public override void _PhysicsProcess(float delta)
    {

    }
}
