﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dimension
{
    Red,
    Green,
    Blue 
}
public class CambioDimension : MonoBehaviour
{
    [SerializeField] private int indexDimension;
    public List<GameObject> objectDimensions;
    public List<Camera> cameraDimensions;
    public GameObject VolumeChangeDimension;
    public Action changingDimension;
    public PlayerLook pl;
    public PlayerEnergy pe;
    public float energyToRemove = 10;




    private void Start()
    {
        ChangeDimension(0, false);
        ObjectAvtivation();
        WorldStateManager.instance.SetState("PlayerDimension", 0);
    }
    private void Update()
    {
        if (Input.GetButtonDown("RightClick"))
        {
            ChangeDimension(1, true);
            ObjectAvtivation();
        }
        else if (Input.GetButtonDown("LeftClick"))
        {
            ChangeDimension(-1, true);
            ObjectAvtivation();
        }
    }

    private void ChangeDimension(int direction, bool removeEnergy)
    {
        StartCoroutine(ExecuteChangeEffect());
        indexDimension += direction;
        LevelManager.Instance.auM.PlayChangeDimension();
        if(removeEnergy) pe.removeEnergy(energyToRemove);

        if (indexDimension > 2)
        {
            indexDimension = 0;
        }
        else if (indexDimension < 0)
        {
            indexDimension = 2;
        }
        Dimension myDimension=default;
        switch (indexDimension)
        {
            case 0:
                myDimension = Dimension.Red;
                break;
            case 1:
                myDimension = Dimension.Green;
                break;
            case 2:
                myDimension = Dimension.Blue;
                break;
        }
        WorldStateManager.instance.SetState("PlayerDimension", myDimension);
        changingDimension?.Invoke();

    }
    private void ObjectAvtivation()
    {
        foreach (var item in objectDimensions)
        {
            item.SetActive(false);
        }

        foreach (var item in cameraDimensions)
        {
            item.gameObject.SetActive(false);
        }

        cameraDimensions[indexDimension].gameObject.SetActive(true);
        objectDimensions[indexDimension].SetActive(true);
        pl.GetCam(cameraDimensions[indexDimension]);
        LevelManager.Instance.playerController.cam = cameraDimensions[indexDimension];
    }

    IEnumerator ExecuteChangeEffect()
    {
        VolumeChangeDimension.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        VolumeChangeDimension.SetActive(false);

    }

}
