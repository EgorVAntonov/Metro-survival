using UnityEngine;

public class BackgroundParallaxController : MonoBehaviour
{
    [SerializeField] private float[] LayerSpeed = new float[5];
    [SerializeField] private GameObject[] LayerObjects = new GameObject[5];

    [SerializeField] private Transform player;
    private float[] startPos = new float[5];
    private float boundSizeX;
    private float sizeX;

    void Start()
    {
        sizeX = LayerObjects[0].transform.localScale.x;
        boundSizeX = LayerObjects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i = 0; i < 5; i++)
        {
            startPos[i] = player.position.x;
        }
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
        {
            float temp = (player.position.x * (1 - LayerSpeed[i]));
            float distance = player.position.x * LayerSpeed[i];
            LayerObjects[i].transform.localPosition = new Vector2(startPos[i] + distance, 0f);
            float width = boundSizeX * sizeX;
            if (temp > startPos[i] + width)
            {
                startPos[i] += width;
            }
            else if (temp < startPos[i] - width)
            {
                startPos[i] -= width;
            }

        }
    }
}
