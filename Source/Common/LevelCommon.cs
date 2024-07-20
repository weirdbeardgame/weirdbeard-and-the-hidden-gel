using Godot;
using System;
using Godot.Collections;

public enum LevelType { DEFAULT, GRASS, ISLAND, ICE, WATER }

public enum LevelState { COMPLETE, ACTIVE, NON_COMPLETE }

[Tool]
public partial class LevelCommon : Node2D
{

    private bool _Unlocked;

    protected Player _Player;

    [Export]
    private Resource _AudioFile;

    [Export] protected LevelType _Type;

    // Use this for non static refs or event calls. IE. Needs to spawn player.
    // Scenes = GetNode<SceneManager>("/root/SceneManager");
    protected SceneManager Scenes;

    protected LevelState _LevelState;

    private AudioStreamPlayer _BackgroundPlayer;

    [Export]
    public string LevelName;

    public LevelState LevelState => _LevelState;

    public bool IsLevelComplete => _LevelState == LevelState.COMPLETE;

    public bool Unlocked => _Unlocked;

    public LevelType LevelType => _Type;

    // Potentially add Unlock conditions? For now we won't implement Level Unlocking just yet

    public void Unlock() => _Unlocked = true;

    public Action UnlockEvent;


    public virtual void EnterLevel(Player p)
    {
        _BackgroundPlayer = (AudioStreamPlayer)GetNode("BackgroundAudio");
        if (_Player == null)
        {
            if (p != null)
            {
                _Player = p;
            }
            else
            {
                _Player = SceneManager._ActivePlayerRef;
            }
        }
    }

    public void CreateAudioStream()
    {
        _BackgroundPlayer.Stream = GD.Load<AudioStream>(_AudioFile.ResourcePath);
        _BackgroundPlayer.Play();
    }

    public void CompleteLevel()
    {
        _LevelState = LevelState.COMPLETE;
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
