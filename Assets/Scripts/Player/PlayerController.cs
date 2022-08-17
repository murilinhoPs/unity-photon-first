using System.Collections;
using Enemies;
using Photon.Pun;
using Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    [Header("Dash")] [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    private float initialSpeed, initialDashCooldown;

    private PhotonView _photonView;
    private PlayersHealth _health;
    private LineRenderer _goldenRay;

    private Rigidbody2D _rigid;
    private Vector2 moveInput;

    private void Awake()
    {
        _goldenRay = LevelManager.Instance.goldenRay;
        _health = FindObjectOfType<PlayersHealth>();
    }

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();

        initialSpeed = speed;
        initialDashCooldown = dashCooldown;
    }

    private void Update()
    {
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }

        if (_photonView.IsMine)
        {
            _goldenRay.SetPosition(0, transform.position);
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero && dashCooldown <= 0)
            {
                StartCoroutine(Dash());
            }
        }
        else
        {
            _goldenRay.SetPosition(1, transform.position);
        }
    }

    private void FixedUpdate()
    {
        var moveTo = _rigid.position + moveInput.normalized * (speed * Time.fixedDeltaTime);

        _rigid.MovePosition(moveTo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _health.OnDamageReceived(EnemyDifficulty.Instance.damage);
        }
    }

    private IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = initialSpeed;
        dashCooldown = initialDashCooldown;
    }
}