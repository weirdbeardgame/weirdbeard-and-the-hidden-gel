using Godot;
using System;

public partial class HoverBeard : PowerUp
{
    Timer timer;

    Sprite2D hoverBeard;

    bool isHover;

    public override void _Ready()
    {
        StateName = "HOVER";
        timer = (Timer)GetNode("Timer");
        Player = (Player)GetParent<Player>();
        weirdBeard = Player.GetNode<Sprite2D>("CenterContainer/WeirdBeard");
        hoverBeard = Player.GetNode<Sprite2D>("CenterContainer/HoverBeard");
        animator = Player.AnimationPlayer;
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");

    }

    public override void Start()
    {
        weirdBeard.Visible = false;
        hoverBeard.Visible = true;

        if (!Player.IsOnFloor())
        {
            Player.gravity = gravity;
            animator.Play("Hover_Start");
            timer.Start();
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
            inputVelocity.X = 1.0f * speed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.X = -1.0f * speed;
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

    public void OnTimeout()
    {
        Stop();
    }

    public override void Stop()
    {
        GD.Print("STOP HOVER");
        animator.Play("Hover_End");
        weirdBeard.Visible = true;
        hoverBeard.Visible = false;
        isHover = false;
        timer.Stop();
        base.Stop();
    }

}
