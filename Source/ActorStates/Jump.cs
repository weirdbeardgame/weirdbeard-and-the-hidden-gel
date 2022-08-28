using Godot;
using System;

public class Jump : State
{
    Vector2 InputVelocity = Vector2.Zero;

    public override void Start()
    {
        player.player.Play("Jump");
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "JUMP";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {
        player.Velocity += GetInput();
        if (player.IsOnFloor() == false && !Input.IsActionJustPressed("Jump"))
        {
            if (Input.IsActionJustPressed("Jump") && player.canJumpAgain)
            {
                player.canJumpAgain = false;
                stateMachine.ResetState();
            }
            stateMachine.UpdateState("FALL");
        }
    }

    public override void Exit()
    {
        player.player.Stop(true);
    }
}
