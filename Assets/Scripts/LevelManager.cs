using Enemies;
using Photon.Pun;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int points;
    [SerializeField] private Text score;
    [SerializeField] private GameObject gameOver;

    private PhotonView _view;
    private PlayersHealth _health;
    public LineRenderer goldenRay;
    private bool hasTwoPlayers;

    protected override void Awake()
    {
        base.Awake();
        
        _view = GetComponent<PhotonView>();
        _health = FindObjectOfType<PlayersHealth>();
    }

    private void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 2) return;

        if (hasTwoPlayers) return;
        goldenRay.gameObject.SetActive(true);
        hasTwoPlayers = true;
    }

    private void GameOver()
    {
        gameOver.SetActive(true);
        FindObjectOfType<EnemySpawner>().gameObject.SetActive(false);
        foreach (var player in FindObjectsOfType<PlayerController>())
        {
            player.gameObject.SetActive(false);
        }
    }

    public void AddScore()
    {
        _view.RPC("AddScoreRPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    private void AddScoreRPC()
    {
        points++;
        score.text = points.ToString();
    }

    public void OnRestart()
    {
        _view.RPC("RestartGame", RpcTarget.All, "Game");
    }

    [PunRPC]
    private void RestartGame(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    
    private void OnEnable()
    {
        _health.onGameOver += GameOver;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _health.onGameOver -= GameOver;
    }

    public int GetPoints => points;
}