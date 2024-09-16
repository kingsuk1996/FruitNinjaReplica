using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Blade")
        {
            Debug.Log("We Hit a watermelon");
            Destroy(this.gameObject);
        }
    }
}
