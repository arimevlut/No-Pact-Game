using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreakerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Ground":
                Destroy(other.gameObject);
                break;
        }
    }
}
