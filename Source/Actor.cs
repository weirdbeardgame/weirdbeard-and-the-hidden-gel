using Godot;
using System;

public partial class Actor : CharacterBody2D
{
    protected StateMachine stateMachine;

    public StateMachine StateMachine
    {
        get
        {
            return stateMachine;
        }
    }

    // Movement properties
    public float gravity;
    [Export] protected float defaultGravity = 400;
    [Export] public float speed = 400f;
    [Export] public float runSpeed = 800f;
    public bool canMove;

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
}
