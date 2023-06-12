using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float speed = 1.5f;

    IEnumerator Start()
    {
        Vector3 startPos = new(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        Vector3 endPos = new(transform.position.x, transform.position.y - 0.1f, transform.position.z);

        while (true)
        {
            yield return RepeatLerp(startPos, endPos, 3.0f);
            yield return RepeatLerp(endPos, startPos, 3.0f);
        }
    }
    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
