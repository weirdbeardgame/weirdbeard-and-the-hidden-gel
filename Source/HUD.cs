using Godot;
using System;

public class HUD : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    Player player;
    Label Lives;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (Player)Owner.GetNode("Player");
        Lives = (Label)GetNode("Lives");
    }

    public override void _Process(float delta)
    {
        Lives.Text = ("Lives: " + player.lives.ToString());
    }
}