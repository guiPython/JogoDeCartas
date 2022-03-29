using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool cartaRevelada = false; // Indica se a variavel esta virada ou não
    public Sprite frenteDaCarta; // Sprite da frente da carta
    public Sprite versoDaCarta; // Sprite do verso da carta
    
    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = versoDaCarta;
        cartaRevelada = false;
    }

    public void MostraCarta()
    {
        GetComponent<SpriteRenderer>().sprite = frenteDaCarta;
        cartaRevelada = true;
    }

    public void SetFrenteCarta(Sprite novaCarta)
    {
        frenteDaCarta = novaCarta;
    }
    public void OnMouseDown()
    {
        print("Carta pressionada");
        if(cartaRevelada) EscondeCarta();
        else MostraCarta();
    }
}
