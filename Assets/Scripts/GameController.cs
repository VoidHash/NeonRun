using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    [Header("Game Props")]
    [SerializeField]
    private float timeBetweenSpawns = 1.75f;
    [SerializeField]
    private float playerScoreHelper;
    [SerializeField]
    [Range(20, 100)]
    private int gameSpeedHelper = 50;

    private float elapsedTime = 0.0f;
    private float totalTime = 0.0f;
    public static int playerScore = 0;
    public static float gameSpeed = 50f;

    [Header("Game HUD")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;

    [Header("Game Enviroment")]
    public GameObject player;
    public GameObject playerIA;
    public GameObject road;

    [Header("Game Enemies")]
    public GameObject enemy01;
    public GameObject enemy02;
    public GameObject enemy03;
    public GameObject enemy04;

    [Header("Game Itens")]
    public GameObject coin;
    public GameObject powerUp;

    //Enviroment Stuffs
    private Transform spawnPoint01;
    private Transform spawnPoint02;
    private Transform spawnPoint03;

    private Transform deadZone01;
    private Transform deadZone02;
    private Transform deadZone03;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBehaviour.isAlive = true;

        spawnPoint01 = GameObject.Find("SpawnPoint01").GetComponent<Transform>();
        spawnPoint02 = GameObject.Find("SpawnPoint02").GetComponent<Transform>();
        spawnPoint03 = GameObject.Find("SpawnPoint03").GetComponent<Transform>();

        deadZone01 = GameObject.Find("DeadZone01").GetComponent<Transform>();
        deadZone02 = GameObject.Find("DeadZone02").GetComponent<Transform>();
        deadZone03 = GameObject.Find("DeadZone03").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehaviour.isAlive)
        {
            totalTime = +Time.time;
            time.text = "Time: " + Mathf.RoundToInt(totalTime) + "s";
            score.text = "Score: " + playerScore;
            elapsedTime += Time.deltaTime;

            if (totalTime % 5 == 0)
            {
                gameSpeed += 3;
            }

            if (totalTime % 10 == 0)
            {
                timeBetweenSpawns -= 0.3f;
            }
        }
        else
        {
            gameOverPanel.SetActive(true);
            TextMeshProUGUI text = GameObject.Find("GameOverScore").GetComponent <TextMeshProUGUI>();
            text.text = "You did\n" + playerScore + " points\n" + "in " + Mathf.RoundToInt(totalTime) + " seconds";
        }

        if (elapsedTime > timeBetweenSpawns)
        {
            elapsedTime = 0;
            NeonRunRNG(0.0f);
        }
    }

    private void InstantiateNewObject(GameObject go, Transform position, Transform destination, float speed)
    {
        if(go.tag == "Enemy")
        {
            GameObject newObject = Instantiate(go, position.position, Quaternion.Euler(270, 0, 0)) as GameObject;
            EnemyBehaviour enemyBehaviour = newObject.GetComponent<EnemyBehaviour>();
            if (enemyBehaviour != null)
            {
                enemyBehaviour.deadZone = destination;
                EnemyBehaviour.enemySpeed = speed;
            }
        }else if (go.tag == "PowerUp")
        {
            GameObject newObject = Instantiate(go, position.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            PowerUpBehaviour powerUpBehaviour = newObject.GetComponent<PowerUpBehaviour>();
            if(powerUpBehaviour != null)
            {
                powerUpBehaviour.deadZone = destination;
                PowerUpBehaviour.speed = speed;
            }
        }
        else if (go.tag == "Coin")
        {
            GameObject newObject = Instantiate(go, position.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            CoinBehaviour coinBehaviour = newObject.GetComponent<CoinBehaviour>();
            if (coinBehaviour != null)
            {
                coinBehaviour.deadZone = destination;
                CoinBehaviour.coinSpeed = speed;
            }
        }

    }

    private void NeonRunRNG(float seed)
    {
        spawnEnemies();
        spawnCoins();
        spawnPowerUp();
    }

    private void spawnEnemies()
    {
        int enemyNumber = Random.Range(1, 5);
        GameObject enemyType = null;

        switch (enemyNumber)
        {
            case 1:
                enemyType = enemy01;
                break;
            case 2:
                enemyType = enemy02;
                break;
            case 3:
                enemyType = enemy03;
                break;
            case 4:
                enemyType = enemy04;
                break;
            default: break;
        }

        int spawnNumber = Random.Range(1, 4);
        Transform spawnPoint = null;
        Transform enemyDestination = null;

        switch (spawnNumber)
        {
            case 1:
                spawnPoint = spawnPoint01;
                enemyDestination = deadZone01;
                break;
            case 2:
                spawnPoint = spawnPoint02;
                enemyDestination = deadZone02;
                break;
            case 3:
                spawnPoint = spawnPoint03;
                enemyDestination = deadZone03;
                break;
            default: break;
        }



        if (enemyType != null && spawnPoint != null && enemyDestination != null)
        {
            InstantiateNewObject(enemyType, spawnPoint, enemyDestination, gameSpeed);
        }
    }

    private void spawnPowerUp()
    {
        int propability = Random.Range(1, 6);

        int spawnNumber = Random.Range(1, 4);
        Transform spawnPoint = null;
        Transform distination = null;

        switch (spawnNumber)
        {
            case 1:
                spawnPoint = spawnPoint01;
                distination = deadZone01;
                break;
            case 2:
                spawnPoint = spawnPoint02;
                distination = deadZone02;
                break;
            case 3:
                spawnPoint = spawnPoint03;
                distination = deadZone03;
                break;
            default: break;
        }

        if (propability == 1 && spawnPoint != null && distination != null)
        {
            InstantiateNewObject(powerUp, spawnPoint, distination, gameSpeed);
        }
    }

    private void spawnCoins()
    {
        int propability = Random.Range(1,3);

        int spawnNumber = Random.Range(1, 4);
        Transform spawnPoint = null;
        Transform distination = null;

        switch (spawnNumber)
        {
            case 1:
                spawnPoint = spawnPoint01;
                distination = deadZone01;
                break;
            case 2:
                spawnPoint = spawnPoint02;
                distination = deadZone02;
                break;
            case 3:
                spawnPoint = spawnPoint03;
                distination = deadZone03;
                break;
            default: break;
        }

        if (propability == 1 && spawnPoint != null && distination != null)
        {
            InstantiateNewObject(coin, spawnPoint, distination, gameSpeed);
        }
    }

    public void restartGame()
    {
        PlayerBehaviour.isAlive = true;
        totalTime = 0.0f;
        playerScore = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void switchToIA()
    {
        if(playerIA.active == false)
        {
            playerIA.SetActive(true);
            player.SetActive(false);
        }
        else
        {
            playerIA.SetActive(false);
            player.SetActive(true);
        }
    }

    public void OnAfterDeserialize()
    {
        gameSpeed = gameSpeedHelper;
    }
}
