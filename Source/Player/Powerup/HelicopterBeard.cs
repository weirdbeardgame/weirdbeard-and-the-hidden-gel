using Godot;
using System;

public class HelicopterBeard : PowerUp
{
    [Export] float gravityPercent;

    Sprite helicopter;

    public override void Equip(Player p)
    {
        helicopter = (Sprite)p.GetNode("CenterContainer/HelicopterBeard");
        stateName = "HELICOPTER";
        base.Equip(p);
    }

    // Play animation. Set physics
    public override void Start()
    {
        weirdBeard.Visible = false;
        helicopter.Visible = true;

        animator.Play("Heli_start");
        player.gravity = (player.gravity * gravityPercent);
        animator.Play("Heli_Loop");
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
        animator.Play("Heli_End");
        helicopter.Visible = false;
        weirdBeard.Visible = true;
        base.Stop();
    }

}
