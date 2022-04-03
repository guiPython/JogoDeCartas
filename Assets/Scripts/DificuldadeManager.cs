using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Classe que gerencia a cena Dificuldades
 */
public class DificuldadeManager : MonoBehaviour
{
    /// <summary>
    /// Inicia o jogo na dificuldade facil 16 cartas - 8 duplas
    /// </summary>
    public void IniciaJogoNoFacil()
    {
        PlayerPrefs.SetString("dificuldade", "facil");
        SceneManager.LoadScene("Jogo");
    }

    /// <summary>
    /// Inicia o jogo na difilculdade normal 26 cartas - 13 duplas
    /// </summary>
    public void IniciaJogoNoMedio()
    {
        PlayerPrefs.SetString("dificuldade", "medio");
        SceneManager.LoadScene("Jogo");
    }

    /// <summary>
    /// Inicia o jogo na difculdade dificil 32 cartas - 16 duplas
    /// </summary>
    public void IniciaJogoNoDificil()
    {
        PlayerPrefs.SetString("dificuldade", "dificil");
        SceneManager.LoadScene("Jogo");
    }

    /// <summary>
    /// Volta para cena do Menu
    /// </summary>
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
