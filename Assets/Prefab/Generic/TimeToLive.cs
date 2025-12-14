using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    [SerializeField] float timeToLive = 5f;
    public float timer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLive)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }

    }
}
