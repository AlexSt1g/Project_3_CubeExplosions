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

    public event UnityAction<int> Split;

    public void ChangeStats(int splitChancePercentage)
    {
        _splitChancePercentage = splitChancePercentage;
        transform.localScale /= _scaleChangeCoefficient;        
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

            Split?.Invoke(splitChancePercentage);
        }
        
        Destroy(gameObject);        
    }  
}
