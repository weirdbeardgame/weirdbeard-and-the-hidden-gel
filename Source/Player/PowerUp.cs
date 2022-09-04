using Godot;
using System;

public class PowerUp : State
{
    Sprite powerupSprite;

    [Export]
    protected float gravity;

    [Export]
    protected float speed;

    protected Vector2 inputVelocity;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    public void Equip(object body)
    {
        GD.Print("Equip");
        if (body is Player)
        {
            player = (Player)body;
            player.EquipPowerup(this);
            stateMachine = (StateMachine)player.GetNode("StateMachine");
            stateMachine.AddState(this, stateName);
        }
    }
}
