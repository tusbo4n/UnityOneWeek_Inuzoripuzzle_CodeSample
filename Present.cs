using UnityEngine;

public class Present : MonoBehaviour
{
    [SerializeField] Inu[] inuList;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Sprite missSprite;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPos = transform.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Land")
        {
            Debug.Log("prezent obore");
            GameSceneController.instance.Miss();
            Miss(null);
        }
    }

    public void Success()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach (var inu in inuList)
        {
            inu.Success();
        }
    }

    public void Miss(Inu oboreInu)
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        foreach(var inu in inuList) 
        { 
            if(inu != oboreInu)
            {
                inu.Miss();
            }
        }
        if(oboreInu == null)
        {
            this.Obore();
        }
    }

    public void Obore()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        float beforeY = sprite.transform.position.y;

        Vector3 newPos = sprite.transform.position;

        if (newPos.y % 1 > 0.65f)
        {
            newPos.y = Mathf.Round(newPos.y - 0.81f) + 0.6f;
        }

        sprite.transform.position = newPos;

        sprite.sprite = missSprite;
    }


    [SerializeField] bool playSound = true;
    float playSoundFrequency = 1f;

    Vector3 lastPos;
    float playSoundTimer = 0;
    private void Update()
    {
        if (playSound)
        {
            playSoundTimer -= (lastPos - transform.position).magnitude;
            lastPos = transform.position;
            //移動中か取得
            if (playSoundTimer < 0)
            {
                SoundManager.PlaySound(CommonData.SoundId.Move);
                playSoundTimer = playSoundFrequency;
            }
            //停止した後に移動した場合は、すぐに音が鳴るように
            if (rb.velocity.magnitude == 0)
            {
                playSoundTimer = 0;
            }
        }
    }
}
