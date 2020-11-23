using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartTextmesh : MonoBehaviour, IPointerClickHandler
{
    public SymbolScript.NameToNum CardName;

    public event Action<SymbolScript.NameToNum> Clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke(CardName);
    }
}
