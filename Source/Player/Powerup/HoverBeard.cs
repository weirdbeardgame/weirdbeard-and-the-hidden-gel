using Godot;
using System;

public class HoverBeard : PowerUp
{
    Timer timer;

    Sprite hoverBeard;

    bool isHover;

    public override void Equip(Player p)
    {
        stateName = "HOVER";
        timer = (Timer)GetNode("Timer");
        hoverBeard = (Sprite)p.GetNode("CenterContainer/HoverBeard");
        base.Equip(p);
    }

    public override void Start()
    {
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
        if (isHover && Input.IsActionPressed("Run"))
        {
            player.Velocity = GetInput();
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
