using Godot;
using System;

public partial class Patrol : State
{
    RayCast2D _left;
    RayCast2D _right;

    public override void _Ready()
    {
        StateName = "PATROL";
        Enemy = GetParent<Enemy>();
        StateMachine = Enemy.GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
        base._Ready();
    }

    public override void Start()
    {
        _left = (RayCast2D)Enemy.GetNode("Left");
        _right = (RayCast2D)Enemy.GetNode("Right");
        Enemy.Direction = new Vector2();
        base.Start();
    }

    public void ChangeDirection(EnemyDirection dirToWalk)
    {
        Enemy.Sprite = (Sprite2D)Enemy.GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                Enemy.Sprite.FlipH = false;
                Enemy.Direction.X = -1.0f;
                break;

            case EnemyDirection.RIGHT:
                Enemy.Sprite.FlipH = true;
                Enemy.Direction.X = 1.0f;
                break;
        }
    }

    public override Vector2 GetInput()
    {
        Vector2 _inputVelocity = new Vector2();

        if (!_right.IsColliding())
        {
            ChangeDirection(EnemyDirection.LEFT);
        }
        else if (!_left.IsColliding())
        {
            ChangeDirection(EnemyDirection.RIGHT);
        }

        _inputVelocity.X = Enemy.Direction.X * Enemy.Speed;

        //GD.Print("Velocity: ", _inputVelocity);
        return _inputVelocity;
    }

    public override void FixedUpdate(double delta)
    {
        Enemy.Velocity = GetInput();

        if (Enemy.PlayerDetected)
        {
            StateMachine.UpdateState("ATTACK");
        }
    }
}
