using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private Renderer _material;
    private TextMesh _text;
    private BoxCollider _collider;

    private int _murationTime = 10;
    private float _normalSize = 0.5f;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _text = GetComponentInChildren<TextMesh>();
        _material = GetComponent<Renderer>();
    }

    public void StartMuration()
    {
        Default();
        StartCoroutine(Maturation());
    }

    private void Default()
    {
        _murationTime = 10;
        _text.text = _murationTime.ToString();
        _material.material.color = Color.yellow;
        transform.localScale = Vector3.one * _normalSize;
        _collider.enabled = false;
    }

    private IEnumerator Maturation()
    {
        var time = new WaitForSeconds(1.0f);

        for (int i = _murationTime; i > 0; i--)
        {
            yield return time;
            _murationTime--;
            _text.text = _murationTime.ToString();
        }

        _material.material.color = Color.green;
        transform.localScale = Vector3.one;
        _text.text = "Can Take";
        _collider.enabled = true;
    }
}