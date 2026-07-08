using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inu : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Vector3 moveVec;
    [SerializeField] DirSelectUI selectUI;
    [SerializeField] MasterData_Player data;
    [SerializeField] Present present;
    [SerializeField] bool autRun;

    Animator animator;
    SpriteRenderer sprite;
    Grid grid;

    enum Mode
    {
        Main,
        Success,
        Miss,
        Obore
    }
    Mode mode = Mode.Main;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        if (autRun) animator.SetBool("Run", true);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (moveVec * Time.fixedDeltaTime));
    }

    public void OnPointerDown()
    {
        if (mode != Mode.Main) return;
        var ui = Instantiate(selectUI);
        ui.gameObject.transform.position = transform.position;
        ui.Set(this);

        SoundManager.PlaySound(CommonData.SoundId.DogTouch);
    }

    public void SetMoveVec(Vector3 moveVec)
    {
        this.moveVec = moveVec * data.moveSpeed;
        if(moveVec.x > 0) sprite.flipX = false;
        if(moveVec.x < 0) sprite.flipX = true;
        if(moveVec == Vector3.zero)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            Debug.Log("inu obore");
            GameSceneController.instance.Miss();
            Obore();
            present.Miss(this);
        }
    }

    public void Success()
    {
        mode = Mode.Success;
        moveVec = Vector3.zero;
        animator.SetTrigger("Fan");
    }

    public void Obore()
    {
        mode = Mode.Obore;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        Vector3 newPos = sprite.transform.position;

        if(newPos.y % 1 > 0.08f && newPos.y % 1 < 0.4f)
        {
            float beforY = newPos.y;
            newPos.y = Mathf.Round(newPos.y - 0.91f) + 0.5f;
            Debug.Log(beforY + "," + (beforY - 0.91f)  + " -> " + newPos.y);
        }
        else
        {
            Debug.Log(newPos.y);
        }

        sprite.transform.position = newPos;

        animator.SetTrigger("Obore");
    }

    public void Miss()
    {
        if (mode == Mode.Obore) return;
        mode = Mode.Miss;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("Sad");
    }

    public void Reverse()
    {
        SetMoveVec(moveVec * -1);
    }
}
