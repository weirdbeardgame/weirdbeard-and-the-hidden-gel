using Godot;
using System;

public partial class Ladder : State
{
    Vector2 inputVelocity = Vector2.Zero;
    [Export] float currentSpeed = 0;

    public override void _Ready()
    {
        stateName = "LADDER";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
        base._Ready();
    }
    public override void Start()
    {
        base.Start();
        player.gravity = 0;
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionJustPressed("Up"))
        {
            GD.Print("UP");
            inputVelocity.Y += 1 * currentSpeed;
        }

        if (Input.IsActionJustPressed("Down"))
        {
            inputVelocity.Y += -1 * currentSpeed;
        }

        return inputVelocity;
    }

    public override void FixedUpdate(double delta)
    {
        base.FixedUpdate(delta);
        // ToDo, add detection of End of Ladder in here
        player.Velocity += GetInput();
    }


    public override void Stop()
    {
        base.Stop();
        player.ResetState();
    }
}
