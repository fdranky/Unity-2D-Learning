using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] int health = 500;
    float xMin, xMax, yMin, yMax;
    Coroutine firingCoroutine;
    [SerializeField] AudioClip fireSFX;
    [SerializeField] [Range(0, 1)] float fireSoundVolume = 0.25f;
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] [Range(0, 1)] float explosionSoundVolume = 0.75f;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip warningSFX;
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }
    
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    // Update is called once per frame

    private void Move()
    {
        // The following is done to keep the game frame independent on every machine
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }
    
    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(fireSFX, Camera.main.transform.position, fireSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.Damage;
        damageDealer.Hit();

        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject destructionExplosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(destructionExplosion, durationOfExplosion);
            AudioSource.PlayClipAtPoint(explosionSFX, Camera.main.transform.position);
            SceneManager.LoadScene("GameOver");
        }
    }
}
