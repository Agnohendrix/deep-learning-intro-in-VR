using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    public Material green;

    private int padding = 0;
    private int stride = 3;

    private string[] pos = new string[100];

    private GameObject lastSnapped;

    private bool[] correct = { true, false, false, false, false, false };

    int expectedResult;

    private int[] snapZoneValue;
    private int[] cubeValue;

    // Start is called before the first frame update
    void Start()
    {
        setFivePositions();
        Debug.Log("stride " + stride);
        Debug.Log("zones " + string.Join(" ", pos));
        //Initialize numbers for snapzones
        GameObject snap1 = transform.Find("Matrix3x311").gameObject;
        GameObject snap2 = transform.Find("Matrix3x312").gameObject;
        GameObject snap3 = transform.Find("Matrix3x313").gameObject;
        GameObject snap4 = transform.Find("Matrix3x321").gameObject;
        GameObject snap5 = transform.Find("Matrix3x322").gameObject;
        GameObject snap6 = transform.Find("Matrix3x323").gameObject;
        GameObject snap7 = transform.Find("Matrix3x331").gameObject;
        GameObject snap8 = transform.Find("Matrix3x332").gameObject;
        GameObject snap9 = transform.Find("Matrix3x333").gameObject;
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
        GameObject cube4 = transform.Find("Interactable.InputFilter4").gameObject;
        GameObject cube5 = transform.Find("Interactable.InputFilter5").gameObject;

        cube1.GetComponent<NNInput>().setInputValue(int.Parse(pos[1]));
        cube2.GetComponent<NNInput>().setInputValue(int.Parse(pos[2]));
        cube3.GetComponent<NNInput>().setInputValue(int.Parse(pos[3]));
        cube4.GetComponent<NNInput>().setInputValue(int.Parse(pos[4]));
        cube5.GetComponent<NNInput>().setInputValue(int.Parse(pos[5]));

        
        //slideDoorScript = door.GetComponent<SlideDoor>();

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

        switch (Random.Range(1, 10))
        {
            case 1:
                snapValue = snap1.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 2:
                snapValue = snap2.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 3:
                snapValue = snap3.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 4:
                snapValue = snap4.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 5:
                snapValue = snap5.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 6:
                snapValue = snap6.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 7:
                snapValue = snap7.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 8:
                snapValue = snap8.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
            case 9:
                snapValue = snap9.GetComponent<SnapZoneInput3x3>().getInputValue();
                break;
        }
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

    // Update is called once per frame
    void Update()
    {
    }

    public void input1Snapped(GameObject input)
    {
        lastSnapped = input;

        cubeValue = input.GetComponent<FilterInput>().getInputValue();
        //Debug.Log("snapped object " + input.name + " " + string.Join(" ", cubeValue));

    }

    public void snapZoneSnapped(string value)
	{
        Debug.Log(lastSnapped.GetComponent<NNInput>().getInputValue() + " cube in snap method " + lastSnapped.name.Substring(lastSnapped.name.Length - 1));
        Debug.Log(value.Substring(value.Length - 2) + " snapzone in snap method");
        if (lastSnapped.GetComponent<NNInput>().getInputValue().ToString() == value.Substring(value.Length - 2))
		{
            correct[int.Parse(lastSnapped.name.Substring(lastSnapped.name.Length - 1))] = true;
		}
        Debug.Log("correct array " + string.Join(" ", correct));
        Debug.Log("snapzone");
        Debug.Log("snapzone object " + value);
        Debug.Log(this.transform.parent.name);
        Transform n = this.transform.Find(value);
        SnapZoneInput3x3 v = n.GetComponent<SnapZoneInput3x3>();
        Debug.Log("this " + string.Join(" ", v.getInputValue()));
        snapZoneValue = v.getInputValue();
        int result = calculateMatrixOutput();
        bool allCorrect = true;

        foreach(bool c in correct)
        {
            if (!c)
                allCorrect = false;
        }

        if(allCorrect)
            slideDoorScript.OpenDoor();

    }

    public void input1UnSnapped(GameObject cube)
    {
        Debug.Log(cube.name);
        correct[int.Parse(cube.name.Substring(cube.name.Length - 1))] = false;
        Debug.Log("correct array " + string.Join(" ", correct));
    }

    public void input2Snapped(GameObject input)
    {
    }

    public void input2UnSnapped()
    {
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
    
    private void setFivePositions()
	{
        if(padding == 0)
		{
            int rowValue = 1;
            int colValue = 1;
            int counter = 1;
            //pos[counter] = rowValue.ToString() + colValue.ToString();
            //counter++;
            for(int i = rowValue; i + 3 <= 10; i+= stride)
			{
                for(int j = colValue; j + 3 <= 10; j += stride)
				{

                        pos[counter] = i.ToString() + j.ToString();
                        counter++;
                     
				}
			}
        }
	}
}
