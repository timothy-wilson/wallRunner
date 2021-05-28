using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject LeftHalfBar;
    public GameObject LeftWideBar;
    public GameObject LeftFullBar;
    public GameObject RightHalfBar;
    public GameObject RightWideBar;
    public GameObject RightFullBar;

    private Rigidbody rb;
    private Vector3 dir;
    private float E = 2.7182818284590452353602874713527f;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        dir = new Vector3(0.0f, 0.0f, -0.2f);
    }

    void Update()
    {
        rb.MovePosition(transform.position + dir);

        float shift = 4 / (1 + Mathf.Pow(E, -1 * (Time.frameCount - 1500)));    // logistic formula calculating the shift based on how many frames have passed
        int obstacles = findNumObstacles(shift);
        Debug.Log(obstacles);
    }


    int findNumObstacles(float shift)
    {
        List<float> probs = new List<float>();
        float total = 0;

        for (int i = 0; i < 5; i++)
        {
            float prob = -1 * Mathf.Pow(i - shift, 2) + 16;    // hyperbolla that shifts as the game goes on to increase chance of more obstacles;
            probs.Add(prob);
            total += prob;
        }

        float prevSum = 0;
        float randomGen = Random.value;
        for (int i = 0; i < 5; i++)
        {
            probs[i] = probs[i] / total;
            probs[i] += prevSum;
            prevSum = probs[i];

            if (randomGen < probs[i])
            {
                return i + 1;
            }
        }
        return 4;
    }
}
