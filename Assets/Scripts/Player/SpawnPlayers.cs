using Photon.Pun;
using UnityEngine;
using Utils;

namespace Player
{
    public class SpawnPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject[] players;

        private void Start()
        {
            var randomPos = new Vector2(LevelBounds.RandomPosX, LevelBounds.RandomPosY);

            PhotonNetwork.Instantiate(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? players[0].name : players[1].name,
                randomPos, Quaternion.identity);
        }
    }
}