using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public Image Background;
    public Color Standard;
    public Color Highlighted;

    public void OnGazeEnter()
    {
        Background.color = Highlighted;
    }

    public void OnGazeExit()
    {
        Background.color = Standard;
    }

}
