using Godot;
using System;


public class Player : Actor
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    public AnimationPlayer player;

    bool canThrowWeapon = true;

    WeaponSlot equipped;


    [Export]
    public float maxCoyoteTimer = 2f;

    public bool coyoteTime;

    Timer timer;

    public int lives = 0;

    public Sprite weirdBeard;

    public bool wasOnFloor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        stateMachine = (StateMachine)GetNode("StateMachine");
        stateMachine.UpdateState("IDLE");
        weirdBeard = (Sprite)GetNode("WeirdBeard");
        timer = (Timer)GetNode("Timer");
        lives = 3;
    }

    public Vector2 Velocity
    {
        get
        {
            return velocity;
        }

        set
        {
            velocity = value;
        }
    }

    public void SetState(string state)
    {
        stateMachine.UpdateState(state);
    }

    public void StartCoyoteTimer()
    {
        timer.Start(maxCoyoteTimer);
        coyoteTime = true;
    }

    public void StopCoyoteTimer()
    {
        if (!timer.IsStopped())
        {
            timer.Stop();
        }
        coyoteTime = false;
    }

    public bool CanJump()
    {
        return IsOnFloor() || coyoteTime;
    }


    public override async void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("Attack") && canThrowWeapon)
        {
            if (equipped == null)
            {
                string path = GetPath();
                GD.Print("Path: " + path);
            }

            Weapon w = (Weapon)equipped.Weapon.Instance();

            w.Position = GlobalPosition;
            //w.Rotation = GlobalRotation;

            Owner.AddChild(w);

            w.Throw(velocity.x, delta);
            canThrowWeapon = false;
            await ToSignal(GetTree().CreateTimer(w.fireRate), "timeout");
            canThrowWeapon = true;
        }

        wasOnFloor = IsOnFloor();
        velocity.y += gravity * delta;
        velocity = MoveAndSlide(velocity, Vector2.Up);


        if (wasOnFloor && !IsOnFloor())
        {
            StartCoyoteTimer();
        }
        else if (IsOnFloor() || timer.IsStopped())
        {
            StopCoyoteTimer();
        }
    }
}
