using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    // 1 - 왼쪽 // 2 - 오른쪽 //
    public int turn;
    public int actedUnit;

    private GameObject turnUI;
    private Dice dice;
    private Text actedUnitText;

    private void Start()
    {
        turn = 1;
        actedUnit = 0;
        turnUI = GameObject.Find("TurnUI");
        dice = GameObject.Find("Dice").GetComponent<Dice>();
        actedUnitText = GameObject.Find("ActedUnit").GetComponent<Text>();
    }

    private void Update()
    {
        actedUnitText.text = "행동한 유닛: " + actedUnit + "/3";
    }

    public void EndTurnButtonClick()
    {
        if (turn == 1) turn = 2;
        else turn = 1;
        turnUI.transform.Find("TurnText").GetComponent<Text>().text = turn.ToString() + "P Turn";
        foreach (SymbolScript symbol in GameObject.Find("PieceUI").GetComponent<UIControl>().allSymbols)
        {
            symbol.PieceSelect(false);
            symbol.moved = false;
            symbol.attacked = false;
        }
        actedUnit = 0;
        dice.RollDice();
    }
}
