using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private Cube _cube;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;        

    private void OnEnable()
    {
        _cubeSpawner.Spawned += Explode;
        _cube.NotSplit += Explode;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= Explode;
        _cube.NotSplit -= Explode;
    } 
    
    private void Explode(List<Cube> cubes)
    {
        foreach (var cube in cubes)        
            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);      
    }
    
    private void Explode(int explodeMultiplier)
    {
        float explosionForce = _explosionForce * explodeMultiplier;
        float explosionRadius = _explosionRadius * explodeMultiplier;


        foreach (Rigidbody explodableObject in GetExpodableObjects(explosionRadius))        
            explodableObject.AddExplosionForce(explosionForce, transform.position, explosionRadius);        
    }

    private List<Rigidbody> GetExpodableObjects(float explosionRadius)
    {
        List<Rigidbody> cubes = new();

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)        
            if (hit.attachedRigidbody != null)            
                cubes.Add(hit.attachedRigidbody);           
        
        return cubes;
    }
}
