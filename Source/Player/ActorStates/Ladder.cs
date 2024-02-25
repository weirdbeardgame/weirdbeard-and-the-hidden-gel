using Godot;
using System;

enum LadderStates { BEGIN = 0, CLIMBING = 1, END = 2 };

public partial class Ladder : State
{
    Vector2 _InputVelocity = Vector2.Zero;
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
            _InputVelocity.Y = -1 * currentSpeed;

            if (LadderState == LadderStates.END)
            {
                GD.Print("Nothing");
                Stop();
            }
        }

        if (Input.IsActionPressed("Down"))
        {
            _InputVelocity.Y = 1 * currentSpeed;
        }
        else if (!Input.IsAnythingPressed())
        {
            _InputVelocity = Vector2.Zero;
        }
        return _InputVelocity;
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        GetInput();
        LadderState = (LadderStates)Player.Map.Collided(Player, "LadderEvent");
        GD.Print("State: ", LadderState.ToString());
        Player.Velocity = _InputVelocity;
        GD.Print("Velocity: ", Player.Velocity);
    }

    public override void Stop()
    {
        base.Stop();
        LadderState = LadderStates.BEGIN;
        Player.ResetState();
    }
}
