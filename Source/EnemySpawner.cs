using Godot;
using System;

// Basically the Abstract Factory Pattern
public class EnemySpawner : Node
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

    Level activeScene;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enePos = new Vector2();
        randNum = new RandomNumberGenerator();
        timer = (Timer)GetNode("Timer");

        activeScene = (Level)Owner;
    }

    public void Randomize()
    {
        screenSize = GetViewport().GetVisibleRect().Size;
        enePos.x = randNum.RandfRange(0, screenSize.x);

        randNum.Randomize();

        enePos.y = randNum.RandfRange(0, screenSize.y);
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
            Enemy ene = (Enemy)spawn.Instance();

            ene.SetAsToplevel(true);

            ene.Position = enePos;

            activeScene.AddChild(ene);
            activeScene.activeEnemies.Add(ene);
        }
    }

    void TimeOut()
    {
        if (activeScene.activeEnemies.Capacity < activeScene.maxEnemyAmnt)
        {
            GD.Print("Call Spawn");
            Spawn();
        }
    }
}
