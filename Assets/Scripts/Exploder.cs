using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;        

    private void OnEnable()
    {
        _cubeSpawner.Spawned += Expload;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= Expload;
    } 
    
    private void Expload(List<Cube> cubes)
    {
        foreach (var cube in cubes)        
            cube.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, transform.position, _explosionRadius);      
    }
}
