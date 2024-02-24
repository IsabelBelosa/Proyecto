using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button boton;
    // Start is called before the first frame update
    void Start()
    {
        boton = GameObject.FindWithTag("empezar").GetComponent<Button>();
        boton.onClick.AddListener(Gameover);
    }

    void Gameover()
    {
        SceneManager.LoadScene("SampleScene");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
