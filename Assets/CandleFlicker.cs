using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class CandleFlicker : MonoBehaviour
{
    private Transform candelabra;
    private MeshRenderer meshRenderer;
    private Material sharedMaterial;

    [Range(0, 1)] public float flickerRate = .3f;
    [Range(1, 3)] public float flickerIntensity = 2f;
    [Range(0, 45)] public float swayAmount = 5;
    [Range(0, 45)] public float spinAmount = 20f;

    private float baseIntensity = 1f;

    private Quaternion baseRotation;
    private Vector3 swayDirection;
    private Vector3 spinDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        candelabra = meshRenderer.transform;
        sharedMaterial = meshRenderer.sharedMaterial;
        var color = sharedMaterial.GetColor("_EmissionColor");
        baseIntensity = color.r;
        
        baseRotation = candelabra.localRotation;
        swayDirection = candelabra.forward;
        spinDirection = Vector3.up;
        
        StartCoroutine(Flicker());
    }

    private void Update()
    {
        var swayAngle = Mathf.Sin(Time.time) * swayAmount;
        var sway = Quaternion.Euler(swayDirection * swayAngle);
        candelabra.localRotation = baseRotation * sway;
        
        var spinAngle = Mathf.Cos(Time.time) * spinAmount;
        var spin = Quaternion.Euler(spinDirection * spinAngle);
        transform.rotation = spin;
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            var targetIntensity = baseIntensity + Random.Range(-flickerIntensity, flickerIntensity);
            var c = Mathf.Lerp(baseIntensity, targetIntensity, flickerRate);
            sharedMaterial.SetColor("_EmissionColor", new Vector4(c, c, c, 1f));
            yield return new WaitForSeconds(flickerRate);
        }
    }
}
