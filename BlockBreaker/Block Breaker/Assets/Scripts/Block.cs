using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] float destroyTime;
    [SerializeField] AudioClip audioClip;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // Cached reference
    Level level;
    GameStatus gameStatus;

    // State variables
    [SerializeField] int timesHit;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }

        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            timesHit++;
            HandleHit();
        }
    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        TriggerSparklesVFX();
        Destroy(gameObject, destroyTime);
        level.BreakableBlockDestroyed();
        gameStatus.AddToScore();
    }

    private void TriggerSparklesVFX()
    {
        GameObject effect = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(effect, 1f);
    }
}
