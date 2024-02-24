using Godot;
using System;

public partial class Actor : CharacterBody2D
{
    protected StateMachine _StateMachine;

    public StateMachine StateMachine
    {
        get
        {
            return _StateMachine;
        }
    }

    // Movement properties
    public float Gravity;
    [Export] protected float DefaultGravity = 400;
    [Export] public float Speed = 400f;
    [Export] public float RunSpeed = 800f;
    public bool CanMove;

    //Jump properties
    [Export] public float maxJumpImpulse = 600f;
    [Export] public float minJumpImpulse = 300f;
    [Export] public bool projectileMotionJump;
    [Export] public float maxCoyoteTimer = 2f;
    [Export] public float jumpDescent;
    [Export] public float jumpHeight;
    [Export] public float jumpPeak;

    public float JumpVelocity;
    public float JumpGravity;
    public float FallGravity;

    public int NumJumps = 2;
    public bool wasOnFloor;

    public Sprite2D Sprite;
}
