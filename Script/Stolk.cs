using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stolk : MonoBehaviour
{
      void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ("Money"))
        {
            other.gameObject.SetActive (false);
           
        }
    }
}
