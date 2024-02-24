using Godot;
using System;

public partial class Patrol : State
{
    RayCast2D Left;
    RayCast2D Right;
    Vector2 _Dir;
    Vector2 _InputVelocity;

    Enemy _Parent;

    public override void _Ready()
    {
        StateName = "PATROL";
        _Parent = GetParent<Enemy>();
        StateMachine = _Parent.GetNode<StateMachine>("StateMachine");
        StateMachine.AddState(this, StateName);
        base._Ready();
    }

    public override void Start()
    {
        Left = (RayCast2D)_Parent.GetNode("Left");
        Right = (RayCast2D)_Parent.GetNode("Right");
        _Dir = new Vector2();
        base.Start();
    }

    public void ChangeDirection(EnemyDirection dirToWalk)
    {
        _Parent.Sprite = (Sprite2D)GetNode("Enemy");
        switch (dirToWalk)
        {
            case EnemyDirection.LEFT:
                _Parent.Sprite.FlipH = false;
                _Dir.X = -1.0f;
                break;

            case EnemyDirection.RIGHT:
                _Parent.Sprite.FlipH = true;
                _Dir.X = 1.0f;
                break;
        }
    }

    public override Vector2 GetInput()
    {
        if (!Right.IsColliding())
        {
            ChangeDirection(EnemyDirection.LEFT);
        }
        else if (!Left.IsColliding())
        {
            ChangeDirection(EnemyDirection.RIGHT);
        }

        _InputVelocity.X = _Dir.X * _Parent.Speed;

        //GD.Print("Velocity: ", inputVelocity);

        return _InputVelocity;
    }



    public override void FixedUpdate(double delta)
    {

    }
}
