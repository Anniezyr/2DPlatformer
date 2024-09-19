using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFIreballHolder : MonoBehaviour
{
    [SerializeField] private Transform ene;
    private void Update()
    {
        transform.localScale = ene.localScale;
    }
}
