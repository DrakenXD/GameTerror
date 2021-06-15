using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Start Stats")]
    public float MaxLife;
    public float MaxSpeed;
    public float MaxRun;

    [Header("Update Stats")]
    public float Life;
    public float Speed;
    public float Run;

    private void Awake()
    {
        Life = MaxLife;

        Speed = MaxSpeed;

        Run = MaxRun;
    }
}
