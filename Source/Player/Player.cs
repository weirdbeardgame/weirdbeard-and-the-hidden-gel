using Godot;
using System;

public partial class Player : Actor
{
    Timer _coyoteTimer;
    Timer _bufferedJumpTimer;
    PowerUp _currentPowerup;
    Area2D body;

    public TileCommon map;

    public Sprite2D weirdBeard;
    public int PlayerLives = 3;
    public WeaponCommon CurrentWeapon;
    public AnimationPlayer AnimationPlayer;
    public Vector2 direction = Vector2.Right;

    Camera2D camera;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        stateMachine = (StateMachine)GetNode("StateMachine");
        AnimationPlayer = (AnimationPlayer)GetNode("AnimationPlayer");
        _bufferedJumpTimer = (Timer)GetNode("BufferedJump");

        body = GetNode<Area2D>("ObjectDetect");

        CurrentWeapon = new WeaponCommon();

        _coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.startNewGame += NewGame;

        map = Owner.GetNode<TileCommon>("TileMap");

        camera = (Camera2D)GetNode("Camera2D");

        stateMachine.InitState("IDLE");

        if (projectileMotionJump)
        {
            JumpVelocity = ((2.0f * jumpHeight) / jumpPeak) * -1.0f;
            JumpGravity = ((-2.0f * jumpHeight) / (jumpPeak * jumpPeak)) * -1.0f;
            FallGravity = ((-2.0f * jumpHeight) / (jumpDescent * jumpDescent)) * -1.0f;
        }
    }

    public void NewGame()
    {
        PlayerLives = 3;
        ResetState();
        SceneManager.startNewGame -= NewGame;
    }

    public void ResetState()
    {
        canJumpAgain = true;
        wasOnFloor = false;
        Velocity = Vector2.Zero;
        gravity = defaultGravity;
        SetState("IDLE");
        canMove = true;
    }

    public void SetState(string state)
    {
        if (stateMachine != null)
        {
            stateMachine.UpdateState(state);
        }
    }

    public void StartCoyoteTimer()
    {
        if (_coyoteTimer.IsStopped())
        {
            _coyoteTimer.Start(maxCoyoteTimer);
        }
    }

    public void BufferJump()
    {
        if (_bufferedJumpTimer.IsStopped())
        {
            _bufferedJumpTimer.Start();
        }
    }

    public void ActivateCamera()
    {
        camera.Enabled = true;
        camera.MakeCurrent();
    }

    public void DeactivateCamera()
    {
        camera.Enabled = false;
    }

    public void CenterCamera()
    {
        Vector2 CameraPosition = camera.GlobalPosition;
        CameraPosition.X = GlobalPosition.X;
        camera.Position = CameraPosition;
    }

    public void Die()
    {
        GD.Print("U ded son");
        if (PlayerLives > 0)
        {
            PlayerLives -= 1;
            SceneManager.resetLev();
        }
    }

    public bool CanJump()
    {
        return ((IsOnFloor() || !_coyoteTimer.IsStopped()) || _bufferedJumpTimer.IsStopped() || canJumpAgain);
    }

    // Game Grumps joke
    public bool GamaOvar()
    {
        return (PlayerLives <= 0);
    }

    public void EquipWeapon(PackedScene w, Texture2D weapSprite)
    {
        WeaponCommon weapon = w.Instantiate<WeaponCommon>();
        CurrentWeapon = weapon;

        if (weapSprite != null)
        {
            WeaponSlot.updateWSprite.Invoke(weapSprite);
        }

        if (CurrentWeapon == null)
        {
            GD.PrintErr("Weapon NULL");
        }
    }

    public void EquipPowerup(PowerUp power)
    {
        if (_currentPowerup != power)
        {
            RemoveChild(_currentPowerup);
            AddChild(power);
            _currentPowerup = power;
        }
        else
        {
            return;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Objects collision = (Objects)map.Collided(this);

        if (collision != Objects.NOTHING)
        {
            switch (collision)
            {
                case Objects.LADDER:
                    // Activate ladder state
                    if (Input.IsActionJustPressed("Up"))
                    {
                        stateMachine.UpdateState("LADDER");
                    }
                    break;

                case Objects.SPIKE:
                    stateMachine.UpdateState("DEATH");
                    break;

                case Objects.WATER:
                    // I think my day is going swimmingly!
                    break;
            }
        }
    }

    public void GetAirState()
    {
        if (Velocity.Y > 0.0f)
        {
            if (StateMachine.CurrentStateName != "FALL")
            {
                StateMachine.UpdateState("FALL");
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("Attack") && CurrentWeapon != null)
        {
            CurrentWeapon.Attack(direction.Sign(), GetTree().CurrentScene);
        }

        wasOnFloor = IsOnFloor();
        Velocity = ApplyGravity(delta, gravity);
        MoveAndSlide();

        if (_currentPowerup != null)
        {
            if (_currentPowerup.CanBeActivated() && Input.IsActionJustPressed("Run"))
            {
                _currentPowerup.Activate();
            }
        }
    }
}
