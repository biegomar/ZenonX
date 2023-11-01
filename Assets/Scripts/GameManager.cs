using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static float EnemySinusStep = 0.01f;
    public static float EnemySinusAmplitude = 1.5f;
    public static float EnemyYStep = 0.03f;
    public static float EnemyYSpeed = 0.1f;

    public static float ShipLaserSpeed = 5f;
    public static uint MaxShipLaserPower = 100;
    public static float LaserPowerRegainFactor = 0.3f;
}
