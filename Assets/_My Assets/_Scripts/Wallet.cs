using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet instance;

    [Header("References")]
    [SerializeField] private ResourcePresentor coinsPresentor;
    [SerializeField] private ResourcePresentor woodPresentor;

    [Header("Options")]
    [SerializeField] private int woodCount;
    [SerializeField] private int coinsCount;
    [SerializeField] private int initialCoinsCount;
    [SerializeField] private int initialWoodCount;
    [SerializeField] private int revenuePeriod;

    private float timer;

    public delegate void CountChanged(int newCount);
    public event CountChanged OnCoinsChanged;
    public event CountChanged OnWoodChanged;

    private void OnEnable()
    {
        OnCoinsChanged += coinsPresentor.DisplayResourceCount;
        OnWoodChanged += woodPresentor.DisplayResourceCount;
    }

    private void OnDisable()
    {
        OnCoinsChanged -= coinsPresentor.DisplayResourceCount;
        OnWoodChanged -= woodPresentor.DisplayResourceCount;
    }

    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    private void Start()
    {
        timer = 0f;
        coinsCount = initialCoinsCount;
        woodCount = initialWoodCount;
        OnCoinsChanged?.Invoke(coinsCount);
        OnWoodChanged?.Invoke(woodCount);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > revenuePeriod)
        {
            timer = 0f;
            coinsCount++;
            OnCoinsChanged?.Invoke(coinsCount);
        }
    }

    public void AddCoins(int value)
    {
        if (value <= 0) return;

        coinsCount += value;

        OnCoinsChanged?.Invoke(coinsCount);
    }

    public bool TrySpendCoins(int count)
    {
        if (coinsCount < count) return false;

        coinsCount -= count;
        return true;
    }

    public void AddWood(int value)
    {
        if (value <= 0) return;

        woodCount += value;

        OnWoodChanged?.Invoke(woodCount);
    }
}
