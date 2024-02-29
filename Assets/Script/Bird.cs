using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(-transform.right * Time.deltaTime * 1);
    }
}
