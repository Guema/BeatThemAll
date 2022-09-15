using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] Canvas _canva;
    public List<GameObject> _enemies = new List<GameObject>();
    

    private void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
    }
    

    private void Update()
    {
        
        StartCoroutine(CountEnemy());

    }

    IEnumerator CountEnemy()
    {
        yield return new WaitForSeconds(1);
        _enemies.RemoveAll(go => go == null || !go.activeInHierarchy);
        if (_enemies.Count == 0)
        {
            _canva.gameObject.SetActive(true);
        }
        
    }
}
