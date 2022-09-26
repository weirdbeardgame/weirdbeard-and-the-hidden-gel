using Godot;
using System;

public class Attack : State
{
    Vector2 direction;

    Weapon w;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "ATTACK";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        w = (Weapon)player.equipped.Weapon.Instance();
        direction = player.direction.Sign();
        if (w.canThrowWeapon)
        {
            w.Attack(direction);
            Stop();
        }
    }

    public override void Stop()
    {
        stateMachine.ResetToOldState();
    }
}
