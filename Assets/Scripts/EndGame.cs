using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Essa classe gerencia toda a cena do final do jogo.
/// Carrega as cenas de Menu, Cr�ditos ou Jogo dependendo do input do usu�rio.
/// </summary>
public class EndGame : MonoBehaviour
{
    /// <summary>
    /// Encontra o GameObject status.
    /// Injeta no componente Text o texto "ENFORCADO" na cor vermelha.
    /// </summary>
    private void SetStatusForLoser()
    {
        var statusObject = GameObject.Find("status");
        statusObject.GetComponent<Text>().color = Color.red;
        statusObject.GetComponent<Text>().text = "VOC� PERDEU ;(";
    }

    /// <summary>
    /// Encontra o GameObject status.
    /// Injeta no componente Text o texto "VENCEDOR" na cor verde.
    /// </summary>
    private void SetStatusForWinner()
    {
        var statusObject = GameObject.Find("status");
        statusObject.GetComponent<Text>().color = Color.green;
        statusObject.GetComponent<Text>().text = "VOC� GANHOU !!!!!!";
    }


    // Start is called before the first frame update
    void Start()
    {
        var nameOfScene = SceneManager.GetActiveScene().name;
        if (nameOfScene == "EndGameForLoser") SetStatusForLoser();
        else if (nameOfScene == "EndGameForWinner") SetStatusForWinner();
    }


    /// <summary>
    /// Carrega a cena do Jogo novamente.
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene("Jogo");
    }

    /// <summary>
    /// Carrega a cena do Menu novamente.
    /// </summary>
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    /// <summary>
    /// Carrega a cena de Cr�ditos.
    /// </summary>
    public void ExitGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
