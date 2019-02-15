using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float angle = 1f;
    [SerializeField]
    private float accelerate = 0.1f;
    [SerializeField]
    private float angleDrag = 0.1f;

    [SerializeField]
    private string friendTag;
    [SerializeField]
    private Vector2 offset;
    [SerializeField]
    private Transform direction;
    [SerializeField]
    private GameObject playerSprite;

    private Rigidbody2D rigid;
    private Vector2 moveDirection;
    private GameObject tail;
    public GameObject GetTail()
    {
        return tail;
    }

    private List<GameObject> friends = new List<GameObject>();
    public int GetfriendsCount()
    {
        return friends.Count;
    }

    private bool isBlinking;

	// Use this for initialization
	void Start () {
        SoundManager.Instance.PlayBgm(BGM.Game);
        rigid = GetComponent<Rigidbody2D>();
        tail = this.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!CountDown.GetCanStart() || Timer.GetIsGameOver())
            return;
        Rotate();
    }

    private void FixedUpdate()
    {
        if (!CountDown.GetCanStart() || Timer.GetIsGameOver())
            return;
        Move();
    }

     //移動は自動
    private void Move()
    {
        moveDirection = direction.position - transform.position;
        rigid.velocity = moveDirection * speed;
    }

    //入力で回転
    private void Rotate()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input > 0)
            transform.Rotate(0, 0, -angle);
        if (input < 0)
            transform.Rotate(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!CountDown.GetCanStart() || Timer.GetIsGameOver())
            return;

        if (/*!isBlinking && */collision.gameObject.tag == "Enemy")
        {
            Join(collision.gameObject);
        }

        if(collision.gameObject.tag == "BlockObject")
        {
            SoundManager.Instance.PlaySe(SE.HitWall);
            Divide();
            StartCoroutine("Blink");
        }
    }

    //新しく列に加えたNPCの位置を列の後ろに設定する
    private Vector3 SetChildPosition()
    {
        Vector3 arrayDirection = Vector3.Normalize(tail.transform.position - direction.position);
        float arrayAngle = Mathf.Atan2(arrayDirection.y, arrayDirection.x);

        Vector3 childPosition = Vector3.zero;
        childPosition.x = tail.transform.position.x + offset.x * Mathf.Cos(arrayAngle);
        childPosition.y = tail.transform.position.y + offset.y * Mathf.Sin(arrayAngle);

        return childPosition;
    }

    private void Join(GameObject collision)
    {
        SoundManager.Instance.PlaySe(SE.GetEnemy);
        Transform collideObject = collision.transform;

        collideObject.parent = transform;
        collideObject.gameObject.tag = friendTag;
        collideObject.position = SetChildPosition();
        collideObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;

        collideObject.GetComponent<Rigidbody2D>().drag = 10;
        Destroy(collideObject.GetComponent<Collider2D>());

        HingeJoint2D joint = collideObject.gameObject.AddComponent<HingeJoint2D>();
        joint.connectedBody = transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>();

        DistanceJoint2D disJoint = collideObject.gameObject.AddComponent<DistanceJoint2D>();
        disJoint.connectedBody = tail.GetComponent<Rigidbody2D>();

        Destroy(collideObject.GetComponent<EnemyBase>());

        collideObject.parent = null;
        tail = collideObject.gameObject;
        speed += accelerate;
        angle -= angleDrag;

        friends.Add(collideObject.gameObject);
    }

    //ぶつかった時最後尾が分裂
    private void Divide()
    {
        if (isBlinking ||  friends.Count == 0)
            return;
        
        GameObject divideObj = tail;

        divideObj.tag = "Enemy";

        Vector3 spawnPos = divideObj.GetComponent<EnemyType>().GetStartPosition();
        //divideObj.transform.position = divideObj.GetComponent<EnemyType>().GetStartPosition();

        Destroy(divideObj.GetComponent<HingeJoint2D>());
        Destroy(divideObj.GetComponent<DistanceJoint2D>());

        //divideObj.GetComponentInChildren<SpriteRenderer>().color = divideObj.GetComponent<EnemyType>().GetEnemyColor();
        //Debug.Log(divideObj.GetComponent<EnemyType>().GetEnemyColor());

        //divideObj.AddComponent<EnemyType>().GetEnemyBase();

        //divideObj.GetComponent<Rigidbody2D>().drag = 0;

        //Instantiate(divideObj.GetComponent<EnemyType>().GetSelfPrefab(), spawnPos, Quaternion.identity);

        speed -= accelerate;
        angle += angleDrag;

        friends.Remove(friends[friends.Count - 1]);
        if (friends.Count != 0)
            tail = friends[friends.Count - 1];
        else
            tail = gameObject;

        Destroy(divideObj);
    }

    //無敵時間の点滅
    IEnumerator Blink()
    {
        if (isBlinking)
            yield break;

        float blinkTime = 0.3f;
        List<SpriteRenderer> renderer = new List<SpriteRenderer>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(friendTag))
        {
            renderer.Add(obj.GetComponentInChildren<SpriteRenderer>());
        }

        isBlinking = true;
        while ((blinkTime-=Time.deltaTime) > 0)
        {
            foreach (SpriteRenderer r in renderer)
            {
                if(r!=null)
                    r.enabled = !r.enabled;
            }
            yield return new WaitForSeconds(0.1f);
        }
        isBlinking = false;
        foreach (SpriteRenderer r in renderer)
        {
            if (r != null)
                r.enabled = true;
        }
    }
}
