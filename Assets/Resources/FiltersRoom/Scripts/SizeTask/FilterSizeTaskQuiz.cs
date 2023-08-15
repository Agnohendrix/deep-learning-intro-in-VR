using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class FilterSizeTaskQuiz : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    public GameObject lamp;
    public GameObject lampOther;

    [SerializeField]
    private GameObject stridePaddingPanel;
    [SerializeField]
    private GameObject outputPanel;

    public Material green;

    [SerializeField]
    private int inputSize = 9;

    [SerializeField]
    private int stride = 2;
    [SerializeField]
    private int padding = 1;

    private int chosenCube;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize numbers for cubes
        GameObject cube1 = transform.Find("Interactable.InputFilter1").gameObject;
        GameObject cube2 = transform.Find("Interactable.InputFilter2").gameObject;
        GameObject cube3 = transform.Find("Interactable.InputFilter3").gameObject;

        //TODO Select filter value from cube
        //Randomizes correct objects to open the door
        int cubeValue = 0;
        int[] snapValue = { };
        switch (Random.Range(1, 4))
        {
            case 1:
                cubeValue = cube1.GetComponent<NNInput>().getInputValue();
                break;
            case 2:
                cubeValue = cube2.GetComponent<NNInput>().getInputValue();
                break;
            case 3:
                cubeValue = cube3.GetComponent<NNInput>().getInputValue();
                break;
        }

        chosenCube = cubeValue;
        int output = calculateOutputSize();
        stridePaddingPanel.GetComponent<TextMeshProUGUI>().SetText("Parameters are:\nStride = " + stride + "\n Padding = " + padding);
        outputPanel.GetComponent<TextMeshProUGUI>().SetText("Output size is " + output + "x" + output);
        slideDoorScript = door.GetComponent<SlideDoor>();
    }

    public void filterSnapped(GameObject input)
    {
        int filterValue = input.GetComponent<NNInput>().getInputValue();
        if (filterValue == chosenCube)
        {
            Material[] lampColors = lamp.GetComponent<MeshRenderer>().materials;
            lampColors[1] = green;
            lamp.GetComponent<MeshRenderer>().materials = lampColors;

            Material[] otherLampColors = lampOther.GetComponent<MeshRenderer>().materials;
            //If other lamp is green open the door
            if (otherLampColors[1].name.Contains("green"))
                slideDoorScript.OpenDoor();
        }
    }

    private int calculateOutputSize()
	{
        return ((inputSize - chosenCube + 2 * padding) / stride) + 1;
	}

}
