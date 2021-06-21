using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Start Stats")]
    public int MaxLife;
    public float MaxSpeedWalking;
    public float MaxSpeedFollow;

    [Header("Update Stats")]
    public float Life;
    public float SpeedWalking;
    public float SpeedFollow;

    private void Awake()
    {
        Life = MaxLife;

        SpeedWalking = MaxSpeedWalking;

        SpeedFollow = MaxSpeedFollow;
    }
}
