using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUp : MonoBehaviour
{
    public float Energy = 30;
    public void DestroyEnergy()
    {
        LevelManager.Instance.auM.PlayPickeable();
        Destroy(gameObject);
    }
}
