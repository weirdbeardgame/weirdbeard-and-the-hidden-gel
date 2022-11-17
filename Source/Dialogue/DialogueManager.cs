using Godot;
using System;

public class DialogueManager : Control
{
    StateMachine states;
    DialogueBox box;

    int line = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var node = GetNode("/root/GameStates");
        states = node.GetNode<StateMachine>("GameState");
    }

    public void Open(Dialogue toSpeak)
    {
        states.UpdateState("DIALOGUE");
        SceneManager.CurrentScene.AddChild(box);
        GetNode<Popup>("DialogueBox").Visible = true;
        GetNode<Popup>("DialogueBox").Popup_();
    }

    public override void _Process(float delta)
    {

    }

    public void Close()
    {

    }
}
