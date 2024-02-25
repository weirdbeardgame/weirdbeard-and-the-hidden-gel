using Godot;
using System;

public partial class Actor : CharacterBody2D
{
    private StateMachine _stateMachine;

    public StateMachine StateMachine
    {
        get
        {
            return _stateMachine;
        }
    }

    // Movement properties
    private float _gravity;
    [Export] protected float DefaultGravity = 400;
    [Export] public float Speed = 400f;
    [Export] public float RunSpeed = 800f;
    public bool CanMove;

    //Jump properties
    [Export] public float maxJumpImpulse = 600f;
    [Export] public float minJumpImpulse = 300f;
    [Export] public bool projectileMotionJump;
    [Export] public float maxCoyoteTimer = 2f;
    [Export] public float JumpTimeToDescent;
    [Export] public float JumpTimeToPeak;
    [Export] public float jumpHeight;

    public Vector2 Direction = Vector2.Right;

    public float JumpVelocity;
    public float JumpGravity;
    public float FallGravity;

    public int NumJumps = 2;
    public bool wasOnFloor;
    public Sprite2D Sprite;

    public override void _Ready()
    {
        base._Ready();
        _stateMachine = (StateMachine)GetNode("StateMachine");
    }

    protected float GetGravity()
    {
        if (projectileMotionJump)
        {
            if (Velocity.Y > 0.0)
            {
                return FallGravity;
            }
            return JumpGravity;
        }
        return DefaultGravity;
    }

    public float Gravity
    {
        get
        {
            return _gravity;
        }
        set
        {
            if (value > -1)
            {
                _gravity = value;
            }
        }
    }

}
