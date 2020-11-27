using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showScreen : MonoBehaviour
{
    private Vector3 v;

    public void Start()
    {
        v = transform.position;
    }

    public void SetLocation(float value)
    {
        transform.position = v + new Vector3(655*value, 0, 0);
    }

}
