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
        player.player.Play("Heli_Jump");
        player.gravity = (player.gravity * gravityPercent);
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

    public override void PlayAnimation()
    {
        player.player.Play("Heli_Jump");
    }

    public override void FixedUpdate(float delta)
    {
        player.Velocity = GetInput();

        GD.Print(player.Velocity);

        if (player.IsOnFloor())
        {
            Stop();
        }
    }

    public override void Stop()
    {
        player.player.Play("Heli_Fall");
    }

}
