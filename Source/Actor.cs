using Godot;
using System;

public class Actor : KinematicBody2D
{
    protected Vector2 velocity;

    [Export]
    public float speed = 400.0f;

    [Export]
    public float jumpForce = 900f;

    protected StateMachine stateMachine;

    [Export]
    public float gravity = 4000f;

    public bool canJumpAgain;
}
