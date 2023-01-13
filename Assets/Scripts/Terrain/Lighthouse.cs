using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighthouse : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 0.75f, Space.Self);
    }
    
}
