using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorNivel2 : MonoBehaviour
{
    public Camera camara;
    public GameObject suelo;
    public GameObject bola;
    public float velocidad = 5;
    public GameObject moneda;
    public GameObject tronco;
    public Text Contador;
    public GameObject moneda2;

    private Vector3 offset;
    private float ValX, ValZ;
    private Vector3 DireccionActual;
    private int TotalMonedas = 0;

    void Start()
    {
        ValX = 0.0F;
        ValZ = 0.0f;
        offset = camara.transform.position;
        CreateSueloInicial();
        DireccionActual = Vector3.forward;

        
        Rigidbody rb = bola.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bola.AddComponent<Rigidbody>();
            rb.useGravity = false; 
        }
    }

    void CreateSueloInicial()
    {
        for (int i = 0; i < 3; i++)
        {
            ValZ += 6.0f;
            Instantiate(suelo, new Vector3(ValX, 0, ValZ), Quaternion.identity);
        }
    }

    void Update()
    {
        camara.transform.position = transform.position + offset;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CambiartDireccion();
        }
    }

    void FixedUpdate()
    {
        camara.transform.position = transform.position + offset;

        // Usa FixedUpdate para manejar la física del Rigidbody
        GetComponent<Rigidbody>().MovePosition(transform.position + DireccionActual * velocidad * Time.fixedDeltaTime);

        if (bola.transform.position.y < 0)
        {
            SceneManager.LoadScene("HasPerdido");
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Suelo")
        {
            Debug.Log("Rutina");
            StartCoroutine(BorrarSuelo(other.gameObject));
        }
    }

    IEnumerator BorrarSuelo(GameObject suelo)
    {
        float aleatorio = Random.Range(0.0f, 1.0f);
        if (aleatorio > 0.5f)
        {
            ValX -= 6.0f;
        }
        else
        {
            ValZ += 6.0f;
        }
        Instantiate(suelo, new Vector3(ValX, 0.0f, ValZ), Quaternion.identity);
        yield return new WaitForSeconds(2);
        suelo.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        suelo.gameObject.GetComponent<Rigidbody>().useGravity = true;
        yield return new WaitForSeconds(2);
        Destroy(suelo);

        float ran = Random.Range(0f, 1f);
        if (ran < 1f) // Cada suelo que se genera tiene un 100% de posibilidades de poseer una moneda
        {
            ran = Random.Range(-2f, 2f);
            Instantiate(moneda, new Vector3(ValX + ran, 1.5f, ValZ + aleatorio), Quaternion.identity);
        }

        float ran2 = Random.Range(0f, 1f);
        if (ran2 < 0.7f) // Cada suelo que se genera tiene un 70% de posibilidades de poseer un tronco
        {
            ran2 = Random.Range(-2f, 2f);
            tronco = Instantiate(tronco, new Vector3(ValX + ran2, 0.8f, ValZ + ran2), Quaternion.identity);
            tronco.transform.rotation = Quaternion.Euler(0.0f, Random.Range(0.0f, 90.0f), 90.0f);
        }

        float ran3 = Random.Range(0f, 1f);
        if (ran3 < 0.5f) // Cada suelo que se genera tiene un 50% de posibilidades de poseer una moneda
        {
            ran3 = Random.Range(-2f, 2f);
            Instantiate(moneda2, new Vector3(ValX + ran, 1.5f, ValZ + aleatorio), Quaternion.identity);
        }
    }

    void CambiartDireccion()
    {
        if (DireccionActual == Vector3.forward)
        {
            DireccionActual = Vector3.left;
        }
        else
        {
            DireccionActual = Vector3.forward;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Premio"))
        {
            TotalMonedas++;
            Contador.text = "Contador =" + TotalMonedas + "/10";
            Destroy(other.gameObject);
            if (TotalMonedas == 10)
            {
                SceneManager.LoadScene("HasGanado");
            }
        }
        if (other.gameObject.CompareTag("tronco"))
        {
            Debug.Log("He chocado con un tronco");
            SceneManager.LoadScene("HasPerdido");
        }
        if (other.gameObject.CompareTag("premio2"))
        {
            TotalMonedas += 2;
            Contador.text = "Contador =" + TotalMonedas + "/10";
            Destroy(other.gameObject);
            if (TotalMonedas >= 10)
            {
                SceneManager.LoadScene("HasGanado");
            }
        }
    }
}

