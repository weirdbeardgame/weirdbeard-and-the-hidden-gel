using Godot;
using System;

public class HoverBeard : PowerUp
{

    Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        stateName = "HOVER";
        player = (Player)GetParent<Player>();
        stateMachine = (StateMachine)GetParent<Player>().GetNode<StateMachine>("StateMachine");
        stateMachine.AddState(this, stateName);
        animator = (AnimationPlayer)GetNode("AnimationPlayer");
    }

    public override void Start()
    {
        if (!player.IsOnFloor())
        {
            player.gravity = gravity;
        }
        else
        {
            Exit();
        }
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

    public void OnTimeout()
    {
        Exit();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
