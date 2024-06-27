using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeSpawner : MonoBehaviour
{    
    [SerializeField] private Cube _cube;

    private int _minNumberOfCubes = 2;
    private int _maxNumberOfCubes = 6;    

    public event UnityAction <List<Cube>> Spawned;

    private void OnEnable()
    {
        _cube.Split += Spawn;
    }

    private void OnDisable()
    {
        _cube.Split -= Spawn;
    }

    private void Spawn(int splitChancePercentage, int explodeMultiplier)
    {        
        int numberOfCubes = Random.Range(_minNumberOfCubes, _maxNumberOfCubes + 1);
        List<Cube> newCubes = new();

        for (int i = 0; i < numberOfCubes; i++)
        {
            Cube newCube = Instantiate(_cube, transform.position, Quaternion.identity);            

            newCube.ChangeStats(splitChancePercentage, explodeMultiplier);
            
            newCubes.Add(newCube);
        }
        
        Spawned?.Invoke(newCubes);
    }
}
