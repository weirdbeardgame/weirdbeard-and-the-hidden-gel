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
        Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard").Visible = false;
        Player.GetNode<Sprite2D>("CenterContainer/ClimbBeard").Visible = true;
        Player.AnimationPlayer.Play("Climb");
        Player.DisableGravity();
    }

    public override Vector2 GetInput()
    {

        switch (Player.LadderState)
        {
            case LadderStates.BEGIN:
                if (Input.IsActionPressed("Down"))
                {
                    Stop();
                }
                break;

            case LadderStates.CLIMBING:
                if (Input.IsActionPressed("Up"))
                {
                    _InputVelocity.Y = -1 * currentSpeed;
                }

                if (Input.IsActionJustPressed("Jump"))
                {

                    // Issue is here. State machine never updates when jump reaches end of ladder
                    if (Input.IsActionPressed("Right"))
                    {
                        _InputVelocity.X = 1;
                        Player.Velocity = _InputVelocity;
                        Player.StateMachine.UpdateState("JUMP");
                    }

                    _InputVelocity.Y = -2 * currentSpeed;

                    if (Player.LadderState == LadderStates.END)
                    {
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

                break;

            case LadderStates.END:
                if (Input.IsActionJustPressed("Up"))
                {
                    Stop();
                }
                break;
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
            Stop();
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
                Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard").Visible = true;
                Player.GetNode<Sprite2D>("CenterContainer/ClimbBeard").Visible = false;
                Player.AnimationPlayer.Play("RESET");
                Player.ResetPlayer();

                break;
            case LadderStates.END:
                Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard").Visible = true;
                Player.GetNode<Sprite2D>("CenterContainer/ClimbBeard").Visible = false;
                Player.AnimationPlayer.Play("RESET");
                // Move the player, then reset gravity
                Player.ResetPlayer();
                break;
        }

    }
}
