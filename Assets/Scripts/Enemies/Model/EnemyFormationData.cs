using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyFormation")]
public class EnemyFormationData : ScriptableObject
{
    public GameObject EnemyTemplate;
    public GameObject LootTemplate;
    public uint EnemyHealthPoints;
    public Vector3 StartPoint;
    public Vector3 TransitionRangeFrom;
    public Vector3 TransitionRangeTo;
    public Vector3 Distance;
    public bool Flag;
}
