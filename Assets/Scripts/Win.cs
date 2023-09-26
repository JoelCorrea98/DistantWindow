using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public LayerMask layer;
    public Scenemanager scm;

    void OnCollisionEnter(Collision other)

    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("Cambio a win");
            LevelManager.Instance.scM.WinScene();
        }
    
    }
}
