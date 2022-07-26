using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitAppication : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
