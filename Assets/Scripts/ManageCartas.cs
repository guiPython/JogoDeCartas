using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta; // A carta a ser descartada

    // Start is called before the first frame update
    void Start()
    {
        MostraCartas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MostraCartas()
    {
        //Instantiate(carta, new Vector3(0,0,0), Quaternion.identity);

        for(int rank = 0; rank < 13; rank++)
        {
            AddCarta(rank);
        }
    }

    private void AddCarta(int rank)
    {
        GameObject centroDaTela = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 100.0f;
        Vector3 cartaPosicao = new Vector3(
            centroDaTela.transform.position.x + ((rank - 13 / 2) * fatorEscalaX),
            centroDaTela.transform.position.y, 
            centroDaTela.transform.position.z);
        GameObject c = (GameObject)Instantiate(carta, cartaPosicao, Quaternion.identity);
        c.tag = "" + rank;
        c.name = "" + rank;
        string nomeDaCarta = "";
        string numeroCarta = "";

        if (rank == 0) numeroCarta = "ace";
        else if (rank == 10) numeroCarta = "jack";
        else if (rank == 11) numeroCarta = "queen";
        else if (rank == 12) numeroCarta = "king";
        else numeroCarta = $"{rank + 1}";

        nomeDaCarta = numeroCarta + "_of_clubs";
        Sprite s1 = (Sprite)Resources.Load<Sprite>(nomeDaCarta);
        print("S1:" + s1);
        GameObject.Find($"{rank}").GetComponent<Tile>().SetFrenteCarta(s1);
    }
}
