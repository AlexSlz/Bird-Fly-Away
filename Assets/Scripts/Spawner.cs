using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Block _spawnPrefab;

    [SerializeField]
    private float _distanceBetweenBlock = 6;
    [SerializeField]
    private float _blockSpeed = 3;
    [SerializeField]
    private float _gapSize = 3;

    private float _spawnRate = 1.7f;

    [SerializeField]
    private List<Block> _blockList = new List<Block>();
    public ReadOnlyCollection<Block> BlockList => _blockList.AsReadOnly();

    private float _leftEdge, _rightEdge, _height;
    public float LeftEdge => _leftEdge;

    private void Start()
    {
        var mainCamera = Camera.main;
        _leftEdge = mainCamera.ViewportToWorldPoint(Vector3.zero).x - 1f;
        _rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x + 1f;
        _height = mainCamera.orthographicSize;
        UpdateSpeed(_blockSpeed);
    }


    private Coroutine SpawnCoroutine = null;
    private void OnEnable()
    {
        //InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
        SpawnCoroutine = StartCoroutine(SpawnObjectCoroutine());
    }
    private void OnDisable()
    {
        //CancelInvoke(nameof(Spawn));
        if (SpawnCoroutine != null)
            StopCoroutine(SpawnCoroutine);
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        Spawn();
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Spawn();
        }
    }
    private void UpdateSpeed(float speed)
    {
        _spawnRate = _distanceBetweenBlock / speed;
        _blockSpeed = speed;
    }

    private void Spawn()
    {
        var posLimit = _height - _gapSize * .5f;
        var yPos = Random.Range(-posLimit, posLimit);

        CreateBlock(yPos, _gapSize);
    }

    private void Update()
    {
        foreach (var block in BlockList)
        {
            if (block.transform.position.x <= LeftEdge)
            {
                block.DestroyObject();
                continue;
            }
            block.Move(_blockSpeed);
        }
    }

    private List<Block> GetDisabledObjects()
    {
        return _blockList.Where(item => !item.gameObject.activeSelf).ToList();
    }
    
    private void CreateBlock(float yPos, float size = 3)
    {
        Block temp;
        var disabledObjects = GetDisabledObjects();
        if (disabledObjects.Count() <= 0)
        {
            temp = Instantiate(_spawnPrefab, this.transform);
            _blockList.Add(temp);
        }
        else
        {
            temp = disabledObjects.FirstOrDefault();
        }
        temp.ChangeGapSize(size);
        temp.transform.position = new Vector3(_rightEdge, yPos);
        temp.gameObject.SetActive(true);
    }
}
