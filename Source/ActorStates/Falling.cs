using Godot;
using System;

public partial class Falling : State
{
    bool doubleJump = true;

    Vector2 inputVelocity;

    public override void _Ready()
    {
        stateName = "FALL";
        Player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override void Start()
    {
        Player.AnimationPlayer.Play("Fall");
        if (Player.wasOnFloor)
        {
            Player.StartCoyoteTimer();
        }
        if (Player.projectileMotionJump)
        {
            Player.gravity = Player.FallGravity;
            GD.Print("Fall Gravity: ", Player.gravity);
        }
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if (Input.IsActionJustPressed("Jump") && Player.CanJump())
        {
            Player.canJumpAgain = false;
            stateMachine.UpdateState("JUMP");
        }
        if (Player.IsOnFloor())
        {
            Player.ResetState();
        }
    }

    public override void Stop()
    {
        Player.AnimationPlayer.Stop(false);
    }
}
