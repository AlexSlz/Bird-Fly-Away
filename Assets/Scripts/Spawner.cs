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

    [Header("Speed")]
    [SerializeField]
    private float _distanceBetweenBlock = 6;
    [SerializeField]
    private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;
    [SerializeField]
    private float _minSpeed;
    public float MinSpeed => _minSpeed;
    private float _blockSpeed;

    public float BlockSpeed { get { return _blockSpeed; } set { UpdateSpeed(value); } }

    [Header("Gap Size")]
    [SerializeField]
    private float _maxGapSize;
    [SerializeField]
    private float _minGapSize;
    public float MinGapSize => _minGapSize;
    public float MaxGapSize => _maxGapSize;
    [HideInInspector]
    public float GapSize = 3;

    private float _spawnRate = 1.7f;

    [SerializeField]
    private List<Block> _blockList = new List<Block>();
    public ReadOnlyCollection<Block> BlockList => _blockList.AsReadOnly();

    private float _leftEdge, _rightEdge, _height;

    private void Start()
    {
        var mainCamera = Camera.main;
        _leftEdge = mainCamera.ViewportToWorldPoint(Vector3.zero).x - 1f;
        _rightEdge = mainCamera.ViewportToWorldPoint(new Vector3(1,0,0)).x + 1f;
        _height = mainCamera.orthographicSize;
        BlockSpeed = _minSpeed;
        GapSize = _maxGapSize;
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
        speed = Mathf.Clamp(speed, _minSpeed, _maxSpeed);
        _spawnRate = _distanceBetweenBlock / speed;
        _blockSpeed = speed;
    }

    private float _yPrevPos = 0;
    private void Spawn()
    {
        GapSize = Mathf.Clamp(GapSize, _minGapSize, _maxGapSize);
        var yPos = _yPrevPos + Random.Range(-2.0f, 2.0f);
        _yPrevPos = Mathf.Clamp(yPos, -_height + GapSize, +_height - GapSize);
        CreateBlock(yPos, GapSize);
    }

    private void Update()
    {
        foreach (var block in BlockList)
        {
            if (block.transform.position.x <= _leftEdge)
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
