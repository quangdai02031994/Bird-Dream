using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            Destroy(other.gameObject);
        }
    }


}
