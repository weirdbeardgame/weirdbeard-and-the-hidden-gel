using Godot;
using System;

public partial class HoverBeard : PowerUp
{

    Sprite2D hoverBeard;

    bool isHover;

    public override void _Ready()
    {
        StateName = "HOVER";
        regenTimer = (Timer)GetNode("RegenTimer");
        Player = (Player)GetParent<Player>();
        weirdBeard = Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard");
        hoverBeard = Player.GetNode<Sprite2D>("CenterContainer/HoverBeard");
        animator = Player.AnimationPlayer;
        StateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        regenTimer.Timeout += Stop;

    }

    public override void Start()
    {
        weirdBeard.Visible = false;
        hoverBeard.Visible = true;

        if (!Player.IsOnFloor())
        {
            Player.Gravity = Gravity;
            animator.Play("Hover_Start");
            regenTimer.Start();
            animator.Play("Hover_Loop");
            isHover = true;
        }
        else
        {
            Stop();
        }
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

    public override void _PhysicsProcess(double delta)
    {
        if (isHover && Input.IsActionPressed("Run"))
        {
            Player.Velocity = GetInput();
        }
        if (!Input.IsActionPressed("Run") && isHover)
        {
            Stop();
        }
    }

    public override void Stop()
    {
        animator.Play("Hover_End");
        weirdBeard.Visible = true;
        hoverBeard.Visible = false;
        isHover = false;
        regenTimer.Stop();
        base.Stop();
    }

}
