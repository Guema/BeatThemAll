using UnityEngine;

[CreateAssetMenu(fileName = "xxxCollectableStats", menuName = "Scriptables/Collectable", order = 1)]
public class CollectableStats : ScriptableObject
{
    public int healAmount;
    public int scoreAmount;
    public int usesAmount;
}