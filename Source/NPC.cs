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

    DialogueEventPublish dialogueEvent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = (AnimationPlayer)GetNode("AnimationPlayer");
        player.Play("IDLE");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    void Interact(object body)
    {
        if (body is Player && Input.IsActionJustPressed("Submit"))
        {
            dialogueEvent = new DialogueEventPublish((Dialogue)dialogue[0].Instance());
        }
    }
}
