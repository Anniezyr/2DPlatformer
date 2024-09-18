using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialposition;

    private void Awake()
    {
        //save all enemies' position at the beginning
        initialposition = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialposition[i] = enemies[i].transform.position;
        }
    }

    public void ActiveRoom(bool _status)
    {
        if (enemies.Length > 0)
        {
            //activate or deactivate enemies
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialposition[i];//reset the enemies'position
            }
        }
        
    }
}
