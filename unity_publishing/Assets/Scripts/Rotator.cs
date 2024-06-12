using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    /// rotate coin dot gif
    void Update()
    {
        transform.Rotate(45 * Time.deltaTime, 0, 0);
    }
}
