using Godot;
using System;

enum LadderStates { BEGIN = 0, CLIMBING = 1, END = 2 };

public partial class Ladder : State
{
    Vector2 inputVelocity = Vector2.Zero;
    [Export] float currentSpeed = 0;

    LadderStates LadderState;

    public override void _Ready()
    {
        StateName = "LADDER";
        Player = (Player)GetParent<Player>();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
        base._Ready();
    }
    public override void Start()
    {
        base.Start();
        Player.Gravity = 0;
    }

    public override Vector2 GetInput()
    {
        if (Player.IsOnFloor() || LadderState == LadderStates.BEGIN)
        {
            if (Input.IsActionPressed("Down"))
            {
                Stop();
            }
        }

        if (Input.IsActionPressed("Up"))
        {
            inputVelocity.Y = -1 * currentSpeed;

            if (LadderState == LadderStates.END)
            {
                GD.Print("Nothing");
                Stop();
            }
        }

        if (Input.IsActionPressed("Down"))
        {
            inputVelocity.Y = 1 * currentSpeed;
        }
        else if (!Input.IsAnythingPressed())
        {
            inputVelocity = Vector2.Zero;
        }
        return inputVelocity;
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        GetInput();
        LadderState = (LadderStates)Player.Map.Collided(Player, "LadderEvent");
        GD.Print("State: ", LadderState.ToString());
        Player.Velocity = inputVelocity;
        GD.Print("Velocity: ", Player.Velocity);
    }

    public override void Stop()
    {
        base.Stop();
        LadderState = LadderStates.BEGIN;
        Player.ResetState();
    }
}
