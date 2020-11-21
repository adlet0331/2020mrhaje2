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
        public int maxHP;
        public string abilityName;
        [TextArea(2, 4)]public string abilityInformation;
        public int move;
        public int range;
        public int damage;
    }
    public CardInfo[] cardInfos;
}
