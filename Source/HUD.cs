using Godot;
using System;

public class HUD : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Label Lives;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Lives = (Label)GetNode("Lives");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Lives.Text = ("Lives: " + PlayerData.playerLives);
    }
}
