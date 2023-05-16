using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Transform playerAlias;

    public float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 y = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Debug.Log(y);
    }
}
