using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]

public class EnemyScriptableObject : ScriptableObject
{
    //Stats base de los enemigos
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField]
    float maxhealth;
    public float Maxhealth {get => maxhealth; private set => maxhealth = value;}
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}

}
