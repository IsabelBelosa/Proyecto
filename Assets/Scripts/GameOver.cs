using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button reinicio;
    public Button salir;
    // Start is called before the first frame update
    void Start()
    {
        reinicio = GameObject.FindWithTag("empezar").GetComponent<Button>();
        reinicio.onClick.AddListener(Gameover);
        salir = GameObject.FindWithTag("terminar").GetComponent<Button>();
        salir.onClick.AddListener(salida);
    }

    void Gameover()
    {
        SceneManager.LoadScene("SampleScene");

    }

    void salida()
    {
        Application.Quit();
        Debug.Log("salio del juego");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
