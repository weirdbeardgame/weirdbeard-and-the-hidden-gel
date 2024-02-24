using Godot;
using System;

// Basically the Abstract Factory Pattern
public partial class EnemySpawner : Node
{
    [Export]
    PackedScene spawn;

    [Export]
    float maxAmnt, amt, spawnRate;

    [Export]
    EnemyDirection enemyDirection = EnemyDirection.LEFT;

    RandomNumberGenerator randNum;

    Vector2 enePos, screenSize;

    Timer timer;

    LevelCommon activeScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enePos = new Vector2();
        randNum = new RandomNumberGenerator();
        timer = (Timer)GetNode("Timer");

        activeScene = (LevelCommon)Owner;
    }

    public void Randomize()
    {
        screenSize = GetViewport().GetVisibleRect().Size;
        enePos.X = randNum.RandfRange(0, screenSize.X);

        randNum.Randomize();

        enePos.Y = randNum.RandfRange(0, screenSize.Y);
        randNum.Randomize();
    }

    public void Spawn()
    {
        for (int i = 0; i <= spawnRate; i++)
        {
            amt = randNum.RandfRange(0, maxAmnt);
            randNum.Randomize();

            GD.Print("Spawn");

            Randomize();
            Enemy ene = spawn.Instantiate<Enemy>();

            ene.TopLevel = true;

            ene.Position = enePos;

            activeScene.AddChild(ene);
        }
    }

    void TimeOut()
    {

    }
}
