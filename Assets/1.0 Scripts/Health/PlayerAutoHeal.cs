using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAutoHeal : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Health health;
    [SerializeField] private HealResource healResource;

    [Header("Config")]
    [SerializeField] private float delayAfterDamage = 2f;
    [SerializeField] private float healInterval = 0.1f; 

    private Coroutine healRoutine;
    private float lastDamageTime;

    private void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        PlayerDamageEvent.OnPlayerDamaged += OnPlayerDamaged;
    }

    private void OnDisable()
    {
        PlayerDamageEvent.OnPlayerDamaged -= OnPlayerDamaged;
    }

    private void OnPlayerDamaged()
    {
        lastDamageTime = Time.time;

        if (healRoutine != null)
        {
            StopCoroutine(healRoutine);
            healRoutine = null;
        }
    }

    private void Update()
    {
        if (healRoutine != null) return;
        if (!healResource.CanUse) return;
        if (health.CurrentHealth >= health.MaxHealth) return;

        if (Time.time >= lastDamageTime + delayAfterDamage)
        {
            healRoutine = StartCoroutine(AutoHeal());
        }
    }

    private IEnumerator AutoHeal()
    {
        while (healResource.CanUse &&
               health.CurrentHealth < health.MaxHealth)
        {
            health.TakeDamage(-1);        // hồi 1 HP
            healResource.Consume(1);      // trừ 1 Green

            yield return new WaitForSeconds(healInterval);
        }

        healRoutine = null;
    }
}
