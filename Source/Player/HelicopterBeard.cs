using Godot;
using System;

public class HelicopterBeard : State
{

    Vector2 inputVelocity = Vector2.Zero;

    [Export]
    float gravity = 2500;

    float currentSpeed = 300f;

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
        inputVelocity.x = currentSpeed * GetInput().x;

        if (player.IsOnFloor())
        {
            player.ResetState();
        }

        player.Velocity = inputVelocity;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
