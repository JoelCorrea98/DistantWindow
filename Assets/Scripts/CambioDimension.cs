using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioDimension : MonoBehaviour
{
    private int indexDimension;
    public List<GameObject> objectDimensions;
    public List<Camera> cameraDimensions;

    public PlayerLook pl;
    public PlayerEnergy pe;

    private void Start()
    {
        ChangeDimension(0);
        ObjectAvtivation();
    }
    private void Update()
    {
        if (Input.GetButtonDown("RightClick"))
        {
            ChangeDimension(1);
            ObjectAvtivation();
        }
        else if (Input.GetButtonDown("LeftClick"))
        {
            ChangeDimension(-1);
            ObjectAvtivation();
        }
    }

    private void ChangeDimension(int direction)
    {
        indexDimension += direction;
        LevelManager.Instance.auM.PlayChangeDimension();

        if (indexDimension > 2)
        {
            indexDimension = 0;
        }
        else if (indexDimension < 0)
        {
            indexDimension = 2;
        }
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

        if (indexDimension == 0)
        {
            cameraDimensions[indexDimension].gameObject.SetActive(true);
            objectDimensions[indexDimension].SetActive(true);
            pl.GetCam(cameraDimensions[indexDimension]);
        }
        if (indexDimension == 1)
        {
            cameraDimensions[indexDimension].gameObject.SetActive(true);
            objectDimensions[indexDimension].SetActive(true);
            pl.GetCam(cameraDimensions[indexDimension]);
        }
        if (indexDimension == 2)
        {
            cameraDimensions[indexDimension].gameObject.SetActive(true);
            objectDimensions[indexDimension].SetActive(true);
            pl.GetCam(cameraDimensions[indexDimension]);
        }
    }

}
