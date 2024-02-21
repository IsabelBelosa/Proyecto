using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class CuentaAtras : MonoBehaviour
{
    //PUBLICA
    public Button boton;
    public UnityEngine.UI.Image imagen;
    public Sprite[] numeros;
    // Start is called before the first frame update
    void Start()
    {
        //boton = GameObject.FindAnyObjectByType<Button>();
        boton = GameObject.FindWithTag("empezar").GetComponent<Button>();
        boton.onClick.AddListener(Empezar);
    }

    // Update is called once per frame
    void Empezar()
    {
        imagen.gameObject.SetActive(true);
        boton.gameObject.SetActive(false);

        StartCoroutine(AtrasCuenta());
    }


    IEnumerator AtrasCuenta(){

        for(int i = 0; i<numeros.Length; i++){
            imagen.sprite = numeros[i];
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Nivel1");
    }

      void Update()
    {

    }



}
