using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorBola : MonoBehaviour
{
    
    public Camera camara;
    public GameObject suelo;
    public float velocidad=5;

    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    // Start is called before the first frame update
    void Start()
    {
        offset = camara.transform.position;
        CreateSueloInicial();
        DireccionActual = Vector3.forward;
    }

    void CreateSueloInicial()
    {
        for(int i = 0 ; i < 3; i++){
            ValZ +=6.0f;
            Instantiate(suelo,new Vector3(ValX,0,ValZ), Quaternion.identity);
        }
    }


    // Update is called once per frame
    void Update()
    {
        camara.transform.position = transform.position + offset;
        if(Input.GetKeyUp(KeyCode.Space))
        {
            CambiartDireccion();
        }
        transform.Translate(DireccionActual * velocidad * Time.deltaTime );
        
    }

    private void OnCollisionExit(Collision other){
        if(other.gameObject.tag =="Suelo")
        {
            Debug.Log("Rutina");
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo){
        float aleatorio = Random.Range(0.0f,1.0f);

        if(aleatorio > 0.5f)
        {
            ValX += 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        Instantiate(suelo,new Vector3(ValX,0,ValZ),Quaternion.identity);
        yield return new WaitForSeconds(2);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        Destroy(suelo);
    }

    void CambiartDireccion()
    {
        if (DireccionActual == Vector3.forward)
        {
            DireccionActual = Vector3.right;
        }
        else
        {
            DireccionActual = Vector3.forward;
        }
    }
}
