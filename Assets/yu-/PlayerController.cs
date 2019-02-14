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

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        tail = this.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Rotate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //移動は自動
        moveDirection = direction.position - transform.position;
        rigid.velocity = moveDirection * speed;
    }

    private void Rotate()
    {
        //入力で回転
        float input = Input.GetAxisRaw("Horizontal");
        if (input > 0)
            transform.Rotate(0, 0, -angle);
        if (input < 0)
            transform.Rotate(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Transform collideObject = collision.transform;
            collideObject.parent = transform;
            collideObject.gameObject.tag = friendTag;
            collideObject.position = SetChildPosition();

            collideObject.gameObject.GetComponent<HingeJoint2D>().connectedBody =
                transform.GetChild(transform.childCount - 1).GetComponent<Rigidbody2D>();

            collideObject.gameObject.GetComponent<DistanceJoint2D>().connectedBody = 
                tail.GetComponent<Rigidbody2D>();

            collideObject.parent = null;
            tail = collideObject.gameObject;
            speed += accelerate;
            angle -= angleDrag;
            
        }
    }

    private Vector3 SetChildPosition()
    {
        //新しく列に加えたNPCの位置を列の後ろに設定する

        Vector3 arrayDirection = Vector3.Normalize(tail.transform.position - direction.position);
        float arrayAngle = Mathf.Atan2(arrayDirection.y, arrayDirection.x);

        Vector3 childPosition = Vector3.zero;
        childPosition.x = tail.transform.position.x + offset.x * Mathf.Cos(arrayAngle);
        childPosition.y = tail.transform.position.y + offset.y * Mathf.Sin(arrayAngle);

        return childPosition;
    }
}
