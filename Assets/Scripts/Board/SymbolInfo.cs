using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolInfo : MonoBehaviour
{
    [System.Serializable]
    public class CardInfo
    {
        public Sprite cardImage;
        public string cardName;
        public Sprite pieceSprite_L;
        public Sprite pieceSprite_R;
        public int maxHP;
        public string abilityName;
        [TextArea(2, 4)]public string abilityInformation;
        public int move;
        public int range;
        public int damage;
    }
    public CardInfo[] cardInfos;

    public List<SymbolScript.NameToNum> BoardPieceInfo_Left;
    public List<SymbolScript.NameToNum> BoardPieceInfo_Right;

    public void setBoardPiece(List<SymbolScript.NameToNum> left, List<SymbolScript.NameToNum> right)
    {
        BoardPieceInfo_Left = left;
        BoardPieceInfo_Right = right;

        return;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        BoardPieceInfo_Left = new List<SymbolScript.NameToNum>();
        BoardPieceInfo_Right = new List<SymbolScript.NameToNum>();
    }

    public static SymbolInfo Instance;
}
