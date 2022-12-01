using Godot;
using System;

public class HubActor : Node2D
{

    // This just holds the active refrence but doesn't allow the player to act.
    Player player;

    // Playable sprite bobs up and down.
    AnimationPlayer animation;

    public Player Player
    {
        get
        {
            return player;
        }
    }

    public void Activate(Player p)
    {
        player = p;
        player.DeactivateCamera();
        player.Visible = false;
    }

    public void Deactivate()
    {
        player.ActivateCamera();
        player.Visible = true;
        RemoveChild(player);
    }

    // Play animations to the call of the Hubworld / active path making character walk.
    public void Move()
    {

    }
}
