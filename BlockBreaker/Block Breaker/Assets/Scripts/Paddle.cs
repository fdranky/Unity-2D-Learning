using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // Cache
    GameStatus gameStatus;
    Ball ball;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        position.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = position;
    }

    private float GetXPos()
    {
        return gameStatus.IsAutoPlayEnabled ? 
            ball.transform.position.x : 
            Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
