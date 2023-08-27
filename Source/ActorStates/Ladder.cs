using Godot;
using System;

public partial class Ladder : State
{
    Vector2 inputVelocity = Vector2.Zero;
    [Export] float currentSpeed = 0;

    Objects obj = 0;

    public override void _Ready()
    {
        StateName = "LADDER";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
        base._Ready();
    }
    public override void Start()
    {
        base.Start();
        Player.gravity = 0;
    }

    public override Vector2 GetInput()
    {
        if (Player.IsOnFloor())
        {
            if (Input.IsActionPressed("Down"))
            {
                Stop();
            }
        }

        GD.Print("Ladder");

        if (Input.IsActionPressed("Up"))
        {
            GD.Print("UP");
            inputVelocity.Y = -1 * currentSpeed;
        }

        if (Input.IsActionPressed("Down"))
        {
            inputVelocity.Y = 1 * currentSpeed;
        }
        else if (!Input.IsAnythingPressed())
        {
            inputVelocity = Vector2.Zero;
        }

        if (obj == Objects.NOTHING)
        {
            GD.Print("Nothing");
            if (Input.IsActionJustPressed("Up"))
            {
                Stop();
            }
        }
        return inputVelocity;
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        GetInput();
        obj = Player.Collision;
        Player.Velocity = inputVelocity;
        GD.Print("Velocity: ", Player.Velocity);
    }

    public override void Stop()
    {
        base.Stop();
        Player.ResetState();
    }
}
