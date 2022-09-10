using Godot;
using System;
using System.Collections.Generic;

public class CoinPurse : Node
{
    List<Coin> coins;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (coins == null)
        {
            coins = new List<Coin>();
        }

        Coin.getCoin += CollectCoin;
    }

    void CollectCoin()
    {
        coins.Add(new Coin());
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}