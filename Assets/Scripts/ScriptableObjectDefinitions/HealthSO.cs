using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(fileName = "Health_01", menuName = "ScriptableObjects/HealthScriptableObject", order = 0)]
public class HealthSO : ScriptableObject
{
    public int health = 100;
}
