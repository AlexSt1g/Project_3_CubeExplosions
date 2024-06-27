using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const int MaxSplitChancePercentage = 100;
    private const int MinSplitChancePercentage = 1;    
    
    private int _splitChancePercentage = MaxSplitChancePercentage;
    private int _splitChancePercentageChangeCoefficient = 2;
    private int _scaleChangeCoefficient = 2;
    private int _explodeMultiplier = 1;
    private int _explodeMultiplierChangeCoefficient = 2;

    public event UnityAction<int, int> Split;
    public event UnityAction<int> NotSplit;    

    public void ChangeStats(int splitChancePercentage, int explodeMultiplier)
    {
        _splitChancePercentage = splitChancePercentage;
        transform.localScale /= _scaleChangeCoefficient;
        _explodeMultiplier = explodeMultiplier;
    }

    private void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();          
    }    

    private void OnMouseDown()
    {
        if (_splitChancePercentage != 0 && Random.Range(MinSplitChancePercentage, MaxSplitChancePercentage + 1) <= _splitChancePercentage)
        {                 
            int splitChancePercentage = _splitChancePercentage / _splitChancePercentageChangeCoefficient;
            int explodeMultiplier = _explodeMultiplier * _explodeMultiplierChangeCoefficient;

            Split?.Invoke(splitChancePercentage, explodeMultiplier);
        }
        else
        {
            NotSplit?.Invoke(_explodeMultiplier);
        }
        
        Destroy(gameObject);        
    }  
}
