using Godot;
using System;
using System.Collections.Generic;

public class NPC : Node
{
    [Export]
    string npcName;

    AnimationPlayer player;

    [Export]
    List<PackedScene> dialogue;

    DialogueManager manager;

    bool isPlayerCollide;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        manager = (DialogueManager)GetNode("/root/GameManager/DialogueManager");
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        player.Play("IDLE");
        isPlayerCollide = false;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (isPlayerCollide && Input.IsActionPressed("Submit"))
        {
            GD.Print("Dialogue Colision");
            manager.Open((Dialogue)dialogue[0].Instance());
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
        }
    }
}
