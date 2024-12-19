using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergy : MonoBehaviour
{
    public float actualEnergy;
    public float totalEnergy;
    public Image energyBar;
    public Text energyNum;

    void Start()
    {
        actualEnergy = totalEnergy;
    }
    void Update()
    {
        substractEnergy();
    }

    void substractEnergy()
    {
        actualEnergy -= Time.deltaTime;
        canvasRefresh();
        if (actualEnergy <= 0)
        {
            LevelManager.Instance.scM.LoseScene();
        }
    }

    public void subtractAmountOfEnergy(float amount)
    {
        actualEnergy -= amount;
        canvasRefresh();
        if (actualEnergy <= 0)
        {
            LevelManager.Instance.scM.LoseScene();
        }
    }

    void canvasRefresh()
    {
        energyBar.fillAmount = actualEnergy / totalEnergy;
    }

    public void removeEnergy(float energyToRemove)
    {
        float energySum = actualEnergy - energyToRemove;

        if(energySum < 0)
        {
            actualEnergy = 0;
        } else
        {
            actualEnergy = energySum;
        }

        canvasRefresh();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go.layer == 13) // EnergyUp
        {
            EnergyUp eu = go.GetComponent<EnergyUp>();
            float energySum = actualEnergy + eu.Energy;

            if(energySum > totalEnergy)
            {
                actualEnergy = totalEnergy;
            } else
            {
                actualEnergy = energySum;
            }

            eu.DestroyEnergy();
        }
    }
}
