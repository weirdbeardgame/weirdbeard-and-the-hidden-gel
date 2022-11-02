using Godot;
using System;

public class HoverBeard : PowerUp
{

    Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "HOVER";
        timer = (Timer)GetNode("Timer");
    }

    public override void Start()
    {
        if (!player.IsOnFloor())
        {
            player.gravity = gravity;
            timer.Start();
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
        inputVelocity.x = speed * GetInput().x;
        player.Velocity = inputVelocity;
    }

    public void OnTimeout()
    {
        Stop();
    }

    public override void Stop()
    {
        base.Stop();
    }

}
