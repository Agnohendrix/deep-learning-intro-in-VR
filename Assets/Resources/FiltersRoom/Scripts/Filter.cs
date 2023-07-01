using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    public Material red;
    public Material green;
    public List<GameObject> outputElements;

    [SerializeField]
    private int bias = 3;
    [SerializeField]
    private int w1 = -2;
    [SerializeField]
    private int w2 = -2;
    private int x1 = 999;
    private int x2 = 999;

    private int previousOutput = 0;

    private int[] snapZoneValue;
    private int[] cubeValue;

    public GameObject cubePrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize numbers for snapzones
        GameObject snap1 = GameObject.Find("InputSnapZone11");
        snap1.GetComponent<SnapZoneInput3x3>().setInputValueSetFromEditor("0,1,0,1,0,1,0,1,0");
        snap1.transform.Find("Text").GetComponent<TextMeshPro>().SetText("0 1 0\n1 0 1\n0 1 0");

        slideDoorScript = door.GetComponent<SlideDoor>();
        snapZoneValue = snap1.GetComponent<SnapZoneInput3x3>().getInputValue();
        Debug.Log("snapZone value: " + string.Join(" ", snapZoneValue));
    }

    // Update is called once per frame
    void Update()
    {
        //should probably not do this in this loop
        /*
        if (calculateOutput() == 1 && previousOutput == 0)
        {
            slideDoorScript.OpenDoor();
            foreach (GameObject o in outputElements)
            {
                o.GetComponent<Renderer>().material = green;
            }
            previousOutput = 1;
        }
        else if (calculateOutput() == 0 && previousOutput == 1)
        {
            slideDoorScript.CloseDoor();
            foreach (GameObject o in outputElements)
            {
                o.GetComponent<Renderer>().material = red;
            }
            previousOutput = 0;
        }*/
    }

    public void input1Snapped(GameObject input)
    {
        Debug.Log("snapped");
        //x1 = input.GetComponent<FilterInput>().getInputValue();
        x1 = 1;
        cubeValue = input.GetComponent<FilterInput>().getInputValue();
        Debug.Log("snapped object " + this.name + " " + string.Join(" ", cubeValue));

    }

    public void snapZoneSnapped(string value)
	{
        Debug.Log("snapzone");
        Debug.Log("snapzone object " + value);
        Debug.Log(this.transform.parent.name);
        Transform n = this.transform.FindChild(value);
        SnapZoneInput3x3 v = n.GetComponent<SnapZoneInput3x3>();
        Debug.Log("this " + string.Join(" ", v.getInputValue()));
        snapZoneValue = v.getInputValue();
        int result = calculateMatrixOutput();
        if(result == 45)
            slideDoorScript.OpenDoor();

    }

    public void input1UnSnapped()
    {
        x1 = 999;
    }

    public void input2Snapped(GameObject input)
    {
        x2 = input.GetComponent<NNInput>().getInputValue();
    }

    public void input2UnSnapped()
    {
        x2 = 999;
    }

    private int calculateMatrixOutput()
	{
        int tot = 0;
        for(int i=0; i<9; i++)
		{
            tot += snapZoneValue[i] * cubeValue[i];
		}
        Debug.Log("operation value: " + tot);
        return tot;
	}

    private int calculateOutput()
    {
        if ((w1 * x1) + (w2 * x2) + bias > 0)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
