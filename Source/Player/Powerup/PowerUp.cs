using Godot;
using System;

public partial class PowerUp : State
{
    Sprite2D powerupSprite;

    [Export] protected float gravity;

    [Export] protected float speed;

    protected Vector2 inputVelocity;

    bool isActivated;
    bool wasActivated;

    public Timer regenTimer;

    public AnimationPlayer playerAnimator;

    public bool CanBeActivated()
    {
        if (!wasActivated && regenTimer.TimeLeft <= 0)
        {
            return true;
        }

        if (!Player.IsOnFloor() && !wasActivated && !isActivated)
        {
            return true;
        }

        if (wasActivated && regenTimer.TimeLeft <= 0)
        {
            wasActivated = false;
        }

        if (isActivated)
        {
            return false;
        }

        return false;
    }

    public void Activate()
    {
        if (!isActivated)
        {
            Player.ResetState();
            stateMachine.UpdateState(StateName);
            playerAnimator.Play(StateName);
            isActivated = true;
        }
    }

    public override void Stop()
    {
        isActivated = false;
        Player.ResetState();
        regenTimer.Start();
    }
}
