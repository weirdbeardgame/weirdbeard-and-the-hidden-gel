using Godot;
using System;

public partial class SwimStart : Area2D
{

    public override void _Ready()
    {
        BodyEntered += OnBody;

    }
    public void OnBody(object body)
    {
        if (body is Player)
        {

            GD.Print("Plaayyyerr");
            Player p = (Player)body;

            p.StateMachine.UpdateState("SWIM");
        }
    }

}
