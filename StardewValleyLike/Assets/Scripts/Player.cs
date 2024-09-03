using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed = 3;

    private Animator anim;
    private Vector2 direction = Vector2.zero;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        bool isWalk = direction.magnitude > 0;
        anim.SetBool("IsWalking", isWalk);

        if (isWalk)
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }

    }

    private void FixedUpdate()
    {
        // 位置变化在 FixedUpdate 中更新，保持与物理引擎一致，否则会出现抖动
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new(x, y);

        transform.Translate(Time.deltaTime * speed * direction.normalized);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Pickable"))
        {
            var itemType = collider.GetComponent<Pickable>().type;
            InventoryManager.Instance.AddToBackpack(itemType);
            Destroy(collider.gameObject);
        }
    }
}
