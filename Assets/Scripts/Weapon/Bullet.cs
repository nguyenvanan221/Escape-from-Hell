using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0f, 5f)]
    public float speed;
    private float direction;
    protected bool hit;
    [SerializeField] protected AudioClip explosionAudio;
    protected Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (hit) { return; }
        Move();
    }

    private void Move()
    {
        
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        hit = false;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
   
    private void Deactivate()
    {
        Destroy(gameObject);
    }
}
