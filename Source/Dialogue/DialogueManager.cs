using Godot;
using System;

public class DialogueManager : Node
{
    StateMachine states;
    DialogueBox box;

    int line = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        states = (StateMachine)Owner.GetNode("StateMachine");
        box = (DialogueBox)GetTree().CurrentScene.GetNode("TextBox");

        GetTree().CurrentScene.RemoveChild(box);
    }

    public void Open(Dialogue toSpeak)
    {
        states.UpdateState("DIALOGUE");
        SceneManager.CurrentScene.AddChild(box);
        box.Open(toSpeak);
    }

    public override void _PhysicsProcess(float delta)
    {
        if (Input.IsActionJustPressed("Submit"))
        {
            line += 1;
        }
    }

    public void Close()
    {

    }
}
