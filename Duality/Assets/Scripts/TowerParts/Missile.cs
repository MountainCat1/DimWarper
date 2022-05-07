using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Missile : MonoBehaviour
{
    public Vector2 Direction { get;  set; }

    [SerializeField] private ParticleSystem hitParticles; 

    [SerializeField] private float speed = 2f;
    [SerializeField] private float targetSpeed = 3.5f;
    [SerializeField] private float acceleration = 0.4f;

    [SerializeField] private Vector3 audioClipPlayPositionOffset = new Vector3(0, 4, 0);
    [SerializeField] private AudioClip missileShotSound;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(
            missileShotSound, 
            Camera.main.transform.position + audioClipPlayPositionOffset);
    }

    void FixedUpdate()
    {
        float step = Time.fixedDeltaTime * speed;

        Vector3 direction3D = new Vector3(Direction.x, Direction.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction3D, step);

        speed = Mathf.Min(speed + Time.deltaTime * acceleration, targetSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerController.Instance.gameObject)
        {
            hitParticles.gameObject.SetActive(true);
            PlayerController.Instance.Kill();
        }
    }

    protected virtual void Hit(Collider2D hit) { }
}
