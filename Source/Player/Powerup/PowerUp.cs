using Godot;
using System;

public class PowerUp : State
{
    Sprite powerupSprite;

    [Export]
    protected float gravity;

    [Export]
    protected float speed;

    protected Vector2 inputVelocity;

    bool isActivated;

    AnimationPlayer playerAnimator;

    public void Equip(object body)
    {
        GD.Print("Equip");
        if (body is Player)
        {
            player = (Player)body;
            playerAnimator = player.player;
            player.EquipPowerup(this);
            weirdBeard = (Sprite)player.GetNode("CenterContainer/WeirdBeard");
            animator = (AnimationPlayer)player.GetNode("AnimationPlayer");
            stateMachine = (StateMachine)player.GetNode("StateMachine");
            stateMachine.AddState(this, stateName);
        }
    }

    public virtual void PlayAnimation()
    {

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
    }
}
