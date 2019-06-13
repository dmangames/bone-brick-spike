using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour
{
    public GameObject[] backgroundStructures;
    public Transform spawnLocation;


    private void OnEnable()
    {
        GameManager.Instance.gameStartEvent += StartSpawningBackground;
        GameManager.Instance.gameOverEvent += StopSpawningBackground;
    }

    private void OnDisable()
    {
        GameManager.Instance.gameStartEvent -= StartSpawningBackground;
        GameManager.Instance.gameOverEvent -= StopSpawningBackground;
    }

    private void Start()
    {
        if(GameManager.Instance == null)
        {
            Debug.Log("Help me");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawningBackground()
    {
        StartCoroutine(SpawnBackground());
    }

    public void StopSpawningBackground()
    {
        StopAllCoroutines();
    }


    IEnumerator SpawnBackground()
    {
        while (GameManager.Instance.isPlaying)
        {
            Instantiate(backgroundStructures[Random.Range(0, backgroundStructures.Length)], spawnLocation.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 15));
        }

    }
}
