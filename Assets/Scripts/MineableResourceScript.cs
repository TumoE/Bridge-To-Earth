using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineableResourceScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _resources;

    [SerializeField] private GameObject _effect;
    [SerializeField] private GameObject _mainModel;
    [SerializeField] private GameObject _destroyedModel;

    [SerializeField] private Collider _collider;

    [SerializeField] private float _destroyTime = 0.2f;
    [SerializeField] private float _respawnTime = 5f;

    [SerializeField] private bool _isOneTime;

    private PlayerVision _playerVision;

    private float _health;
   
    private void Start()
    {
        ScriptInit();
    }

    public void TakeDamage()
    {
        _health--;

        if (_health < 0)
        {
            _playerVision.UnPickObject();

            StartCoroutine(nameof(Destroy));
        }
    }

    private void SpawnResources()
    {
        for (int i = 0; i < _resources.Length; i++)
        {
            Instantiate(_resources[i], transform.position + (Vector3.up / 0.5f) * i + Vector3.right * Random.Range(-0.2f,0.2f), Quaternion.identity);
        }
    }

    private IEnumerator Destroy()
    {
        _collider.enabled = false;

        yield return new WaitForSeconds(_destroyTime);

        Values.CanMove = true;

        _mainModel.SetActive(false);
        _destroyedModel.SetActive(true);

        SpawnDestroyedEffects();
        SpawnResources();

        if (_isOneTime) StopCoroutine(nameof(Destroy));

        yield return new WaitForSeconds(_respawnTime);

        Respawn();
    }
    private void SpawnDestroyedEffects()
    {
        Instantiate(_effect,transform.position, transform.rotation);
    }
    private void Respawn()
    {
        _mainModel.SetActive(true);
        _collider.enabled = true;
        _destroyedModel.SetActive(false);
    }

    private void ScriptInit()
    {
        _playerVision = PlayerVision.Script;
    }


}
