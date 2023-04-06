using Godot;
using System;

public partial class Player : Actor
{
    Timer coyoteTimer;
    Timer bufferedJumpTimer;
    PowerUp currentPowerup;
    Area2D body;

    public TileCommon map;

    public Sprite2D weirdBeard;
    public int playerLives = 3;
    public WeaponCommon currentWeapon;
    public AnimationPlayer player;
    public Vector2 direction = Vector2.Right;

    Camera2D camera;

    [Export] float defaultGravity = 400;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        weirdBeard = (Sprite2D)GetNode("CenterContainer/WeirdBeard");
        stateMachine = (StateMachine)GetNode("StateMachine");
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        bufferedJumpTimer = (Timer)GetNode("BufferedJump");

        body = GetNode<Area2D>("ObjectDetect");

        currentWeapon = new WeaponCommon();

        coyoteTimer = (Timer)GetNode("CoyoteTimer");
        SceneManager.startNewGame += NewGame;

        map = Owner.GetNode<TileCommon>("TileMap");

        camera = (Camera2D)GetNode("Camera2D");

        stateMachine.InitState("IDLE");

        if (projectileMotionJump)
        {
            jumpVelocity = ((2.0f * jumpHeight) / jumpPeak) * -1.0f;
            jumpGravity = ((-2.0f * jumpHeight) / (jumpPeak * jumpPeak)) * -1.0f;
            fallGravity = ((-2.0f * jumpHeight) / (jumpDescent * jumpDescent)) * -1.0f;
        }
    }

    public void NewGame()
    {
        playerLives = 3;
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
        if (coyoteTimer.IsStopped())
        {
            coyoteTimer.Start(maxCoyoteTimer);
        }
    }

    public void BufferJump()
    {
        if (bufferedJumpTimer.IsStopped())
        {
            bufferedJumpTimer.Start();
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
        if (playerLives > 0)
        {
            playerLives -= 1;
            SceneManager.resetLev();
        }
    }

    public bool CanJump()
    {
        return ((IsOnFloor() || !coyoteTimer.IsStopped()) || bufferedJumpTimer.IsStopped() || canJumpAgain);
    }

    // Game Grumps joke
    public bool GamaOvar()
    {
        return (playerLives <= 0);
    }

    public void EquipWeapon(PackedScene w, Sprite2D weapSprite)
    {
        WeaponCommon weapon = w.Instantiate<WeaponCommon>();
        currentWeapon = weapon;

        if (weapon.weaponType == WeaponType.SHOOT)
        {
            AddChild(weapon);

            weapon.GlobalPosition = GlobalPosition + new Vector2(20, 0);
            weapon.GlobalRotation = GlobalRotation;
        }
        if (weapSprite != null)
        {
            WeaponSlot.updateWSprite.Invoke(weapSprite.Texture);
        }

        if (currentWeapon == null)
        {
            GD.PrintErr("Weapon NULL");
        }
    }

    public void EquipPowerup(PowerUp power)
    {
        if (currentPowerup != power)
        {
            RemoveChild(currentPowerup);
            AddChild(power);
            currentPowerup = power;
        }
        else
        {
            return;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Objects collision = map.Collided(this);

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
                    Die();
                    break;

                case Objects.WATER:
                    // I think my day is going swimmingly!
                    break;
            }
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (Input.IsActionJustPressed("Attack") && currentWeapon != null)
        {
            currentWeapon.Attack(direction.Sign());
        }

        wasOnFloor = IsOnFloor();
        Velocity = ApplyGravity(delta, gravity);
        MoveAndSlide();

        if (currentPowerup != null)
        {
            if (currentPowerup.CanBeActivated() && Input.IsActionJustPressed("Run"))
            {
                currentPowerup.Activate();
            }
        }
    }
}
