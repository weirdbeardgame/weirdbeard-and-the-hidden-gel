using Godot;
using System;

public class Actor : KinematicBody2D
{
    protected StateMachine stateMachine;

    // Movement properties
    [Export] public float gravity;
    [Export] public float speed = 400f;
    [Export] public float runSpeed = 800f;
    protected Vector2 velocity;
    public bool canMove;

    //Jump properties
    [Export] public float maxCoyoteTimer = 2f;
    [Export] public float jumpHeight;
    [Export] public float jumpPeak;
    [Export] public float jumpDescent;

    [Export] public float maxJumpImpulse = 600f;
    [Export] public float minJumpImpulse = 300f;

    public float jumpVelocity;
    public float jumpGravity;
    public float fallGravity;

    public bool canJumpAgain;
    public bool wasOnFloor;
    public bool coyoteTime;

    [Export] public bool projectileMotionJump;
}
