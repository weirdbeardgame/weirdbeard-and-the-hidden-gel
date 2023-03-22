using Godot;
using System;

public partial class LevelSpace : Area2D
{
    int ID;
    string levelNum;
    RichTextLabel numDisp;
    LevelCommon level;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        numDisp = GetNode<RichTextLabel>("LevelNum");
    }

    public void CreateSpace(int i, string l, LevelCommon lev)
    {
        ID = i;
        levelNum = l;
        level = lev;

        numDisp.Text = ID.ToString();
    }

    public void EnterLevel(Player p)
    {
        SceneManager.changeScene(level.levelName, p);
    }

}
