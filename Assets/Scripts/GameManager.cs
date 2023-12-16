using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static readonly object lockObject = new object();
    private static GameManager instance = null;

    //Player
    public float ShipHorizontalSpeed { get; set; }
    public float ShipBoosterVelocity { get; set; }
    public float ShipLaserSpeed { get; set; }
    public uint ShipLaserHitPoints { get; set; }
    public uint MaxShipLaserPower { get; set; }
    public float LaserPowerRegainInterval { get; set; }

    //Enemies    
    public float EnemySinusStep { get; set; }
    public float EnemySinusAmplitude { get; set; }
    public float EnemyYStep { get; set; }
    public float EnemyYSpeed { get; set; }
    public float EnemyDistance { get; set; }

    //Level 1
    public float Level1LaserFrequence { get; set; }

    //Score
    public uint Score { get; set; }

    // Privater Konstruktor, um Instanziierung von auﬂen zu verhindern
    private GameManager()
    {
        // Setze Standardwerte
        ShipHorizontalSpeed = 7f;
        ShipBoosterVelocity = 5f;
        ShipLaserSpeed = 6.5f;
        ShipLaserHitPoints = 1;
        MaxShipLaserPower = 50;
        LaserPowerRegainInterval = 1f;

        EnemySinusStep = 0.01f;
        EnemySinusAmplitude = 0.25f;
        EnemyYStep = 0.03f;
        EnemyYSpeed = 0.4f;
        EnemyDistance = 0.5f;

        Level1LaserFrequence = 0.4f;

        Score = 0;
    }

    // ÷ffentliche Methode, um die Instanz abzurufen
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new GameManager();
                    }
                }
            }
            return instance;
        }
    }
}

