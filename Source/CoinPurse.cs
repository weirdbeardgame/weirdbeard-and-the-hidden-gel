using Godot;
using System;
using Godot.Collections;

// That's my purse! I don't know you!
public partial class CoinPurse : Node
{
    Array<Coin> coins;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (coins == null)
        {
            coins = new Array<Coin>();
        }

        Coin.getCoin += CollectCoin;
    }

    void CollectCoin()
    {
        coins.Add(new Coin());
    }


    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(double delta)
    //  {
    //      
    //  }
}
