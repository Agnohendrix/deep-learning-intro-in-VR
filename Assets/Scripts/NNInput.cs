using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNInput : MonoBehaviour
{
    public int inputValue;

    public int getInputValue()
    {
        return inputValue;
    }

    public void setInputValue(int n)
	{
        this.inputValue = n;
	}
}
