using Godot;
using System;

public class HelicopterBeard : State
{

    [Export]
    float gravity = 1000;

    public override void _Ready()
    {
        StateName = "HELICOPTER";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);
    }

    // Play animation. Set physics
    public override void Start()
    {
        StateName = "HELICOPTER";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, StateName);

        player.gravity = gravity;
    }

    public override Vector2 GetInput()
    {
        return base.GetInput();
    }

    public override void FixedUpdate(float delta)
    {
        base.FixedUpdate(delta);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
