using Godot;
using System;

public class DeathBoundry : Area2D
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
