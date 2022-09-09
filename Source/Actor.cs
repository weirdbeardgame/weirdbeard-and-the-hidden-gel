using Godot;
using System;

public class Actor : KinematicBody2D
{
    protected StateMachine stateMachine;

    // Movement properties
    [Export] public float gravity = 4000f;
    [Export] public float speed = 400f;
    [Export] public float runSpeed = 800f;
    protected Vector2 velocity;

    //Jump properties
    [Export] public float maxCoyoteTimer = 2f;
    [Export] public float jumpForce = 900f;
    public bool canJumpAgain;
    public bool wasOnFloor;
    public bool coyoteTime;
}
