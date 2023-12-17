using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ProgressOff;

    [SerializeField]
    private GameObject ProgressOn;
    
    private const float ProgressPartDistance = 7f;

    // Start is called before the first frame update
    void Start()
    {
        this.DrawProgressBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawProgressBar()
    {
        float posX = 0;
        for (int i = 0; i < 20; i++) 
        {
            Instantiate(ProgressOff, new Vector3(
               posX,
               this.gameObject.transform.position.y,
               this.gameObject.transform.position.z), Quaternion.identity);

            posX += ProgressPartDistance;
        }
    }
}
