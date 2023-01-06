using Godot;
using System;

public class HoverBeard : PowerUp
{
    Timer timer;

    Sprite hoverBeard;

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
            timer.Start();
            animator.Play("Hover_Start");
        }
        else
        {
            Stop();
        }
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {
        if (!animator.PlaybackActive)
        {
            animator.Play("Hover_Loop");
        }
        inputVelocity.x = speed * GetInput().x;
        player.Velocity = inputVelocity;
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
        base.Stop();
    }

}
