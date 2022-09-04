using Godot;
using System;

public class HelicopterBeard : PowerUp
{
    public override void _Ready()
    {
        stateName = "HELICOPTER";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
        animator = (AnimationPlayer)GetNode("AnimationPlayer");
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

    public override void Exit()
    {
        base.Exit();
    }
}
