using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject channelingParticleSystem;
    [SerializeField] private GameObject missile;
    [SerializeField] private GameObject hitParticleSystem;

    [SerializeField] private float channelingTime = 1.5f;
    [SerializeField] private float missileSpeed = 1f;
    [SerializeField] private float delayAfterHit = 1f;
    public GameObject Target { get;set; }

    private Coroutine attackCoroutine;
    private Action action;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="action">Action called after attack is completed</param>
    public void StartAttack(Action action, GameObject target)
    {
        this.action = action;
        Target = target;
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private void Update()
    {
        channelingParticleSystem.transform.position = PlayerController.Instance.transform.position;
    }

    IEnumerator AttackCoroutine()
    {
        // Show channeling animation
        channelingParticleSystem.SetActive(true);

        yield return new WaitForSeconds(channelingTime);
        
        // Disable channeling animation and show missile
        channelingParticleSystem.SetActive(false);
        missile.SetActive(true);
        missile.transform.position = PlayerController.Instance.transform.position;
        
        // Move missile towards target until it will reach it
        while (Vector3.Distance(missile.transform.position, Target.transform.position) > 0.001f)
        {
            // Move missile towards target
            float step = Time.deltaTime * missileSpeed;
            Vector3 newPosition = Vector3.MoveTowards(
                missile.transform.position,
                Target.transform.position, 
                step);
            missile.transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }
        
        // Disable missile, run hit animation
        missile.SetActive(false);
        hitParticleSystem.transform.position = Target.transform.position;
        hitParticleSystem.SetActive(true);
        
        // Wait...
        yield return new WaitForSeconds(delayAfterHit);
        
        // Run action...
        action();
    }
}
