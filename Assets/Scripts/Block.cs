using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Block : MonoBehaviour
{
    [SerializeField]
    private Transform _topObject;
    [SerializeField]
    private Transform _bottomObject;
    [SerializeField]
    private Transform _gapObject;

    private void Start()
    {
        Color mainColor = GameAssets.Instance.ColorPalette.BlockColor;
        _topObject.GetComponent<SpriteRenderer>().color = mainColor;
        _bottomObject.GetComponent<SpriteRenderer>().color = mainColor;
    }

    public void ChangeGapSize(float gapSize)
    {
        _gapObject.localScale = new Vector3(this._gapObject.localScale.x, gapSize);
        ChangeObjectPos(_bottomObject, -1);
        ChangeObjectPos(_topObject, 1);
    }

    private void ChangeObjectPos(Transform obj, float pos)
    {
        var calcPos = (_gapObject.localScale.y / 2) + (obj.localScale.y / 2);
        obj.localPosition = new Vector3(0, calcPos * pos);
    }

    public void Move(float speed)
    {
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
    }

    public void DestroyObject()
    {
        this.gameObject.SetActive(false);
    }

}
