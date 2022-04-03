using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Essa classe gerencia toda a cena do final do jogo.
/// Carrega as cenas de Menu, Créditos ou Jogo dependendo do input do usuário.
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
        statusObject.GetComponent<Text>().text = "VOCÊ PERDEU ;(";
    }

    /// <summary>
    /// Encontra o GameObject status.
    /// Injeta no componente Text o texto "VENCEDOR" na cor verde.
    /// </summary>
    private void SetStatusForWinner()
    {
        var statusObject = GameObject.Find("status");
        statusObject.GetComponent<Text>().color = Color.green;
        statusObject.GetComponent<Text>().text = "VOCÊ GANHOU !!!!!!";
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
    /// Carrega a cena de Créditos.
    /// </summary>
    public void ExitGame()
    {
        SceneManager.LoadScene("Credits");
    }
}
