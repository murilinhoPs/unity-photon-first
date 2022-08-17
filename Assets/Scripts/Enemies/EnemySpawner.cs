using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private GameObject enemy;
        [SerializeField] private float startTimeSpawns;

        private float timeBetweenSpawns;

        private void Start()
        {
            timeBetweenSpawns = startTimeSpawns;
        }

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
            {
                return;
            }
        
            if (timeBetweenSpawns > 0)
            {
                timeBetweenSpawns -= Time.deltaTime;
                return;
            }

            var spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBetweenSpawns = startTimeSpawns;
        }
    }
}
