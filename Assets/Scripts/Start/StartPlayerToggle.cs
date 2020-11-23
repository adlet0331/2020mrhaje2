using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartPlayerToggle : MonoBehaviour, IPointerClickHandler
{
    public bool IsPlayer1;

    public event Action<bool> Checked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Checked?.Invoke(IsPlayer1);
    }
}
