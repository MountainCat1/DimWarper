using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHeightBehaviour : MonoBehaviour
{
    public float height;

    private bool fired = false;

    private void Update()
    {
        if(!fired && GameManager.Instance.ExpectedHeight >= height)
        {
            fired = true;
            Debug.Log($"Fired OnHeightBehaviour {gameObject.name} {this.GetType()}");
            Action();
        }
    }

    protected abstract void Action();
}
