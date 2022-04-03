using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool cartaRevelada; // Indica se a variavel esta virada ou não
    public Sprite frenteDaCarta; // Sprite da frente da carta
    public Sprite versoDaCarta; // Sprite do verso da carta
    
    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    /// <summary>
    /// Colocamos como Sprite do GameObject Prefab o verso da carta
    /// </summary>
    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = versoDaCarta;
        cartaRevelada = false;
    }

    /// <summary>
    /// Colocamos como Sprite do GameObject Prefab a frente da carta
    /// </summary>
    public void MostraCarta()
    {
        GetComponent<SpriteRenderer>().sprite = frenteDaCarta;
        cartaRevelada = true;
    }

    /// <summary>
    /// Configura a frente da carta para ser a sprite recebida
    /// </summary>
    /// <param name="novaCarta">Sprinte de frente da carta</param>
    public void SetFrenteCarta(Sprite novaCarta)
    {
        frenteDaCarta = novaCarta;
    }

    /// <summary>
    /// Configura o fundo da carta para ser a sprite recebida
    /// </summary>
    /// <param name="novaCarta">Sprite de fundo da carta</param>
    public void SetFundoDaCarta(Sprite novaCarta)
    {
        versoDaCarta = novaCarta;
    }

    /// <summary>
    /// Retorna o nome da Sprite do verso da carta
    /// </summary>
    /// <returns>Nome da Sprite do verso da carta</returns>
    public string GetNameOfVersoDaCarta()
    {
        return versoDaCarta.name;
    }

    /// <summary>
    ///    Ao clicarmos em uma carta esse evento é disparado e chamamos a função CartaSelecionada para que o gameManager consiga guardar a carta que selecionamos
    /// </summary>
    public void OnMouseDown()
    {
        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }
}
