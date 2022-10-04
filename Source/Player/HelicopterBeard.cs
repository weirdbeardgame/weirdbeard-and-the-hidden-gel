using Godot;
using System;

public class HelicopterBeard : PowerUp
{

    [Export] float gravityPercent;
    public override void _Ready()
    {
        stateName = "HELICOPTER";
    }

    // Play animation. Set physics
    public override void Start()
    {
        player.gravity = (player.gravity * gravityPercent);
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {
        inputVelocity.x = speed * GetInput().x;
        player.Velocity = inputVelocity;

        if (player.IsOnFloor())
        {
            Stop();
        }

    }
}
