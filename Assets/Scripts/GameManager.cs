
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance {get; private set;}

  public float initialGameSpeed = 5f;
  public float gameSpeedIncrease = 0.1f;

  public TextMeshProUGUI gameOverText;
  public Button retryButton;

  public TextMeshProUGUI scoreText;
  public TextMeshProUGUI hiScoreText;
  
  private float score;


  public float gameSpeed {get; private set;}

  private Player player;
  private Spawner spawner;

  private void Awake() {
    if (Instance == null) {
        Instance = this;
    } else {
        DestroyImmediate(gameObject);
    }
  }

  private void onDestroy() {
    if (Instance == this) {
        Instance = null;
    }
  }

  private void Start() {
    
    player = FindObjectOfType<Player>();
    spawner = FindObjectOfType<Spawner>();

    NewGame();
  }
  public void NewGame() {

    Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

    foreach (var obstacle in obstacles) {
      Destroy(obstacle.gameObject);
      
    }
    gameSpeed = initialGameSpeed;
    enabled = true;
    score = 0;
    
    player.gameObject.SetActive(true);
    spawner.gameObject.SetActive(true);
    gameOverText.gameObject.SetActive(false);
    retryButton.gameObject.SetActive(false);

  }

  public void GameOver() {
    gameSpeed = 0f;
    enabled = false;

    player.gameObject.SetActive(false);
    spawner.gameObject.SetActive(false);

    gameOverText.gameObject.SetActive(true);
    retryButton.gameObject.SetActive(true);

    UpdateHiScore();
  }
  private void Update() {

    gameSpeed += gameSpeedIncrease * Time.deltaTime;

    score += gameSpeed * Time.deltaTime;
    scoreText.text = Mathf.FloorToInt(score).ToString();

  }

  private void UpdateHiScore() {
    float hiscore = PlayerPrefs.GetFloat("hiscore" , 0);

    if (score > hiscore) {
      hiscore = score;
      PlayerPrefs.SetFloat("hiscore" , hiscore);
    }

    hiScoreText.text = Mathf.FloorToInt(hiscore).ToString();
  }
}
