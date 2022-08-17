using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject waitText;
    
    private void Start()
    {
        scoreText.text = LevelManager.Instance.GetPoints.ToString();

        if (PhotonNetwork.IsMasterClient) return;

        restartButton.SetActive(false);
        waitText.SetActive(true);
    }
}