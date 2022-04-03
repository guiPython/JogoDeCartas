using UnityEngine;
using UnityEngine.SceneManagement;


/*
* Essa classe gerencia a cena de menu que s� pode carregar a cena do jogo;
*/
public class MenuManager : MonoBehaviour
{
    /// <summary>
    /// Carrega a cena de Dificuldades
    /// </summary>
    public void StartJogoDaMemoria()
    {
        SceneManager.LoadScene("Dificuldades"); 
    }

    /// <summary>
    /// Carrega a cena de Recordes
    /// </summary>
    public void GoToRecordes()
    {
        SceneManager.LoadScene("Recordes");
    }

    /// <summary>
    /// Carrega a cena de Cr�ditos
    /// </summary>
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
