using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

/*
 * Enum que contem as cores de fundo das cartas
 */
public enum CorDeFundoDaCarta 
{
    VERMELHO,
    AZUL
}

/*
 * Enum que contem os naipes das cartas
 */
public enum NaipeDaCarta
{
    Paus,
    Copas,
    Espadas,
    Ouro
}

public class ManageCartas : MonoBehaviour
{
    // A carta a ser descartada
    public GameObject Carta; 

    // Indicadores de seleção para as duas cartas que o jogador pode selecionar por rodada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada;

    // Atributos que guardam as cartas selecionadas no jogo
    private GameObject primeiraCarta, segundaCarta;

    // Atributos que indicam o estado do timer
    private bool timerAcionado;
    
    // Atributo que guarda o valor do timer
    private float timer;

    // Atributo que guarda o numero de tentativas
    private int numeroDeTentativas = -1;

    // Atributo que guarda o numero de acertos do jogador
    private int numeroDeAcertos = 0;

    // Atributos com os audios de acerto e erro na seleção da dupla de cartas
    private AudioSource somAcerto, somErro;

    // Atributo que guarda o numero de jogadas do ultimo jogo
    private int ultimoJogo = 0;

    // Atributo que guarda os naipes e seus respectivos nome nos Sprites
    private Dictionary<NaipeDaCarta, string> naipes = new Dictionary<NaipeDaCarta, string>();

    // Atributo que guarda o numero de cartas em jogo
    private int numeroDeCartasNoJogo;

    // Atributo que guarda as cartas sorteada para o jogo
    private CartaDoJogo[] cartas;

    // Atributo que guarda a dficuldade do jogo
    private string dificuldade;

    // Atributo que guarda o numero maximo de tentativas para o jogo
    private int numeroMaximoDeTentativas;

    /*
     * Classe que define as cartas em jogo
    */
    public class CartaDoJogo
    {
        public NaipeDaCarta naipe;
        public int numeroDaCarta;

        public CartaDoJogo(NaipeDaCarta naipe, int numeroDaCarta)
        {
            this.naipe = naipe;
            this.numeroDaCarta = numeroDaCarta;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Pega de PlayerPrefs a escolha da dificuldade
        dificuldade = PlayerPrefs.GetString("dificuldade");

        // Analisa a dificuldade e configura o numero de cartas do jogo
        if (dificuldade == "facil") numeroDeCartasNoJogo = 8;
        if (dificuldade == "medio") numeroDeCartasNoJogo = 13;
        if (dificuldade == "dificil") numeroDeCartasNoJogo = 16;

        // Configura o numero maximo de tentativas
        numeroMaximoDeTentativas = numeroDeCartasNoJogo * 3;

        // Coloca na tela o numero maximo de tentativa permitidas para a dificuldade selecionada
        GameObject.Find("numMaximoTentativas").GetComponent<Text>().text = $"Numero Maximo de Tentativas: {numeroMaximoDeTentativas}";

        // Configuração do Dicionario de naipes para possibilitar busca pelo enum de NaipeDaCarta
        naipes.Add(NaipeDaCarta.Paus, "_of_clubs");
        naipes.Add(NaipeDaCarta.Copas, "_of_hearts");
        naipes.Add(NaipeDaCarta.Espadas, "_of_spades");
        naipes.Add(NaipeDaCarta.Ouro, "_of_diamonds");

        MostraCartas();
        UpdateNumeroDeTentativas();
        somAcerto = GameObject.Find("audioAcerto").GetComponent<AudioSource>();
        somErro = GameObject.Find("audioErro").GetComponent<AudioSource>();
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = $"Jogo Anterior: {ultimoJogo}";     
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se o timer foi acionado a cada frame
        if (timerAcionado)
        {
            // Coloca no timer a variação de tempo que passou desde seu acionamento (segundos)
            timer += Time.deltaTime;
            // Verifica se a variação é maior que 1 segundo
            if (timer > 1)
            {
                timerAcionado = false;
                // Verifica se o jogo acabou
                if(numeroDeTentativas == numeroMaximoDeTentativas)
                {
                    if (numeroDeAcertos == numeroDeCartasNoJogo)
                    {
                        // Coloca a na variavel Jogadas do escopo do unity o numero de tentativas
                        PlayerPrefs.SetInt("Jogadas", numeroDeTentativas);
                        ultimoJogo = numeroDeTentativas;

                        var recordeAtual = PlayerPrefs.GetInt($"recorde_{dificuldade}"); // Guarda o recorde atual para a dificuldade selecionada

                        /* Compara se o recorde é relacionado ao primeiro jogo ou se o jogador conseguiu terminar o jogo com menos tentativas
                         * Caso seja verdadeira a condição descrita acima atualizamos o recorde
                        */
                        if (recordeAtual > numeroDeTentativas || recordeAtual == 0) PlayerPrefs.SetInt($"recorde_{dificuldade}", numeroDeTentativas);

                        PlayerPrefs.Save(); // Salva as PlayersPrefs
                        SceneManager.LoadScene("EndGameForWinner");
                    }
                    SceneManager.LoadScene("EndGameForLoser");
                }
                // Verifica se a dupla escolhida pelo jogador é equivalente
                else if(primeiraCarta.tag == segundaCarta.tag)
                {
                    // Retira as duas cartas da cena destuindo os GameObjects
                    Destroy(primeiraCarta);
                    Destroy(segundaCarta);

                    // Toca o som de acerto
                    somAcerto.Play();

                    if (++numeroDeAcertos == numeroDeCartasNoJogo)
                    {
                        // Coloca a na variavel Jogadas do escopo do unity o numero de tentativas
                        PlayerPrefs.SetInt("Jogadas", numeroDeTentativas);
                        ultimoJogo = numeroDeTentativas;

                        var recordeAtual = PlayerPrefs.GetInt($"recorde_{dificuldade}"); // Guarda o recorde atual para a dificuldade selecionada

                        /* Compara se o recorde é relacionado ao primeiro jogo ou se o jogador conseguiu terminar o jogo com menos tentativas
                         * Caso seja verdadeira a condição descrita acima atualizamos o recorde
                        */
                        if (recordeAtual > numeroDeTentativas || recordeAtual == 0) PlayerPrefs.SetInt($"recorde_{dificuldade}", numeroDeTentativas);

                        PlayerPrefs.Save(); // Salva as PlayersPrefs
                        SceneManager.LoadScene("EndGameForWinner");
                    }
                }
                else
                {
                    // Esconde as duas cartas escolhidas pelo jogador
                    primeiraCarta.GetComponent<Tile>().EscondeCarta();
                    segundaCarta.GetComponent<Tile>().EscondeCarta();
                    
                    // Toca o som de Erro
                    somErro.Play();
                }

                // Reseta as variaveis de controle de tempo e seleção de cartas
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                primeiraCarta = null;
                segundaCarta = null;
                timer = 0;
            }
        }
    }

    /// <summary>
    /// Cria um array com os indices das cartas e o embaralha
    /// </summary>
    /// <returns>O array com os indices das cartas embaralhado</returns>
    private int[] CriaArrayEmbaralhado()
    {
        int[] indicesDasCartas = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        int temp; // Valor temporario para realizar o swap entre as posições
        for (int t = 0; t < indicesDasCartas.Length; t++)
        {
            temp = indicesDasCartas[t];
            int indiceAleatório = Random.Range(t, 13); // Gera o indice aleatório
            indicesDasCartas[t] = indicesDasCartas[indiceAleatório];
            indicesDasCartas[indiceAleatório] = temp;
        }
        return indicesDasCartas;
    }

    /// <summary>
    /// Pega um array de CartasDoJogo e devolve ele embaralhado
    /// </summary>
    /// <param name="cartasDoJogo">Array original de cartas</param>
    /// <returns>Array de cartas embaralhado</returns>
    private CartaDoJogo[] EmbaralhaCartasDoJogo(CartaDoJogo[] cartasDoJogo)
    {
        CartaDoJogo[] cartasDoJogoEmbaralhadas = cartasDoJogo;
        CartaDoJogo temp; // Valor temporario para realizar o swap entre as posições
        for (int t = 0; t < cartasDoJogo.Length; t++)
        {
            temp = cartasDoJogo[t];
            int indiceAleatório = Random.Range(t, numeroDeCartasNoJogo); // Gera o indice aleatório
            cartasDoJogo[t] = cartasDoJogo[indiceAleatório];
            cartasDoJogo[indiceAleatório] = temp;
        }
        return cartasDoJogoEmbaralhadas;
    }

    /// <summary>
    /// Cria todas as cartas do jogo com decisões aleatórias de naipe e posição de cada valor 
    /// </summary>
    /// <returns></returns>
    private CartaDoJogo[] CriaCartasDoJogo()
    {
        int[] indicesEmbaralhados = CriaArrayEmbaralhado();
        cartas = new CartaDoJogo[numeroDeCartasNoJogo];
        NaipeDaCarta naipe = (NaipeDaCarta)Random.Range(0, 4);
        int indiceAuxiliar = 0;
        for(int i = 0; i < numeroDeCartasNoJogo; i++)
        {
            if(i >= 12)
            {
                NaipeDaCarta newNaipe = (NaipeDaCarta)Random.Range(0, 4);
                while(newNaipe == naipe) newNaipe = (NaipeDaCarta)Random.Range(0, 4);
                cartas[i] = new CartaDoJogo(newNaipe, indicesEmbaralhados[indiceAuxiliar++]);
            }
            else
            {
                cartas[i] = new CartaDoJogo(naipe, indicesEmbaralhados[i]);
            }
        }
    
        return cartas;
    }

    /// <summary>
    /// Cria as cartas do game tanto com fundo Vermelho quanto com fundo Azul
    /// </summary>
    private void MostraCartas()
    { 
        CorDeFundoDaCarta cor = CorDeFundoDaCarta.VERMELHO;
        CriaCartasDoJogo();
        CartaDoJogo[] cartasDoJogo = cartas;
        for(int colecao = 0; colecao < 2; colecao++)
        {
            for(int rank = 0; rank < numeroDeCartasNoJogo; rank++)
            {
                var carta = cartas[rank];
                AddCarta(colecao, rank, carta.numeroDaCarta, cor, carta.naipe);
            }
            cor = CorDeFundoDaCarta.AZUL;
            cartasDoJogo = EmbaralhaCartasDoJogo(cartasDoJogo);
        }     
    }

    /// <summary>
    /// Cria uma carta com a sua posição na tela ajustada e coloca o sprite relacionado ao indice da carta como a frente da carta
    /// </summary>
    /// <param name="linha">Possibilita o ajuste da posição no eixo Y da carta e varia de 0-2</param>
    /// <param name="rank">Possibilita o ajuste de posição no eixo X da carta e varia de 0-13</param>
    /// <param name="valor">Refere-se ao valor da carta no baralho 0-13 (Ás-Rei)</param>
    /// <param name="corDeFundo">Refere-se a cor de fundo que nossa carta vai ter ao ser criada (Vermelho|Azul)</param>
    /// <param name="naipeDaCarta">Refere-se a naipe da carta varia entre (Copas|Espadas|Ouro|Paus)</param>
    private void AddCarta(int linha, int rank, int valor, CorDeFundoDaCarta corDeFundo, NaipeDaCarta naipeDaCarta)
    {
        // Pegamos o objeto centro da tela e ajustamos nossas cartas a partir dele
        GameObject centroDaTela = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = Carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (950 * escalaCartaOriginal) / 110.0f;

        // Criamos a posição para o GameObject que representa carta com os devidos ajustes
        Vector3 cartaPosicao = new Vector3(
            centroDaTela.transform.position.x + ((rank - numeroDeCartasNoJogo / 2) * fatorEscalaX),
            centroDaTela.transform.position.y + ((linha - 2 / 2) * fatorEscalaY), 
            centroDaTela.transform.position.z);

        // Criamos o GameObject da carta e configuramos seu nome e numero da carta As até King 
        GameObject c = (GameObject)Instantiate(Carta, cartaPosicao, Quaternion.identity);
        c.tag = $"{(valor)}";
        c.name = $"{linha}_{rank}";
        string nomeDaCarta;
        string numeroCarta;
        string naipe;

        // Ajusta valores para indices especiais
        if (valor == 0) numeroCarta = "ace";
        else if (valor == 10) numeroCarta = "jack";
        else if (valor == 11) numeroCarta = "queen";
        else if (valor == 12) numeroCarta = "king";
        else numeroCarta = $"{(valor + 1)}";

        naipe = naipes[naipeDaCarta]; 
        nomeDaCarta = numeroCarta + naipe;

        // Com o nome da carta configurado buscamos essa carta (Sprite) nos nossos recursos
        Sprite frenteDaCarta = (Sprite)Resources.Load<Sprite>(nomeDaCarta);

        /*
         * Na linha de código abaixo buscamos o GameObject carta pela sua tag
         * Colocamos a sprite encontrada acima como a frente desse GameObject
         */
        GameObject.Find($"{linha}_{rank}").GetComponent<Tile>().SetFrenteCarta(frenteDaCarta);


        string nomeDoFundoDaCarta;
        nomeDoFundoDaCarta = corDeFundo == CorDeFundoDaCarta.VERMELHO ? "playCardBackBlue" : "playCardBackRed";
        // Com a cor da carta recebida como argumento da função buscamos o (Sprite) nos nossos recursos
        Sprite fundoDaCarta = (Sprite)Resources.Load<Sprite>(nomeDoFundoDaCarta);

        /*
         * Na linha de código abaixo buscamos o GameObject carta pela sua tag
         * Colocamos a sprite encontrada acima como a frente desse GameObject
         */
        GameObject.Find($"{linha}_{rank}").GetComponent<Tile>().SetFundoDaCarta(fundoDaCarta);
    }

    /// <summary>
    /// Captura as colisões e configura os registradores de seleção de cartas
    /// caso existam duas cartas selecionadas chama o método para verificar a igualdade delas
    /// </summary>
    /// <param name="carta">Carta que usuario clicou</param>
    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            primeiraCartaSelecionada = true;
            primeiraCarta = carta;
            primeiraCarta.GetComponent<Tile>().MostraCarta();
        }
        else if(primeiraCarta && !segundaCartaSelecionada)
        {
            var primeiroVerso = primeiraCarta.GetComponent<Tile>().GetNameOfVersoDaCarta();
            var segundoVerso = carta.GetComponent<Tile>().GetNameOfVersoDaCarta();

            // Verifica se clicamos na mesma carta, caso não inicia a verificação
            if(primeiroVerso != segundoVerso)
            {
                segundaCartaSelecionada = true;
                segundaCarta = carta;
                segundaCarta.GetComponent<Tile>().MostraCarta();
                VerificaCartas();
            }   
        }
    }

    /// <summary>
    /// Chama a função dispara timer para criar um "Delay" na analise das cartas e chama a função para atualizar o numero de tentativas
    /// </summary>
    public void VerificaCartas()
    {
        DisparaTimer();
        UpdateNumeroDeTentativas();
    }

    /// <summary>
    /// Dispara o timer alterando o valor dos atributos indicadores do timer
    /// </summary>
    public void DisparaTimer()
    {
        timerAcionado = true;
    }

    /// <summary>
    /// Atualiza o numero de tentativas na tela buscando o GameObject e inserindo o texto com a contagem de tentativas atualizada
    /// </summary>
    public void UpdateNumeroDeTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = $"Tentativas: {++numeroDeTentativas}";
    }
}
