using Godot;
using System;
using System.Collections.Generic;

public class DialogueManager : Control
{
    StateMachine states;
    RichTextLabel text;
    Popup box;

    int line = 0;
    int index = 0;
    int textVisible = 0;

    List<PackedScene> currentBuffer;
    Dialogue currentDialogue;

    bool isOpen = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var node = GetNode("/root/GameStates");
        states = node.GetNode<StateMachine>("GameState");
    }

    public void Open(List<PackedScene> toSpeak)
    {
        states.UpdateState("DIALOGUE");
        SceneManager.CurrentScene.AddChild(box);
        box = GetNode<Popup>("DialogueBox");
        text = box.GetNode<RichTextLabel>("RichTextLabel");
        box.Visible = true;
        box.Popup_();

        currentBuffer = toSpeak;
        currentDialogue = (Dialogue)currentBuffer[index].Instance();
        text.Text = currentDialogue.buffer[line];
        text.VisibleCharacters = 0;
        isOpen = true;
    }

    void PrintText()
    {
        if (isOpen)
        {
            if (textVisible < text.Text.Length)
            {
                textVisible += 1;
                text.VisibleCharacters = textVisible;

                if (Input.IsActionJustPressed("Submit"))
                {
                    text.VisibleCharacters = text.Text.Length;
                }
            }
            if ((line + 1) < currentDialogue.buffer.Count)
            {
                line += 1;
                text.Text = currentDialogue.buffer[line];
                text.VisibleCharacters = 0;
            }
            else
            {
                if (Input.IsActionJustPressed("Submit"))
                {
                    Close();
                }
            }
        }
    }

    public override void _Process(float delta)
    {
        PrintText();
    }

    public void Close()
    {
        line = 0;
        index = 0;
        textVisible = 0;
        currentDialogue = null;
        currentBuffer = null;
        box.Visible = false;
        isOpen = false;
    }
}
