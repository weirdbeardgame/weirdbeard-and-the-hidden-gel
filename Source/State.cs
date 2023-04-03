using Godot;
using System;

public partial class State : Node
{
    protected Player player;

    protected StateMachine stateMachine;

    public string stateName;

    protected Sprite2D weirdBeard;

    protected AnimationPlayer animator;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    public virtual void Start()
    {

    }

    public virtual void Stop()
    {
    }

    public virtual Vector2 GetInput()
    {
        return Vector2.Zero;
    }

    public virtual void Update(double delta)
    {
        Objects collision = player.map.Collided(player);

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
                    player.Die();
                    break;

                case Objects.WATER:
                    // I think my day is going swimmingly!
                    break;
            }
        }
    }

    public virtual void FixedUpdate(double delta)
    {

    }
}
