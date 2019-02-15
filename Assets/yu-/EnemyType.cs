using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType : MonoBehaviour
{
    [SerializeField]
    private GameObject selfPrefab;
    public GameObject GetSelfPrefab()
    {
        return selfPrefab;
    }


    [SerializeField]
    private Color enemyColor;
    public Color GetEnemyColor()
    { return enemyColor; }

    private EnemyBase enemyBase;
    public EnemyBase GetEnemyBase()
    { return enemyBase; }

    private Vector3 startPosition;
    public Vector3 GetStartPosition()
    {
        return startPosition;
    }

    // Use this for initialization
    void Start()
    {
        enemyBase = GetComponent<EnemyBase>();
        startPosition = transform.position;
        GetComponentInChildren<SpriteRenderer>().color = enemyColor;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
