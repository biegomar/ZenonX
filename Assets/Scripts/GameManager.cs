using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameManager
{
    private static readonly object lockObject = new object();
    private static GameManager instance = null;
    private int actualShipHealth;
    private int actualShieldHealth;
    private uint actualLaserPower;

    //Player
    public int MaxShipHealth { get; set; }
    public int ActualShieldHealth { get => actualShieldHealth; set => actualShieldHealth = Math.Max(0,value); }
    public float ShipHorizontalSpeed { get; set; }
    public float ShipBoosterVelocity { get; set; }
    public float ShipLaserSpeed { get; set; }
    public float ShipLaserFrequency { get; set; }
    public uint ShipLaserHitPoints { get; set; }
    public uint MaxShipLaserPower { get; set; }
    public uint ActualShipLaserPower { get => actualLaserPower; set => actualLaserPower = Math.Max(0, value); }
    public float LaserPowerRegainInterval { get; set; }
    
    //Player extensions
    public bool IsShipShieldActive { get; set; }
    public int ActualShipHealth { get => actualShipHealth; set => actualShipHealth = Math.Max(0,value); }
    public int MaxShieldHealth { get; set; }

    //Enemies - general
    public float EnemyWaveLaserSpeed { get; set; }
    
    //Enemies - Wave One   
    public float EnemyWaveOneSinusStep { get; set; }
    public float EnemyWaveOneSinusAmplitude { get; set; }
    public float EnemyWaveOneYStep { get; set; }
    public float EnemyWaveOneYSpeed { get; set; }
    public float EnemyWaveOneDistance { get; set; }
    public uint EnemyWaveOneHealth { get; set; }
    public uint EnemyWaveOneScore { get; set; }

    //Enemies - Wave Two
    public float EnemyWaveTwoDistance { get; set; }
    public uint EnemyWaveTwoHealth { get; set; }
    public uint EnemyWaveTwoScore { get; set; }
    
    //Enemies - Wave Three
    public uint EnemyWaveThreeHealth { get; set; }
    public uint EnemyWaveThreeScore { get; set; }
    public float EnemyWaveThreeYBaseSpeed { get; set; }

    //Score
    public uint Score { get; set; }

    //Game states
    public bool IsGameRunning { get; set; }

    // Privater Konstruktor, um Instanziierung von außen zu verhindern
    private GameManager()
    {
        InitializeGameValues();

        InitializeShipValues();

        InitializeEnemyWaveOneValues();

        InitializeEnemyWaveTwoValues();

        InitializeEnemyWaveThreeValues();
    }

    private void InitializeGameValues()
    {
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
        EnemyWaveLaserSpeed = 6.5f;
        EnemyWaveOneHealth = 2;
        EnemyWaveOneScore = 10;
}

    private void InitializeEnemyWaveTwoValues()
    {
        EnemyWaveTwoDistance = 3.0f;
        EnemyWaveTwoHealth = 2;
        EnemyWaveTwoScore = 25;
    }
    
    private void InitializeEnemyWaveThreeValues()
    {
        EnemyWaveThreeHealth = 2;
        EnemyWaveThreeScore = 25;
        EnemyWaveThreeYBaseSpeed = 3f;
    }

    private void InitializeShipValues()
    {
        MaxShipHealth = 10;
        ActualShipHealth = MaxShipHealth;
        MaxShieldHealth = 5;
        ActualShieldHealth = MaxShieldHealth;
        ShipHorizontalSpeed = 7f;
        ShipBoosterVelocity = 5f;
        ShipLaserSpeed = 6.5f;
        ShipLaserHitPoints = 1;
        MaxShipLaserPower = 30;
        ActualShipLaserPower = MaxShipLaserPower;
        LaserPowerRegainInterval = 2f;
        ShipLaserFrequency = 0.6f;
        IsShipShieldActive = false;
    }

    // �ffentliche Methode, um die Instanz abzurufen
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

