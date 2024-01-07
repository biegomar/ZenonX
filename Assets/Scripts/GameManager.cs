using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static readonly object lockObject = new object();
    private static GameManager instance = null;
    private int actualShipHealth;
    private uint actualLaserPower;

    //Player
    public int MaxShipHealth { get; set; }
    public int ActualShipHealth { get => actualShipHealth; set => actualShipHealth = Math.Max(0,value); }
    public float ShipHorizontalSpeed { get; set; }
    public float ShipBoosterVelocity { get; set; }
    public float ShipLaserSpeed { get; set; }
    public uint ShipLaserHitPoints { get; set; }
    public uint MaxShipLaserPower { get; set; }
    public uint ActualShipLaserPower { get => actualLaserPower; set => actualLaserPower = Math.Max(0, value); }
    public float LaserPowerRegainInterval { get; set; }

    //Enemies - Wave One   
    public float EnemyWaveOneSinusStep { get; set; }
    public float EnemyWaveOneSinusAmplitude { get; set; }
    public float EnemyWaveOneYStep { get; set; }
    public float EnemyWaveOneYSpeed { get; set; }
    public float EnemyWaveOneDistance { get; set; }
    public float EnemyWaveOneLaserSpeed { get; set; }

    //Enemies - Wave Two
    public float EnemyWaveTwoDistance { get; set; }

    //Level 1
    public float Level1LaserFrequence { get; set; }

    //Score
    public uint Score { get; set; }

    //Game states
    public bool IsGameRunning { get; set; }

    // Privater Konstruktor, um Instanziierung von auﬂen zu verhindern
    private GameManager()
    {
        InitializeGameValues();

        InitializeShipValues();

        InitializeEnemyWaveOneValues();

        InitializeEnemyWaveTwoValues();
    }

    private void InitializeGameValues()
    {
        Level1LaserFrequence = 0.4f;
        Score = 0;
        IsGameRunning = true;
    }

    private void InitializeEnemyWaveOneValues()
    {
        EnemyWaveOneSinusStep = 0.01f;
        EnemyWaveOneSinusAmplitude = 0.25f;
        EnemyWaveOneYStep = 0.03f;
        EnemyWaveOneYSpeed = 0.25f;
        EnemyWaveOneDistance = 0.5f;
        EnemyWaveOneLaserSpeed = 6.5f;
    }

    private void InitializeEnemyWaveTwoValues()
    {
        EnemyWaveTwoDistance = 3.0f;
    }

    private void InitializeShipValues()
    {
        MaxShipHealth = 10;
        ActualShipHealth = MaxShipHealth;
        ShipHorizontalSpeed = 7f;
        ShipBoosterVelocity = 5f;
        ShipLaserSpeed = 6.5f;
        ShipLaserHitPoints = 1;
        MaxShipLaserPower = 50;
        ActualShipLaserPower = MaxShipLaserPower;
        LaserPowerRegainInterval = 2f;
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

