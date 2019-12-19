using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remover : MonoBehaviour 
{

    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
