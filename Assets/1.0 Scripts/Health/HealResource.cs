using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealResource : MonoBehaviour
{
    [SerializeField] private int maxResource = 100;
    public int Current { get; private set; }
    public int Max => maxResource;
    public event Action<int, int> OnResourceChanged;

    private void Start()
    {
        Current = maxResource;
        OnResourceChanged?.Invoke(Current, maxResource);
    }

    public bool CanUse => Current > 0;

    public void Init(int Value)
    {
        maxResource = Value;
        Current = maxResource;
        OnResourceChanged?.Invoke(Current, maxResource);
    }

    public void Consume(int amount)
    {
        Current = Mathf.Max(Current - amount, 0);
        OnResourceChanged?.Invoke(Current, maxResource);
    }

    public void RefillFull()
    {
        Current = maxResource;
        OnResourceChanged?.Invoke(Current, maxResource);
    }
}
