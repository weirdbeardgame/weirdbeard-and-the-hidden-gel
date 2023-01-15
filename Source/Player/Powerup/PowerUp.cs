using Godot;
using System;

public class PowerUp : State
{
    Sprite powerupSprite;

    [Export] protected float gravity;

    [Export] protected float speed;

    protected Vector2 inputVelocity;

    bool isActivated;
    bool wasActivated;

    public Timer regenTimer;

    public AnimationPlayer playerAnimator;

    public virtual void Equip(Player p)
    {
        player = p;
        GD.Print("Equip");
        playerAnimator = player.player;
        regenTimer = (Timer)GetNode("RegenTimer");
        animator = (AnimationPlayer)player.GetNode("AnimationPlayer");
        weirdBeard = (Sprite)player.GetNode("CenterContainer/WeirdBeard");
        stateMachine = player.StateMachine;
    }

    public bool CanBeActivated()
    {
        if (!wasActivated && regenTimer.TimeLeft <= 0)
        {
            return true;
        }

        if (!player.IsOnFloor() && !wasActivated && !isActivated)
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
            player.ResetState();
            stateMachine.UpdateState(stateName);
            playerAnimator.Play(stateName);
            isActivated = true;
        }
    }

    public override void Stop()
    {
        isActivated = false;
        player.ResetState();
        regenTimer.Start();
    }
}
