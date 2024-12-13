using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject environment;
    private void Start()
    {
        Instantiate(environment,transform.position,transform.rotation);
    }
}
