using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    private const int MaxSplitChancePercentage = 100;
    private const int MinSplitChancePercentage = 1;

    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;
    
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

        Explode();
        Destroy(gameObject);        
    }
    
    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            explodableObject.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }        
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Vector3 halfOfScale = transform.localScale / 2;
        
        Collider[] hitColliders = Physics.OverlapBox(transform.position, halfOfScale);

        List<Rigidbody> cubes = new();

        foreach (Collider hitCollider in hitColliders)        
            if (hitCollider.attachedRigidbody != null)            
                cubes.Add(hitCollider.attachedRigidbody);        

        return cubes;
    }
}
