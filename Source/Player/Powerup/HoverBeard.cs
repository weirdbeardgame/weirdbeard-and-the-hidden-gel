using Godot;
using System;

public class HoverBeard : PowerUp
{
    Timer timer;

    Sprite hoverBeard;

    bool isHover;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "HOVER";
        timer = (Timer)GetNode("Timer");
    }

    public override void Start()
    {
        hoverBeard = (Sprite)player.GetNode("CenterContainer/HoverBeard");

        weirdBeard.Visible = false;
        hoverBeard.Visible = true;

        if (!player.IsOnFloor())
        {
            player.gravity = gravity;
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
            inputVelocity.x = 1.0f * speed;
        }
        else if (Input.IsActionPressed("Left"))
        {
            inputVelocity.x = -1.0f * speed;
        }
        if (!Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
        {
            inputVelocity.x = 0;
        }
        return inputVelocity;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (isHover)
        {
            player.Velocity = GetInput();
        }
    }

    public void OnTimeout()
    {
        Stop();
    }

    public override void Stop()
    {
        animator.Play("Hover_End");
        weirdBeard.Visible = true;
        hoverBeard.Visible = false;
        isHover = false;
        base.Stop();
    }

}
