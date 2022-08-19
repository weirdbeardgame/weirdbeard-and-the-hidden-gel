using Godot;
using System;
using System.Collections.Generic;

public class Dialogue : Node
{
    [Export]
    public List<string> buffer;

    public int length;

    [Export]
    public Sprite speakerHead;

    public Dialogue Open(int i)
    {
        if (i <= buffer.Capacity)
        {
            length = buffer[i].Length;
            return this;
        }

        return null;
    }
}

public class DialogueEventMessage : EventArgs
{
    public Dialogue speech;

    public DialogueEventMessage(Dialogue dialogue)
    {
        speech = dialogue;
    }
}

public class DialogueEventPublish
{

    public delegate void DialogueEventHandler(object sender, DialogueEventMessage message);

    public event DialogueEventHandler speak;

    public DialogueEventPublish()
    {
        speak = null;
    }

    public DialogueEventPublish(Dialogue speak)
    {
        OnPublish(speak);
    }

    public virtual void OnPublish(Dialogue toSpeak)
    {
        speak(this, new DialogueEventMessage(toSpeak));
        GD.Print("DIALOGUE EVENT PUBLISHED");
    }

}
