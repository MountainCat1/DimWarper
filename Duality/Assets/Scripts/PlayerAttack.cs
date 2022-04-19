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

    [Space]
    [SerializeField] private AudioSource channelingAudio;
    [SerializeField] private AudioSource shotAudioSource;
    [SerializeField] private AudioSource hitAudioSource;
    
    [Space]
    [SerializeField] private float startDelay = 2f;
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
        yield return new WaitForSeconds(startDelay);
        
        // CHANNELING
        channelingParticleSystem.SetActive(true);
        channelingAudio.Play();

        yield return new WaitForSeconds(channelingTime);
        
        // SHOT
        DisableParticleSystem(channelingParticleSystem.GetComponentsInChildren<ParticleSystem>());
        missile.SetActive(true);
        missile.transform.position = PlayerController.Instance.transform.position;
        
        channelingAudio.Stop();
        shotAudioSource.Play();
        
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
        
        // HIT
        DisableParticleSystem(missile.GetComponentsInChildren<ParticleSystem>());
        
        hitParticleSystem.transform.position = Target.transform.position;
        hitParticleSystem.SetActive(true);
        
        shotAudioSource.Stop();
        hitAudioSource.Play();
        
        // Wait...
        yield return new WaitForSeconds(delayAfterHit);
        
        // Run action...
        action();
    }
    
    private void DisableParticleSystem(IEnumerable<ParticleSystem> particleSystems)
    {
        foreach (var particleSystem in particleSystems)
        {
            var emissionModule = particleSystem.emission;
            emissionModule.rateOverTime = 0;
        }
    }
}
