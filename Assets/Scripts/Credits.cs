using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <para>
/// Essa classe gerencia a cena de cr�ditos que s� tem a fun��o de fechar o game.
/// </para>
/// </summary>
public class Credits : MonoBehaviour
{

    /// <summary>
    /// M�todo para fechar o Jogo
    /// </summary>
    public void ExitGame()
    {
      Application.Quit();
    }

    /// <summary>
    /// M�todo que carrega a cena do Menu
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
