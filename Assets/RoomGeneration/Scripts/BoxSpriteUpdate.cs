using UnityEngine;

public class BoxSpriteUpdate : MonoBehaviour
{
    SpriteRenderer topSpriteRenderer;
    SpriteRenderer bottomSpriteRenderer;
    public Sprite[] topSprites;
    public Transform parent;
    public Sprite[] bottomSprites;
    public float dropChance = 0.5f;
    public GameObject drop;
    int health;
    int maxHealth;
    private void Start()
    {
        parent = GameObject.FindGameObjectWithTag("SceneSpawner").GetComponent<Transform>();
    }
    private void Update()
    {
        GameObject top = transform.GetChild(0).gameObject;
        GameObject bottom = transform.GetChild(1).gameObject;
        topSpriteRenderer = top.GetComponent<SpriteRenderer>();
        bottomSpriteRenderer = bottom.GetComponent<SpriteRenderer>();
        health = GetComponent<Stats>().health;
        maxHealth = GetComponent<Stats>().maxHealth;
        if (health >= 0.5 * maxHealth)
        {
            topSpriteRenderer.sprite = topSprites[0];
            bottomSpriteRenderer.sprite = bottomSprites[0];
        }
        else if (health >= 0.3 * maxHealth)
        {
            topSpriteRenderer.sprite = topSprites[1];
            bottomSpriteRenderer.sprite = bottomSprites[1];
        }
        else if (health >= 0.1 * maxHealth)
        {
            topSpriteRenderer.sprite = topSprites[2];
            bottomSpriteRenderer.sprite = bottomSprites[2];
        }
        else if (health > 0)
        {
            topSpriteRenderer.sprite = topSprites[3];
            bottomSpriteRenderer.sprite = bottomSprites[3];
        }
    }
    public void BoxDeath()
    {
        Debug.Log("ѕытаюсь дропнуть аптечку");
        System.Random random = new System.Random();
        double chance = random.NextDouble();
        if (chance < dropChance)
        {
            Instantiate(drop, transform.position - new Vector3(0, 0.5f, 0), transform.rotation, parent);
        }
    }
}
