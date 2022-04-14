using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private float yOffset = 0.4f;
    private void Start()
    {
        collider2D.enabled = false;
    }

    private void Update()
    {
        if (PlayerController.Instance.transform.position.y - yOffset > transform.position.y)
        {
            collider2D.enabled = true;
        }
    }
}
