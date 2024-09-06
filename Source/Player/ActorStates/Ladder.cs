using Godot;
using System;



public partial class Ladder : State
{
    Vector2 _InputVelocity = Vector2.Zero;
    [Export] float currentSpeed = 0;

    public override void _Ready()
    {
        StateName = "LADDER";
        Player = (Player)GetParent<Player>();
        Player.DisableGravity();
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
        base._Ready();
    }

    public override void Start()
    {
        base.Start();
        Player.DisableGravity();
    }

    public override Vector2 GetInput()
    {
        if (Player.LadderState == LadderStates.CLIMBING)
        {
            if (Player.IsOnFloor() || Player.LadderState == LadderStates.BEGIN)
            {
                if (Input.IsActionPressed("Down"))
                {
                    Stop();
                }
            }

            if (Input.IsActionPressed("Up"))
            {
                _InputVelocity.Y = -1 * currentSpeed;

                if (Player.LadderState == LadderStates.END)
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
        }
        return _InputVelocity;
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        GetInput();

        // At the top. Tell the player to start looking for the ground.
        if (Player.LadderState == LadderStates.END)
        {
            Player.DetectPlatform();
        }

        GD.Print("State: ", Player.LadderState.ToString());
        Player.Velocity = _InputVelocity;
        GD.Print("Velocity: ", Player.Velocity);
    }

    public override void Stop()
    {
        base.Stop();
        // TO DO: get off ladder correctly.

        switch (Player.LadderState)
        {
            case LadderStates.BEGIN:
                break;
            case LadderStates.END:

                Player.ResetPlayer();
                break;
        }

    }
}
