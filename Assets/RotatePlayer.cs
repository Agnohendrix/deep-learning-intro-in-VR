using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    OVRInputAxis2DAction rightThumbstick;
    public Transform playerAlias;

    public float rotationSpeed;
    public float debounceTime;
    public int angle;
    bool rotAvailable = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 y = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        int rot = Mathf.RoundToInt(y.x);
        if (rot != 0)
		{
			if (rotAvailable)
			{
                playerAlias.Rotate(rotationSpeed * Vector3.up * rot);
                StartCoroutine("Wait");
            }
            
        }  
    }

    IEnumerator Wait()
	{
        rotAvailable = false;

        yield return new WaitForSeconds(0.5f);
        rotAvailable = true;

    }


}
