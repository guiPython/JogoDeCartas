using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Recordes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Variaveis que guardam os scores de cada dificuldade
        int scoreFacil, scoreMedio, scoreDificil;

        // Variaveis que guardam os Componentes Text dos GameObjects de cada dificuldade
        Text txtScoreFacil, txtScoreMedio, txtScoreDificil;

        /*
         * Buscamos a PlayerPrefs relacionada ao recorde na dificuldade facil
         * Buscamos o GameObject que representa o texto na interface grafica criada no unity para o recorde na dificuldade facil
         * Escrevemos o recorde como N/A se não tivermos nenhum jogador vencedor ou escrevemos o recorde presente no PlayerPrefs no Componente Text
        */
        scoreFacil = PlayerPrefs.GetInt("recorde_facil");
        txtScoreFacil = GameObject.Find("RecordeFacil").GetComponent<Text>();
        txtScoreFacil.text = scoreFacil == 0 ? "Numero de Tentativas(Facil) N/A" : $"Numero de Tentativas (Facil) : {scoreFacil}";

        /*
         * Buscamos a PlayerPrefs relacionada ao recorde na dificuldade medio
         * Buscamos o GameObject que representa o texto na interface grafica criada no unity para o recorde na dificuldade medio
         * Escrevemos o recorde como N/A se não tivermos nenhum jogador vencedor ou escrevemos o recorde presente no PlayerPrefs no Componente Text
        */
        scoreMedio = PlayerPrefs.GetInt("recorde_medio");
        txtScoreMedio = GameObject.Find("RecordeMedio").GetComponent<Text>();
        txtScoreMedio.text = scoreMedio == 0 ? "Numero de Tentativas(Medio) N/A" : $"Numero de Tentativas (Médio) : {scoreMedio}";

        /*
         * Buscamos a PlayerPrefs relacionada ao recorde na dificuldade dificil
         * Buscamos o GameObject que representa o texto na interface grafica criada no unity para o recorde na dificuldade dificil
         * Escrevemos o recorde como N/A se não tivermos nenhum jogador vencedor ou escrevemos o recorde presente no PlayerPrefs no Componente Text
        */
        scoreDificil = PlayerPrefs.GetInt("recorde_dificil");
        txtScoreDificil = GameObject.Find("RecordeDificil").GetComponent<Text>();
        txtScoreDificil.text = scoreDificil == 0 ? "Numero de Tentativas(Dificil) N/A" : $"Numero de Tentativas (Dificil) : {scoreDificil}";
    }

    /// <summary>
    /// Método que reseta todos os recordes e carreda a cena de Recordes novamente
    /// </summary>
    public void ResetRecordes()
    {
        PlayerPrefs.SetInt("recorde_facil", 0);
        PlayerPrefs.SetInt("recorde_medio", 0);
        PlayerPrefs.SetInt("recorde_dificil", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Método que carrega a cena do Menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
