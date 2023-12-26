using DG.Tweening;
using Enemies;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PointAndRange
{
    public Transform attackPoint;
    public float attackRange;
}

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject _healtPanel;
    [SerializeField] private Image _endPanel;

    [SerializeField] private PointAndRange[] _pointsAndRanges;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private int _attackDamage = 25;

    private Enemy _selfEnemy;
    private Animator _animator;
    private Transform _player;
    private bool _isFlipped = false;
    public static string[] _attackAnims = { "Attack1", "Attack2", "Attack3" };

    private void Awake()
    {
        _selfEnemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        _selfEnemy.GetHealthSystem().OnDead -= _selfEnemy.HealthSystem_OnDead;
        _selfEnemy.GetHealthSystem().OnDead += HealthSystem_OnDead;
    }

    private void OnDrawGizmos()
    {
        if (_pointsAndRanges.Length == 0)
            return;

        for (int i = 0; i < _pointsAndRanges.Length; i++)
        {
            Gizmos.DrawWireSphere(_pointsAndRanges[i].attackPoint.position, _pointsAndRanges[i].attackRange);
        }
    }

    public void StartFight()
    {
        PlayerController.isStop = false;
        _animator.SetTrigger("Start");
        _healtPanel.SetActive(true);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if (transform.position.x > _player.position.x && _isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < _player.position.x && !_isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }

    public void Attack(int index)
    {
        Collider2D colInfo = Physics2D.OverlapCircle(_pointsAndRanges[index].attackPoint.position, _pointsAndRanges[index].attackRange, _playerLayer);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(_attackDamage);
        }
    }

    public void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        Die();
    }

    private void Die()
    {
        if (_animator != null)
            _animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        _endPanel.DOFade(1f, 3f);
        StartCoroutine(LoadNext());
        enabled = false;
    }

    private IEnumerator LoadNext()
    {
        yield return new WaitForSeconds(3.2f);
        SceneChanger.LoadNextScene();
    }
}
