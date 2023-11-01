using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y + 0.3f,
               transform.position.z), Quaternion.identity);
        }
    }
}
