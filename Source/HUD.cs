using Godot;
using System;

public class HUD : Node
{
    Label lifeCounter;

    Label coinCounter;

    Sprite equipedWeaponSprite;

    int coins;
    int lives;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        coinCounter = (Label)GetNode("Coins");
        lifeCounter = (Label)GetNode("Lives");
        Coin.getCoin += IncreaseCoinCounter;
        Coin.looseCoin += DecreaseCoinCounter;
    }

    public void Init(int c, int l)
    {
        coins = c;
        lives = l;
    }

    void IncreaseCoinCounter()
    {
        coins += 1;
        coinCounter.Text = ("Coins: " + coins.ToString());
    }

    void DecreaseCoinCounter()
    {
        coins -= 1;
        coinCounter.Text = ("Coins: " + coins.ToString());
    }

    void IncreaseLifeCounter()
    {
        lives += 1;
        lifeCounter.Text = ("Lives: " + coins.ToString());
    }

    void DecreaseLifeCounter()
    {
        lives -= 1;
        lifeCounter.Text = ("Lives: " + coins.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }
}
