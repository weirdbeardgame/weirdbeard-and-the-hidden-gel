using Godot;
using System;

public class HUD : Node
{
    Label lives;

    Label coinCounter;

    int coins;

    Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        coinCounter = (Label)GetNode("Coins");
        lives = (Label)GetNode("Lives");
        //player = (Player)Owner.Owner;
        Coin.getCoin += IncreaseCoinCounter;
    }

    void IncreaseCoinCounter()
    {
        coins += 1;
        coinCounter.Text = ("Coins: " + coins.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //lives.Text = ("Lives: " + player.playerLives);
    }
}
