using Photon.Pun;
using UnityEngine;

namespace Enemies
{
    public class EnemySimple : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject explosionFx;

        private PlayerController[] _players;
        private PlayerController _nearestPlayer;

        private Rigidbody2D _rigid;
        private Vector2 moveInput;

        private PhotonView _view;

        private void Start()
        {
            speed = EnemyDifficulty.Instance.speed;

            _rigid = GetComponent<Rigidbody2D>();
            _view = GetComponent<PhotonView>();
            _players = FindObjectsOfType<PlayerController>();
        }

        private void Update()
        {
            var distanceP1 = Vector2.Distance(transform.position, _players[0].transform.position);
            var distanceP2 = Vector2.Distance(transform.position, _players[1].transform.position);

            _nearestPlayer = distanceP1 < distanceP2 ? _players[0] : _players[1];
        }

        private void FixedUpdate()
        {
            if (!_nearestPlayer) return;

            var playerDirection = _nearestPlayer.transform.position - transform.position;
            playerDirection.Normalize();
            _rigid.velocity = playerDirection * speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            
            EnemyDifficulty.Instance.speed += EnemyDifficulty.Instance.speedMultiplier;
            EnemyDifficulty.Instance.damage += EnemyDifficulty.Instance.damageMultiplier;

            if (other.CompareTag("Player"))
            {
                _view.RPC("SpawnParticle", RpcTarget.AllBuffered);
                PhotonNetwork.Destroy(this.gameObject);
            }

            if (other.CompareTag("Ray"))
            {
                LevelManager.Instance.AddScore();
                _view.RPC("SpawnParticle", RpcTarget.AllBuffered);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }

        [PunRPC]
        private void SpawnParticle()
        {
            if (gameObject)
                Instantiate(explosionFx, transform.position, Quaternion.identity);
        }
    }
}