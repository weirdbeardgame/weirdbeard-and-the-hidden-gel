using Godot;
using System;
using Godot.Collections;

public enum LevelType { DEFAULT, GRASS, ISLAND, ICE, WATER }

public enum LevelCompleteState { COMPLETE, NON_COMPLETE }

[Tool]
public partial class LevelCommon : Node2D
{
    [Export]
    public string LevelName;

    protected Player Player;

    AudioStreamPlayer BackgroundPlayer;

    [Export] Resource _AudioFile;

    [Export] protected LevelType _Type;

    private bool _Unlocked;
    private LevelCompleteState _Completed;

    // Use this for non static refs or event calls. IE. Needs to spawn player.
    // Scenes = GetNode<SceneManager>("/root/SceneManager");
    protected SceneManager Scenes;

    public LevelCompleteState Completed => _Completed;

    public bool IsLevelComplete => _Completed == LevelCompleteState.COMPLETE;

    public bool Unlocked => _Unlocked;

    public LevelType LevelType => _Type;

    public virtual void EnterLevel(Player p)
    {
        BackgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");
        if (Player == null)
        {
            if (p != null)
            {
                Player = p;
            }
            else
            {
                Player = SceneManager._ActivePlayerRef;
            }
        }
    }

    public void CreateAudioStream()
    {
        BackgroundPlayer.Stream = GD.Load<AudioStream>(_AudioFile.ResourcePath);
        BackgroundPlayer.Play();
    }

    public void CompleteLevel()
    {
        _Completed = LevelCompleteState.COMPLETE;
        ExitLevel();
    }

    public virtual void Update()
    {

    }

    public virtual void FixedUpdate()
    {

    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!Engine.IsEditorHint())
        {
            Update();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        if (!Engine.IsEditorHint())
        {
            FixedUpdate();
        }
    }

    public virtual void ExitLevel()
    {

    }

    public virtual void CalledDefferedExitLevel()
    {

    }

    public virtual void ResetLevel()
    {

    }

    public virtual void EnterSubLevel(Player Player, SubLevel parent)
    {

    }

    public virtual void ExitSubLevel()
    {

    }
}
