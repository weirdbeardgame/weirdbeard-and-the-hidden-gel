using Godot;
using System;

public class DialogueManager : Node
{

    SceneManager scenes;
    StateMachine states;
    DialogueBox box;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        scenes = (SceneManager)Owner.GetNode("SceneManager");
        states = (StateMachine)Owner.GetNode("StateMachine");
        box = (DialogueBox)GetTree().CurrentScene.GetNode("TextBox");

        GetTree().CurrentScene.RemoveChild(box);
    }

    public void Open(Dialogue toSpeak)
    {
        states.UpdateState("DIALOGUE");
        scenes.CurrentScene.AddChild(box);
        box.Open(toSpeak);
    }

    public override void _PhysicsProcess(float delta)
    {

    }

    public void Close()
    {

    }
}
