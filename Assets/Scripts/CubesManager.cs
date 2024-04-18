using UnityEngine;

public class CubesManager : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Cube[] _cubes;

    private void Start()
    {
        Invoke(nameof(SetCubesDeactive), 1f);
    }

    private void SetCubesDeactive()
    {
        foreach (var cube in _cubes)
        {
            cube.gameObject.SetActive(false);
        }
    }

    public void SpawnCube()
    {
        foreach (var cube in _cubes)
        {
            if (!cube.gameObject.activeSelf)
            {
                cube.gameObject.SetActive(true);
                cube.StartMuration();
                Vector3 newPosition = _playerPosition.position + _playerPosition.forward;
                cube.transform.position = newPosition;
                return;
            }
        }
    }
}