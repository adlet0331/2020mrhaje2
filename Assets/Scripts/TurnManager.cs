using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // 1 - 왼쪽 // 2 - 오른쪽 //
    public int turn;
    private GameObject turnUI;

    private void Start()
    {
        turn = 1;
        turnUI = GameObject.Find("TurnUI");
    }

    public void EndTurnButtonClick()
    {
        if (turn == 1) turn = 2;
        else turn = 1;
        turnUI.transform.Find("Text").GetComponent<Text>().text = turn.ToString() + "P Turn";
        foreach (SymbolScript symbol in GameObject.Find("PieceUI").GetComponent<UIControl>().allSymbols) symbol.PieceSelect(false);
    }
}
