using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FilterSizeTaskQuiz : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    public Material red;
    public Material green;
    public List<GameObject> outputElements;

    [SerializeField]
    private int stride = 3;
    [SerializeField]
    private int padding = 0;

    private int x1 = 999;
    private int x2 = 999;

    int expectedResult;

    private int[] snapZoneValue;
    private int[] cubeValue;

    private int[] chosenCube;

    // Start is called before the first frame update
    void Start()
    {

        //Initialize numbers for cubes
        GameObject cube1 = transform.Find("Interactable.InputFilter1").gameObject;
        GameObject cube2 = transform.Find("Interactable.InputFilter2").gameObject;
        GameObject cube3 = transform.Find("Interactable.InputFilter3").gameObject;

        //TODO Select filter value from cube


        slideDoorScript = door.GetComponent<SlideDoor>();

    }

    public void filterSnapped(GameObject input)
    {
        int[] filterValue = input.GetComponent<FilterInput>().getInputValue();
        if (filterValue.SequenceEqual(chosenCube))
        {
            slideDoorScript.OpenDoor();
        }
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
        Transform n = this.transform.Find(value);
        SnapZoneInput3x3 v = n.GetComponent<SnapZoneInput3x3>();
        Debug.Log("this " + string.Join(" ", v.getInputValue()));
        snapZoneValue = v.getInputValue();
        int result = calculateMatrixOutput();
        if (result == expectedResult)
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
        for (int i = 0; i < 9; i++)
        {
            tot += snapZoneValue[i] * cubeValue[i];
        }
        Debug.Log("operation value: " + tot);
        return tot;
    }

    private string calculateOutputSize()
	{

        return "";
	}

}
