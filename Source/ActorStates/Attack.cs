using Godot;
using System;

public class Attack : State
{
    Vector2 direction;

    public override void Start()
    {
        direction = GetInput();
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "ATTACK";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {

        Weapon w = (Weapon)player.equipped.Weapon.Instance();

        if (w.canThrowWeapon)
        {
            Owner.Owner.AddChild(w);
            w.Position = player.GlobalPosition;
            w.Rotation = player.GlobalRotation;
            w.Attack(delta, player.GlobalPosition.Sign());
            Stop();
        }
    }

    public override void Stop()
    {
        stateMachine.ResetToOldState();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
