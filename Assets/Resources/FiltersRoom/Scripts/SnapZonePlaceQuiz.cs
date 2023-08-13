using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SnapZonePlaceQuiz : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    public GameObject lamp;
    private bool solved = false;

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

    int expectedResult;

    private int[] snapZoneValue;
    private int[] cubeValue;

    private int[] chosenCube;

    // Start is called before the first frame update
    void Start()
    {
        /*Material[] lampColors = lamp.GetComponent<MeshRenderer>().materials;
        lampColors[1] = green;
        lamp.GetComponent<MeshRenderer>().materials = lampColors;
        Debug.Log(green);*/

        //Initialize numbers for snapzones
        GameObject snap1 = transform.Find("InputSnapZone11").gameObject;
        GameObject snap2 = transform.Find("InputSnapZone12").gameObject;
        GameObject snap3 = transform.Find("InputSnapZone13").gameObject;
        GameObject snap4 = transform.Find("InputSnapZone21").gameObject;
        GameObject snap5 = transform.Find("InputSnapZone22").gameObject;
        GameObject snap6 = transform.Find("InputSnapZone23").gameObject;
        GameObject snap7 = transform.Find("InputSnapZone31").gameObject;
        GameObject snap8 = transform.Find("InputSnapZone32").gameObject;
        GameObject snap9 = transform.Find("InputSnapZone33").gameObject;
        setRandomValues3x3(snap1, "snapzone");
        setRandomValues3x3(snap2, "snapzone");
        setRandomValues3x3(snap3, "snapzone");
        setRandomValues3x3(snap4, "snapzone");
        setRandomValues3x3(snap5, "snapzone");
        setRandomValues3x3(snap6, "snapzone");
        setRandomValues3x3(snap7, "snapzone");
        setRandomValues3x3(snap8, "snapzone");
        setRandomValues3x3(snap9, "snapzone");

        //Initialize numbers for cubes
        GameObject cube1 = transform.Find("Interactable.InputFilter1").gameObject;
        GameObject cube2 = transform.Find("Interactable.InputFilter2").gameObject;
        GameObject cube3 = transform.Find("Interactable.InputFilter3").gameObject;
        setRandomValues3x3(cube1, "cube");
        setRandomValues3x3(cube2, "cube");
        setRandomValues3x3(cube3, "cube");

        slideDoorScript = door.GetComponent<SlideDoor>();

        //Randomizes correct objects to open the door
        int[] cubeValue = { };
        int[] snapValue = { };
        switch (Random.Range(1, 4))
        {
            case 1:
                cubeValue = cube1.GetComponent<FilterInput>().getInputValue();
                break;
            case 2:
                cubeValue = cube2.GetComponent<FilterInput>().getInputValue();
                break;
            case 3:
                cubeValue = cube3.GetComponent<FilterInput>().getInputValue();
                break;
        }
        chosenCube = cubeValue;

        //Calculates and prints output matrix
        int tot = 0;
        snapValue = snap1.GetComponent<SnapZoneInput3x3>().getInputValue();
        for(int i = 0; i< 9; i++)
		{
            tot += snapValue[i] * cubeValue[i];
		}
        transform.Find("OutputSnapZone11").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        //Repeat for all matrix
        snapValue = snap2.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone12").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap3.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone13").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap4.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone21").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap5.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone22").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap6.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone23").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap7.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone31").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap8.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone32").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        tot = 0;

        snapValue = snap9.GetComponent<SnapZoneInput3x3>().getInputValue();
        for (int i = 0; i < 9; i++)
        {
            tot += snapValue[i] * cubeValue[i];
        }
        transform.Find("OutputSnapZone33").Find("Text").GetComponent<TextMeshPro>().SetText(tot.ToString());
        
        
    }


    //Sets random values for matrix snapzones and cubes
    void setRandomValues3x3(GameObject obj, string type)
    {
        int maxRange;

        string editorValue = "";
        string textMeshValue = "";
        int v;

        if (type == "cube")
        {
            //Initialize values for cube
            maxRange = 2;
            for (int i = 1; i < 10; i++)
            {
                v = Random.Range(0, maxRange);
                //Used to calculate matrix results
                editorValue += v;
                textMeshValue += v;
                if (i < 9)
                    editorValue += ",";

                //Used to visualize the values
                if (i % 3 == 0 && i < 9)
                {
                    textMeshValue += "\n";
                }
                else if (i < 9)
                {
                    textMeshValue += " ";
                }
            }
            obj.GetComponent<FilterInput>().setInputValueSetFromEditor(editorValue);
            obj.transform.Find("Meshes").Find("Input").Find("Canvas (1)").Find("Title").GetComponent<TextMeshProUGUI>().SetText(textMeshValue);
        }
        else if (type == "snapzone")
        {
            //Initialize values for snapzone
            maxRange = 10;
            for (int i = 1; i < 10; i++)
            {
                v = Random.Range(0, maxRange);
                //Used to calculate matrix results
                editorValue += v;
                textMeshValue += v;
                if (i < 9)
                    editorValue += ",";

                //Used to visualize the values
                if (i % 3 == 0 && i < 9)
                {
                    textMeshValue += "\n";
                }
                else if (i < 9)
                {
                    textMeshValue += " ";
                }
            }
            obj.GetComponent<SnapZoneInput3x3>().setInputValueSetFromEditor(editorValue);
            obj.transform.Find("Text").GetComponent<TextMeshPro>().SetText(textMeshValue);

        }
    }

    public void filterSnapped(GameObject input)
	{
        int[] filterValue = input.GetComponent<FilterInput>().getInputValue();
        if (filterValue.SequenceEqual(chosenCube))
		{
            Material[] lampColors = lamp.GetComponent<MeshRenderer>().materials;
            lampColors[1] = green;
            lamp.GetComponent<MeshRenderer>().materials = lampColors;
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
		{
            //If other task is complete
            slideDoorScript.OpenDoor();
        }
            

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
