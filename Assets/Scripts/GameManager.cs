
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance {get; private set;}

  public float initialGameSpeed = 5f;
  public float gameSpeedIncrease = 0.1f;

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
  private void NewGame() {

    Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

    foreach (var obstacle in obstacles) {
      Destroy(obstacle.gameObject);
      
    }
    gameSpeed = initialGameSpeed;
    enabled = true;
    
    player.gameObject.SetActive(true);
    spawner.gameObject.SetActive(true);
  }

  public void GameOver() {
    gameSpeed = 0f;
    enabled = false;

    player.gameObject.SetActive(false);
    spawner.gameObject.SetActive(false);
  }
  private void Update() {

    gameSpeed += gameSpeedIncrease * Time.deltaTime;

  }
}
