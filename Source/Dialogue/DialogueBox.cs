using Godot;
using System;

public class DialogueBox : Node
{
    Dialogue dialogue;

    AnimatedTexture speakerBox;

    Label textRender;

    Panel box;

    Tween interp;

    int line = 0;

    bool isOpen = false;

    public override void _Ready()
    {
        textRender = (Label)GetNode("Box/TextRender");
        interp = (Tween)GetNode("Interp");
        box = (Panel)GetNode("Box");
        box.Visible = isOpen;
    }

    public void Open(Dialogue speak)
    {
        dialogue = speak;

        // Play opening animation
        dialogue.Open(line);
        textRender.Text = dialogue.buffer[line];
        interp.InterpolateProperty(textRender, "percent_visible", 0.0, 1.0, dialogue.length * 0.5f, Tween.TransitionType.Linear, Tween.EaseType.InOut);
        isOpen = true;
        box.Visible = isOpen;
    }

    public void Advance()
    {
        if (line < dialogue.buffer.Capacity)
        {
            line += 1;
        }
        else if (line >= dialogue.buffer.Capacity)
        {
            Close();
        }
    }


    public void Close()
    {
        // Play closing animation
        isOpen = false;
    }

    public override void _Process(float delta)
    {
        while (isOpen)
        {
            //interp.Start();
        }
    }

}
