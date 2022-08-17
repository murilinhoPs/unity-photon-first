using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayersHealth : MonoBehaviour
    {
        [SerializeField] private float healthValue;
        [SerializeField] private float maxHealth;
        [SerializeField] private Slider healthSlider;

        public delegate void OnGameOver();
        public event OnGameOver onGameOver;
    
        private PhotonView _view;

        private void Start()
        {
            _view = GetComponent<PhotonView>();
        
            healthSlider.maxValue = maxHealth;
            healthSlider.value = healthValue;
        }

        public void OnDamageReceived(float amount)
        {

            _view.RPC("OnDamageReceivedRPC", RpcTarget.AllBuffered, amount);
        }

        [PunRPC]
        private void OnDamageReceivedRPC(float amount)
        {
            healthValue -= amount;

            if (healthValue <= 0.1)
            {
                healthSlider.fillRect.gameObject.SetActive(false);
                onGameOver?.Invoke();
                return;
            }

            healthSlider.value = healthValue;
        }
    }
}