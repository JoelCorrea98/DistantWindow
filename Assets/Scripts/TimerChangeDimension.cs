using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChangeDimension : MonoBehaviour
{
    public float timeToChange;

    private int indexDimension;
    public List<GameObject> objectDimensions;
    public List<Camera> cameraDimensions;

    void Start()
    {
        StartCoroutine(HoldChange());

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }
    void Update()
    {
        
        ObjectAvtivation();
    }

    private void ChangeDimension()
    {
        indexDimension ++;

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
           
        }
        if (indexDimension == 1)
        {
            cameraDimensions[indexDimension].gameObject.SetActive(true);
            objectDimensions[indexDimension].SetActive(true);
         
        }
        if (indexDimension == 2)
        {
            cameraDimensions[indexDimension].gameObject.SetActive(true);
            objectDimensions[indexDimension].SetActive(true);
       
        }
    }

    IEnumerator HoldChange()
    {
        Debug.Log("Entre a la corrutina");
        yield return new WaitForSeconds(timeToChange);
        ChangeDimension();
        StartCoroutine(HoldChange());
    }
}
