using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]//Para que cada arma tenga acceso a el menu de estadísticas
public class WeaponScriptableObject : ScriptableObject //Esto es una función ya de Unity para lo mismo
{
    [SerializeField] //Este es un método de protección para que no se pueda sobreescribir sobre variables
    GameObject prefab; //Es la animación de nuestra arma
    public GameObject Prefab{get => prefab; private set => prefab = value;}
    //Son las stats de cada arma
    [SerializeField]
    float damage; //El daño de nuestra arma
    public float Damage {get => damage; private set => damage = value;}
    [SerializeField]
    float speed; //La velocidad de nuestra arma
    public float Speed {get => speed; private set => speed = value;}
    [SerializeField]
    float cooldownDuration; //El cooldown de nuestra arma
    public float CooldownDuration {get => cooldownDuration; private set => cooldownDuration = value;}
    [SerializeField]
    int pierce; //Cuantas veces puede el arma golpear los enemigos
    public int Pierce {get => pierce; private set => pierce = value;}
    [SerializeField]
    int repeat; //Cuantas veces puede repetirse el arma
    public int Repeat {get => repeat; private set => repeat = value;}
    [SerializeField]
    float cooldownBetween;
    public float CooldownBetween {get => cooldownBetween; private set => cooldownBetween = value;}
    [SerializeField]
    int level;
    public int Level {get => level; private set => level = value;}
    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab {get => nextLevelPrefab; private set => nextLevelPrefab = value;}

    [SerializeField]
    new string name;
    public string Name {get => name; private set => name = value;}

    [SerializeField]
    string description;
    public string Description {get => description; private set => description = value;}

    [SerializeField]
    Sprite icon;
    public Sprite Icon {get => icon; private set => icon = value;}
    //Pendiente
}
