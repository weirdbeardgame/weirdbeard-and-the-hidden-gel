using Godot;
using System;

public class DialogueBox : Node
{

    Dialogue dialogue;

    Sprite speakerBox;

    Label textRender;

    ColorRect box;

    int i = 0;

    bool isOpen = false;


    public override void _Ready()
    {
        textRender = (Label)GetNode("TextRender");
        box = (ColorRect)GetNode("Box");
    }

    void Open()
    {
        // Play opening animation
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
            // Print text in text box in here.
            textRender.Text += dialogue.buffer[i];

            i++;
        }
    }

}
