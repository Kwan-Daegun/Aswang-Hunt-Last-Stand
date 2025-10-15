using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 target;

    private Rigidbody2D rb;

    private HP house;

    public int enemyDmg;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        house = GameObject.Find("House").GetComponent<HP>();
    }

    public void SetTarget(Vector2 targetPos)
    {
        target = targetPos;
    }

    void Update()
    {

        Vector2 direction = ((Vector2)transform.position - target).x > 0 ? Vector2.left : Vector2.right;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("House"))
        {
            Debug.Log("Enemy reached the house!");
            Destroy(gameObject);        
        }        
    }
}
