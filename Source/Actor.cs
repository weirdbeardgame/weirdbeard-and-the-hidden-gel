using Godot;
using System;

public class Actor : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    protected Vector2 velocity;

    [Export]
    public float speed = 400.0f;

    [Export]
    public float jumpForce = 1200f;

    protected StateMachine stateMachine;

    [Export]
    public float gravity = 4000f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }
}
