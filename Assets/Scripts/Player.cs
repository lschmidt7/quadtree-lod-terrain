using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position += transform.forward * 0.2f;
        }
        if(Input.GetKey(KeyCode.D)){
            transform.Rotate(0,1f,0);
        }
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0,-1f,0);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position + transform.forward * 20f);
    }
}
