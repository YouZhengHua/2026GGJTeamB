using System.Collections.Generic;
using UnityEngine;

public class TestParticleControl : MonoBehaviour
{
    public GameObject PS_obj;
    public ParticleSystem PS;
    public ParticleSystem.Particle[] particles;

    public void SpawnClickParticle()
    {
        Vector3 mousePos = Input.mousePosition;
        //mousePos.z = Camera.main.nearClipPlane;
        mousePos.z = 42f;
        Vector3 worldMousePos= Camera.main.ScreenToWorldPoint(mousePos);
        Destroy(Instantiate(PS_obj, worldMousePos, Quaternion.identity), 4f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SpawnClickParticle();
        }
    }
}
