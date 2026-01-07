using UnityEngine;
using System;
using JS;
using Zenject;

public class PlayerDamageEvent : MonoBehaviour
{
    [SerializeField] private Health health;

    public static event Action OnPlayerDamaged;

    private int lastHealth;
    private bool isDead;

    private IUIService uiService;

    [Inject]
    public void Construct(IUIService uiService)
    {
        this.uiService = uiService;
    }

    private void Awake()
    {
        if (health == null)
            health = GetComponent<Health>();
        isDead = false;
        lastHealth = health.CurrentHealth;
    }

    private void OnEnable()
    {
        health.OnHealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int current, int max)
    {
        if (current < lastHealth)
        {
            OnPlayerDamaged?.Invoke();
        }

        if (!isDead && current <= 0)
        {
            isDead = true;
            ShowGameOver();
        }

        lastHealth = current;
    }

    private void ShowGameOver()
    {
        uiService.Show<GameOverPopup>();
    }
}
