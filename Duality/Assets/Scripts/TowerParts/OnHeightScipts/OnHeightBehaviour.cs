using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHeightBehaviour : MonoBehaviour
{
    [SerializeField] private float height;
    protected bool usePlayerHeight = false; // This shit should be serializable, but at this stage there is no point
                                            // in following any rules
    
    private bool fired = false;

    private void Update()
    {
        var presentHeight = usePlayerHeight
            ? PlayerController.Instance.transform.position.y
            : GameManager.Instance.ExpectedHeight;
    
        if(!fired && presentHeight >= height)
        {
            fired = true;
            Debug.Log($"Fired OnHeightBehaviour {gameObject.name} {this.GetType()}");
            Action();
        }
    }

    protected abstract void Action();
}
