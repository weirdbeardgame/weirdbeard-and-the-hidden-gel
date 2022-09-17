using Godot;
using System;

public class Air : State
{
    Vector2 inputVelocity;
    public override void _Ready()
    {
        stateName = "AIR";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        if (player.projectileMotionJump)
        {
            player.gravity = player.jumpGravity;
            GD.Print("jump Gravity: ", player.gravity);
        }
    }

    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionJustPressed("Jump") && player.canJumpAgain)
        {
            player.canJumpAgain = false;
            stateMachine.ResetToOldState();
        }

        /*if (!player.projectileMotionJump)
        {
            if (player.Velocity.y < player.minJumpImpulse && Input.IsActionJustReleased("Jump") || !Input.IsActionPressed("Jump"))
            {
                inputVelocity.y += player.minJumpImpulse;
                player.Velocity = inputVelocity;
            }
        }*/

        if (player.Velocity.y > 0)
        {
            stateMachine.UpdateState("FALL");
        }

    }

    public override void Stop()
    {
        player.player.Stop(false);
    }
}
