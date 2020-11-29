using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSprite = new Sprite[7];
    public int[] deltaMove = new int[7];
    public int[] deltaRange = new int[7];
    public int[] deltaDamage = new int[7];

    private UIControl pieceUI;
    private TurnManager turnManager;
    private int n;

    // Start is called before the first frame update
    void Start()
    {
        pieceUI = GameObject.Find("PieceUI").GetComponent<UIControl>();
        turnManager = GameObject.Find("Turn Manager").GetComponent<TurnManager>();
        RollDice(); // 처음에도 주사위 굴리기
    }

    public void RollDice()
    {
        n = Random.Range(1, 7);
        transform.Find("Image").GetComponent<Image>().sprite = diceSprite[n];

        // 능력치 보정
        foreach (SymbolScript symbolScript in pieceUI.allSymbols)
        {
            symbolScript.deltaMove = 0;
            symbolScript.deltaRange = 0;
            symbolScript.deltaDamage = 0;

            if (symbolScript.team == turnManager.turn)
            {
                symbolScript.deltaMove = deltaMove[n];
                symbolScript.deltaRange = deltaRange[n];
                symbolScript.deltaDamage = deltaDamage[n];
            }
        }

        transform.Find("Text").GetComponent<Text>().text
            = "이동 거리 " + (deltaMove[n] >= 0 ? "+" : "") + deltaMove[n]
            + " / 공격 거리 " + (deltaRange[n] >= 0 ? "+" : "") + deltaRange[n]
            + " / 공격력 " + (deltaDamage[n] >= 0 ? "+" : "") + deltaDamage[n] + "%";
    }
}
