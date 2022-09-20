using Godot;
using System;

public class Falling : State
{
    bool doubleJump = true;

    Vector2 inputVelocity;

    public override void _Ready()
    {
        stateName = "FALL";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        player.player.Play("Fall");
        if (player.projectileMotionJump)
        {
            player.gravity = player.fallGravity;
            GD.Print("Fall Gravity: ", player.gravity);
        }
    }

    public override void FixedUpdate(float delta)
    {
        if (Input.IsActionJustPressed("Jump") && player.canJumpAgain)
        {
            player.canJumpAgain = false;
            stateMachine.UpdateState("JUMP");
        }

        if (player.IsOnFloor())
        {
            player.ResetState();
        }
    }

    public override void Stop()
    {
        player.player.Stop(false);
    }
}
