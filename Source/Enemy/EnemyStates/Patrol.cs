using Godot;
using System;

public partial class Patrol : State
{
    RayCast2D Left;
    RayCast2D Right;
    Vector2 _Dir;

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
        Left = (RayCast2D)Enemy.GetNode("Left");
        Right = (RayCast2D)Enemy.GetNode("Right");
        _Dir = new Vector2();
        base.Start();
    }

    public void ChangeDirection(EnemyDirection dirToWalk)
    {
        Enemy.Sprite = (Sprite2D)Enemy.GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                Enemy.Sprite.FlipH = false;
                _Dir.X = -1.0f;
                break;

            case EnemyDirection.RIGHT:
                Enemy.Sprite.FlipH = true;
                _Dir.X = 1.0f;
                break;
        }
    }

    public override Vector2 GetInput()
    {
        Vector2 _InputVelocity = new Vector2();

        if (!Right.IsColliding())
        {
            ChangeDirection(EnemyDirection.LEFT);
        }
        else if (!Left.IsColliding())
        {
            ChangeDirection(EnemyDirection.RIGHT);
        }

        _InputVelocity.X = _Dir.X * Enemy.Speed;

        //GD.Print("Velocity: ", inputVelocity);
        return _InputVelocity;
    }

    bool DetectPlayer()
    {
        if (Player != null)
        {
            var Offset = Player.Position - Enemy.Position;

            if (Offset.X < Enemy.MaxDetectDistance)
            {
                return true;
            }
        }
        else
        {
            Player = Enemy.GetParent<LevelCommon>().GetNode<Player>("Player");
        }

        return false;
    }

    public override void FixedUpdate(double delta)
    {
        Enemy.Velocity = GetInput();
    }
}
