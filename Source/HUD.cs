using Godot;
using System;

public partial class HUD : Node
{
    Label lifeCounter;

    Label coinCounter;

    Sprite2D equippedWeaponSprite;

    int coins;
    int lives;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        coinCounter = (Label)GetNode("Coins");
        lifeCounter = (Label)GetNode("Lives");
        equippedWeaponSprite = (Sprite2D)GetNode("WeaponBox/WeaponIcon");
        Coin.getCoin += IncreaseCoinCounter;
        Coin.looseCoin += DecreaseCoinCounter;
        WeaponSlot.updateWSprite += UpdateWeaponSprite;
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

    void UpdateWeaponSprite(Texture2D s)
    {
        if (equippedWeaponSprite.Texture != s)
        {
            equippedWeaponSprite.Texture = s;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }
}
