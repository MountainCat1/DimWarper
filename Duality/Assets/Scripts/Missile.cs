using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Missile : MonoBehaviour
{
    public Vector2 Direction { get;  set; }
    public float speed = 2f;

    void FixedUpdate()
    {
        float step = Time.fixedDeltaTime * speed;

        Vector3 direction3D = new Vector3(Direction.x, Direction.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction3D, step);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            PlayerController.Instance.Kill();
        }
    }
}
