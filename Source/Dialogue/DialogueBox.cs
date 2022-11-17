using Godot;
using System;

public class DialogueBox : Popup
{
    Dialogue dialogue;

    AnimatedTexture speakerBox;
    StateMachine state;

    Label speakerName;
    Label dialogueRender;
    Panel box;

    Timer typing;

    int line = 0, textRendered;

    public override void _Ready()
    {
        state = (StateMachine)GetNode("/root/GameStates/GameState");
        dialogueRender = (Label)GetNode("Box/TextRender");
        box = (Panel)GetNode("Box");
        box.GrabFocus();
    }

    public void Open(Dialogue speak)
    {
        dialogue = speak;

        //box.RectPosition = GetViewport().Size / 2;

        // Play opening animation
        textRendered = 0;
        dialogue.Open(line);
        state.UpdateState("DIALOGUE");
        typing = (Timer)GetNode("Typing");
        dialogueRender.Text = dialogue.buffer[line];
        dialogueRender.VisibleCharacters = 0;

        typing.Start();
    }

    public void Close()
    {
        // Play closing animation
        RemoveChild(this);
        QueueFree();
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("Submit"))
        {
            dialogueRender.VisibleCharacters = dialogueRender.Text.Length;
            typing.Stop();
        }
    }

    public void OnTimeout()
    {
        dialogueRender.VisibleCharacters += 1;
        Close();
    }

}
