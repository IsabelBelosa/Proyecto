using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    public float speed = 20.0f;
    public GameObject particulas;
    public AudioSource moneda;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime, Space.World);
    }

    void OnDestroy(){
        Vector3 pos = new Vector3(transform.position.x,2.0f,transform.position.z);
        Instantiate(particulas,pos,particulas.transform.rotation);
        moneda.Play();
    }
}
