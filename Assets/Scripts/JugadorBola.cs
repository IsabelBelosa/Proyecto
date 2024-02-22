using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorBola : MonoBehaviour
{
    
    public Camera camara;
    public GameObject suelo;
    public float velocidad=5;
    public GameObject moneda;
    public GameObject tronco;
    public Text Contador;

    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int TotalMonedas = 0;
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

        float ran = Random.Range(0f,1f);
        if(ran < 1f) //Cada suelo que se genera tiene un 70% de posibilidades de poseer una moneda
        {
            ran = Random.Range(-2f,2f);
            Instantiate(moneda,new Vector3(ValX + ran,1.5f,ValZ + ran), Quaternion.identity);
        }
        
        float ran2 =  Random.Range(0f,1f);
        if(ran2 < 0.5f) //Cada suelo que se genera tiene un 30% de posibilidades de poseer una moneda
        { 
            ran2 = Random.Range(-3f,3f);
            Instantiate(tronco,new Vector3(ValX + ran2,1.5f,ValZ + ran2), Quaternion.identity);
        }
    }

    void CambiartDireccion()
    {
        if (DireccionActual == Vector3.forward)
        {
            DireccionActual =  Vector3.right;
        }
        else
        {
            DireccionActual =  Vector3.forward;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Premio"))
        {
        TotalMonedas++;
        Contador.text = "Contador =" + TotalMonedas + "/5";
        Destroy(other.gameObject);
        if (TotalMonedas == 5)
        {
            SceneManager.LoadScene("Nivel2");
        }
        }
        if(other.gameObject.CompareTag("tronco"))
        {
            Debug.Log("He chocado con un tronco");
            SceneManager.LoadScene("HasPerdido");
        }
    } 
}
