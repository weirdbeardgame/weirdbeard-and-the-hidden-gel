using Godot;
using System;

public partial class HelicopterBeard : PowerUp
{
    [Export] float gravityPercent;

    Sprite2D helicopter;

    public override void _Ready()
    {
        StateName = "HELICOPTER";
        regenTimer = GetNode<Timer>("RegenTimer");
        Player = (Player)GetParent<Player>();
        weirdBeard = Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard");
        helicopter = Player.GetNode<Sprite2D>("CenterContainer/HelicopterBeard");
        animator = Player.AnimationPlayer;
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
    }

    // Play animation. Set physics
    public override void Start()
    {
        GD.Print("Helicopter");
        weirdBeard.Visible = false;
        helicopter.Visible = true;

        animator.Play("Heli_start");
        Player.Gravity = (Player.Gravity * gravityPercent);
        animator.Play("Heli_Loop");
    }

    public override Vector2 GetInput()
    {
        if (Input.IsActionPressed("Right"))
        {
            inputVelocity.X = 1.0f * Speed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.X = -1.0f * Speed;
        }
        if (!Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
        {
            inputVelocity.X = 0;
        }
        return inputVelocity;
    }

    public override void FixedUpdate(double delta)
    {
        Player.Velocity = GetInput();

        GD.Print(Player.Velocity);

        if (Player.IsOnFloor())
        {
            Stop();
        }
    }

    public override void Stop()
    {
        animator.Play("Heli_End");
        helicopter.Visible = false;
        weirdBeard.Visible = true;
        base.Stop();
    }
}
