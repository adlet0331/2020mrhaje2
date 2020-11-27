using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapGameBoard : MonoBehaviour
{
    public int index;
    private UIControl PieceUI;

    private void Start()
    {
        PieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
    }
}
