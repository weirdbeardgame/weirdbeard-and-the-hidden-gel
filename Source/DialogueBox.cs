using Godot;
using System;

public class DialogueBox : Node
{

    Dialogue dialogue;

    AnimatedTexture speakerBox;

    Label textRender;

    Panel box;

    Tween interp;

    int i = 0;

    bool isOpen = false;


    public override void _Ready()
    {
        textRender = (Label)GetNode("TextRender");
        box = (Panel)GetNode("Box");
    }

    void Open()
    {
        // Play opening animation
        dialogue.Open();
        interp.InterpolateProperty(textRender, "percent_visible", 0.0, 1.0, dialogue.length * 0.5f, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        isOpen = true;
    }

    void Close()
    {
        // Play closing animation
        isOpen = false;
    }

    public override void _Process(float delta)
    {
        while (isOpen)
        {
            interp.Start();
        }
    }

}
