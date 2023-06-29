using UnityEngine;
using System;

public class SnapZoneInput3x3 : MonoBehaviour
{
    [Tooltip("Filter Matrix 3x3 values separated by commas")]
    public string inputValueSetFromEditor;

    public int[] getInputValue()
    {
        int count = inputValueSetFromEditor.Split(',').Length - 1;
        int[] result = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        if (count == 8 && !inputValueSetFromEditor.StartsWith(",") && !inputValueSetFromEditor.EndsWith(","))
        {
            result = getArrayMatrixFromString(inputValueSetFromEditor);
        }
        return result;
    }

    private int[] getArrayMatrixFromString(string input)
    {
        int[] result;
        string[] strlist = input.Split(',');

        result = Array.ConvertAll(strlist, int.Parse);
        return result;
    }
}
