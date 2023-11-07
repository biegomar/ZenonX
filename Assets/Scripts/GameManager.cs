using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    //Player
    public static float ShipHorizontalSpeed = 7f;
    public static float ShipBoosterVelocity = 5f;
    public static float ShipLaserSpeed = 5f;
    public static uint ShipLaserHitPoints = 1;
    public static uint MaxShipLaserPower = 50;
    public static float LaserPowerRegainInterval = 1f;

    //Enemies    
    public static float EnemySinusStep = 0.01f;
    public static float EnemySinusAmplitude = 0.25f;
    public static float EnemyYStep = 0.03f;
    public static float EnemyYSpeed = 0.4f;
    public static float EnemyDistance = 0.5f;

    //Level 1
    public static float Level1LaserFrequence = 0.4f;

    //Score
    public static uint Score = 0;
}
