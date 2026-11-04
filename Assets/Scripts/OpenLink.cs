using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{

    public string URL;

    private void OnTriggerEnter(Collider other)
    {
        Application.OpenURL(URL);
    }
}
