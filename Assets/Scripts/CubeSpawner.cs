using UnityEngine;

public class CubeSpawner : MonoBehaviour
{    
    [SerializeField] private Cube _cube;

    private int _minNumberOfCubes = 2;
    private int _maxNumberOfCubes = 6;    

    private void OnEnable()
    {
        _cube.Split += Spawn;
    }

    private void OnDisable()
    {
        _cube.Split -= Spawn;
    }

    private void Spawn(int splitChancePercentage)
    {        
        int numberOfCubes = Random.Range(_minNumberOfCubes, _maxNumberOfCubes + 1);      

        for (int i = 0; i < numberOfCubes; i++)
        {
            Cube newCube = Instantiate(_cube, transform.position, Quaternion.identity);            

            newCube.ChangeStats(splitChancePercentage);    
        }        
    }
}
