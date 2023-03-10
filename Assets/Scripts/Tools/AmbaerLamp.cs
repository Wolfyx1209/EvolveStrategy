using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbaerLamp : MonoBehaviour
{
    [field: SerializeField] private int _numberOfBeetles;
    public int numberOfBeetles
    {
        get => _numberOfBeetles;
        set
        {
            _numberOfBeetles = Mathf.Clamp(value, 0, _maxBeetlesValue);
            RecalculateBurningTime();
        }
    }

    [field: SerializeField] private float _fuelQuantity;
    public float fuelQuantity
    {
        get => _fuelQuantity;
        set
        {
            _fuelQuantity = Mathf.Clamp(value, 0, _maxFuelQuantity);
            RecalculateBurningTime();
        }
    }

    [field: SerializeField] private int _minBeetlesToLightLamp;
    [field: SerializeField] private float _minBurnTimeToLightLamp;

    [field: SerializeField] private int _maxBeetlesValue;
    [field: SerializeField] private float _maxFuelQuantity;

    [field: SerializeField] private float _fuelConsumptionOneBeetle;
    [field: SerializeField] private float _fuelToTimeIndex;

    [SerializeField] public float burningTime { get; private set; }
    [SerializeField] public bool isLit { get; private set; }

    public void LightLamp()
    {
        if (IsLampReadyToLight())
        {
            isLit = true;
            StartCoroutine(LampShining());
        }
    }

    public bool IsLampReadyToLight()
    {
        RecalculateBurningTime();
        if (numberOfBeetles > _minBeetlesToLightLamp &&
            burningTime > _minBurnTimeToLightLamp)
        {
            return true;
        }
        return false;
    }

    public void PutOutLamp()
    {
        if (isLit)
        {
            StopCoroutine(LampShining());
        }
        isLit = false;
    }

    IEnumerator LampShining()
    {
        while (isLit && fuelQuantity > 0)
        {
            float fuelBurned = numberOfBeetles * _fuelConsumptionOneBeetle * Time.deltaTime / _fuelToTimeIndex;
            fuelQuantity -= fuelBurned;
            yield return null;
        }
        isLit = false;
        PutOutLamp();
    }

    private void RecalculateBurningTime()
    {
        burningTime = fuelQuantity * _fuelToTimeIndex/ (numberOfBeetles * _fuelConsumptionOneBeetle);
    }
}
