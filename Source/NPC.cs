using Godot;
using System;
using Godot.Collections;

public partial class NPC : Node
{
    [Export]
    string npcName;

    AnimationPlayer player;

    [Export]
    Array<PackedScene> dialogue;

    bool isPlayerCollide;

    bool isOpen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        player.Play("IDLE");
        isPlayerCollide = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (isPlayerCollide && Input.IsActionPressed("Submit"))
        {
            if (!isOpen)
            {
                GD.Print("Dialogue Colision");
                isOpen = true;
                Owner.GetNode<InterfaceManager>("InterfaceManager").Open(dialogue);
            }
        }
    }

    public void onBodyEntered(object body)
    {
        if (body is Player)
        {
            isPlayerCollide = true;
        }
    }

    public void onBodyExited(object body)
    {
        if (body is Player)
        {
            isPlayerCollide = false;
            isOpen = false;
        }
    }
}
