using Godot;
using System;

public class HelicopterBeard : PowerUp
{
    public override void _Ready()
    {
        stateName = "HELICOPTER";
    }

    // Play animation. Set physics
    public override void Start()
    {
        player.gravity = gravity;
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

    public override void Stop()
    {
        base.Stop();
    }
}
