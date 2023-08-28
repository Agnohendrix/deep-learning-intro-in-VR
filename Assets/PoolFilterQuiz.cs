using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoolFilterQuiz : MonoBehaviour
{
    public GameObject door;
    private SlideDoor slideDoorScript;

    [SerializeField]
    private GameObject stridePaddingPanel;
    public GameObject lamp;

    public Material green;

    private int padding = 0;
    private int stride = 3;

    private string[] pos = new string[100];

    private GameObject lastSnapped;

    private bool[] correct = { true, false, false, false, false, false, false };

    int expectedResult;

    private int[] snapZoneValue;
    private int[] cubeValue;

    // Start is called before the first frame update
    void Start()
    {
        slideDoorScript = door.GetComponent<SlideDoor>();
    }


    public void input1Snapped(GameObject input)
    {
        lastSnapped = input;
        cubeValue = input.GetComponent<FilterInput>().getInputValue();
    }

    public bool isEven(int value)
	{
        if (value % 2 == 0)
            return true;
        else
            return false;
	}

    public void snapZoneSnapped(string value)
    {
        Debug.Log(lastSnapped.GetComponent<NNInput>().getInputValue() + " cube in snap method " + lastSnapped.name.Substring(lastSnapped.name.Length - 1));
        Debug.Log(value.Substring(value.Length - 1) + " snapzone in snap method, cube: " + lastSnapped.GetComponent<NNInput>().getInputValue().ToString());
        if (isEven(lastSnapped.GetComponent<NNInput>().getInputValue()) == isEven(int.Parse(value.Substring(value.Length - 1))))
        {
            correct[lastSnapped.GetComponent<NNInput>().getInputValue()] = true;
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

        foreach (bool c in correct)
        {
            if (!c)
                allCorrect = false;
        }
        Debug.Log("allCorrect " + allCorrect + " correct array " + string.Join(" ", correct));

        if (allCorrect)
		{
            Material[] lampColors = lamp.GetComponent<MeshRenderer>().materials;
            Debug.Log("material equals: " + (lampColors[1] == green));
            lampColors[1] = green;
            lamp.GetComponent<MeshRenderer>().materials = lampColors;
            slideDoorScript.OpenDoor();
        }
         

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
        for (int i = 0; i < 9; i++)
        {
            tot += snapZoneValue[i] * cubeValue[i];
        }
        Debug.Log("operation value: " + tot);
        return tot;
    }

    private void setFivePositions()
    {
        int rowValue;
        int colValue;
        int max;
        if (padding == 0)
        {
            max = 10;
            rowValue = 1;
            colValue = 1;
        }
        else
        {
            max = 11;
            rowValue = 0;
            colValue = 0;
        }

        int counter = 1;
        //pos[counter] = rowValue.ToString() + colValue.ToString();
        //counter++;
        for (int i = rowValue; i + 3 <= max; i += stride)
        {
            for (int j = colValue; j + 3 <= max; j += stride)
            {
                pos[counter] = i.ToString() + j.ToString();
                counter++;
            }
        }
    }
}
