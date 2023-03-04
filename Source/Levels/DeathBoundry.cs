using Godot;
using System;

public partial class DeathBoundry : Area2D
{

    public void OnTouch(object body)
    {
        if (body is Player)
        {
            Player p = (Player)body;
            p.Die();
        }
    }

}
