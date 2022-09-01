using System.Collections;
using UnityEngine;

public class CandleFlicker : MonoBehaviour
{
    private Transform _candelabra;
    private MeshRenderer _renderer;
    private Material _sharedMaterial;

    [Range(0, 1)]
    public float flickerRate = .3f;

    [Range(1, 3)]
    public float flickerIntensity = 2f;

    private float baseIntensity = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponentInChildren<MeshRenderer>();
        _candelabra = _renderer.transform;
        _sharedMaterial = _renderer.sharedMaterial;
        var color = _sharedMaterial.GetColor("_EmissionColor");
        baseIntensity = color.r;

        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            var targetIntensity = baseIntensity + Random.Range(-flickerIntensity, flickerIntensity);
            var c = Mathf.Lerp(baseIntensity, targetIntensity, flickerRate);
            _sharedMaterial.SetColor("_EmissionColor", new Vector4(c, c, c, 1f));
            yield return new WaitForSeconds(flickerRate);
        }
    }
}
