using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Earth,
    Fire,
    Water,
    Hole,
    Ice,
    Lava,
    Hole_Filled
}

public class Combination_Manager : MonoBehaviour
{
    public List<GameObject> objectPrefabs;

    public bool CheckCombination(ObjectType firstType, GameObject firstObject, GameObject secondObject)
    {
        if (firstType == ObjectType.Earth)
        {
            if (secondObject.CompareTag("Hole"))
            {
                DestroyAndSpawnNewObject(6, firstObject, secondObject);
                return true;
            }

            else if (secondObject.CompareTag("Lava"))
            {
                Destroy(firstObject);
                return true;
            }
        }

        if(firstType == ObjectType.Fire)
        {
            if (secondObject.CompareTag("Door"))
            {
                DestroyBothObjects(firstObject, secondObject);
                return true;
            }

            else if (secondObject.CompareTag("Ice"))
            {
                DestroyAndSpawnNewObject(2, firstObject, secondObject);
                return true;
            }

           else if(secondObject.CompareTag("Element"))
            {
                if (secondObject.GetComponent<Element_Push>().elementType == ObjectType.Earth)
                {
                    DestroyAndSpawnNewObject(5, firstObject, secondObject);
                    return true;
                }
            }

            else if (secondObject.CompareTag("Lava"))
            {
                Destroy(firstObject);
                return true;
            }
        }

        if (firstType == ObjectType.Water)
        {
            if (secondObject.CompareTag("Lava"))
            {
                DestroyAndSpawnNewObject(0, firstObject, secondObject);
                return true;
            }
        }
        return false;
    }

    private void DestroyAndSpawnNewObject(int i, GameObject firstObject, GameObject secondObject)
    {
        if (firstObject.CompareTag("Element"))
        {
            firstObject.GetComponent<Element_Push>().SpawnParticleEffect();
        }

        Instantiate(objectPrefabs[i], secondObject.transform.position, secondObject.transform.rotation);
        Destroy(firstObject);
        Destroy(secondObject);
    }

    private void DestroyBothObjects(GameObject firstObject, GameObject secondObject)
    {
        if (firstObject.CompareTag("Element"))
        {
            firstObject.GetComponent<Element_Push>().SpawnParticleEffect();
        }

        Destroy(firstObject);
        Destroy(secondObject);
    }
}
