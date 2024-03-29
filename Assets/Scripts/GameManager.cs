using System;
using UnityEngine;

/// <summary>
/// The central game manager.
/// </summary>
public sealed class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    private int actualShipHealth;
    private int actualShieldHealth;
    private uint actualLaserPower;

    //Player
    public int MaxShipHealth { get; set; }
    public int ActualShieldHealth { get => actualShieldHealth; set => actualShieldHealth = Math.Max(0,value); }
    public float ShipHorizontalSpeed { get; set; }
    public float ShipVerticalSpeed { get; set; }
    public float ShipBoosterVelocity { get; set; }
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
    public bool IsEnemyWaveGettingStronger { get; set; }
    
    //Enemies - Wave One   
    public float EnemyWaveOneSinusStep { get; set; }
    public float EnemyWaveOneSinusAmplitude { get; set; }
    public float EnemyWaveOneYSpeed { get; set; }
    public float EnemyWaveOneDistance { get; set; }
    public uint EnemyWaveOneHealthAddOn { get; set; }
    public uint EnemyWaveOneScore { get; set; }

    //Enemies - Wave Two
    public float EnemyWaveTwoDistance { get; set; }
    public uint EnemyWaveTwoHealthAddOn { get; set; }
    public uint EnemyWaveTwoScore { get; set; }
    
    //Enemies - Wave Three
    public uint EnemyWaveThreeHealthAddOn { get; set; }
    public uint EnemyWaveThreeScore { get; set; }
    public float EnemyWaveThreeYBaseSpeed { get; set; }
    
    //Enemies - Wave Four
    public uint EnemyWaveFourHealthAddOn { get; set; }
    public uint EnemyWaveFourScore { get; set; }
    public float EnemyWaveFourSpeed { get; set; }
    public float EnemyWaveFourTimeDistanceBeforeMovement { get; set; }

    //Score
    public uint Score { get; set; }

    //Game states
    public bool IsGameRunning { get; set; }
    public bool IsLootSpawned { get; set; }
    public string LootMessage { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        
        Instance = this;
        
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        InitializeGameValues();

        InitializeShipValues();

        InitializeEnemyWaveOneValues();

        InitializeEnemyWaveTwoValues();

        InitializeEnemyWaveThreeValues();

        InitializeEnemyWaveFourValues();
    }
    
    public static T FindObjectInParentChain<T>(Transform currentTransform) where T : Component
    {
        // Überprüfen, ob der aktuelle Transform gültig ist
        if (currentTransform == null)
        {
            return null;
        }

        // Überprüfen, ob der aktuelle Transform das gesuchte Kriterium erfüllt
        T component = currentTransform.GetComponent<T>();
        if (component != null)
        {
            return component;
        }

        // Rekursiv die Elternkette durchlaufen, um das gesuchte GameObject zu finden
        return FindObjectInParentChain<T>(currentTransform.parent);
    }

    private void InitializeGameValues()
    {
        EnemyWaveLaserSpeed = 7.5f;
        IsEnemyWaveGettingStronger = false;
        Score = 0;
        IsGameRunning = true;
        IsLootSpawned = false;
        LootMessage = string.Empty;
    }

    private void InitializeEnemyWaveOneValues()
    {
        EnemyWaveOneSinusStep = 3.0f;
        EnemyWaveOneSinusAmplitude = .35f;
        EnemyWaveOneYSpeed = 3f;
        EnemyWaveOneDistance = 0.5f;
        EnemyWaveOneHealthAddOn = 0;
        EnemyWaveOneScore = 10;
}

    private void InitializeEnemyWaveTwoValues()
    {
        EnemyWaveTwoDistance = 2.0f;
        EnemyWaveTwoHealthAddOn = 0;
        EnemyWaveTwoScore = 25;
    }
    
    private void InitializeEnemyWaveThreeValues()
    {
        EnemyWaveThreeHealthAddOn = 0;
        EnemyWaveThreeScore = 25;
        EnemyWaveThreeYBaseSpeed = 3f;
    }
    
    private void InitializeEnemyWaveFourValues()
    {
        EnemyWaveFourHealthAddOn = 0;
        EnemyWaveFourScore = 25;
        EnemyWaveFourSpeed = 2f;
        EnemyWaveFourTimeDistanceBeforeMovement = 0.2f;
    }

    private void InitializeShipValues()
    {
        MaxShipHealth = 15;
        ActualShipHealth = MaxShipHealth;
        MaxShieldHealth = 5;
        ActualShieldHealth = MaxShieldHealth;
        ShipHorizontalSpeed = 7f;
        ShipVerticalSpeed = 6f;
        ShipBoosterVelocity = 5f;
        ShipLaserHitPoints = 1;
        MaxShipLaserPower = 30;
        ActualShipLaserPower = MaxShipLaserPower;
        LaserPowerRegainInterval = 2f;
        ShipLaserFrequency = 0.5f;
        IsShipShieldActive = false;
    }
}

