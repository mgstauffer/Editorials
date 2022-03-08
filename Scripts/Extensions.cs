using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static bool IsInRange(this int i, int min, int max)
    {
        if (i > min && i < max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsInRange(this int i, int min, int max, bool inclusive)
    {
        if (inclusive)
        {
            if (i >= min && i <= max)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            if (i > min && i < max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public static Vector3 GetRandomPointInBounds(this Collider col)
    {
        return new Vector3(
            Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y),
            Random.Range(col.bounds.min.z, col.bounds.max.z)
        );
    }

    public static Vector2 GetRandomPointInBounds2D(this Collider2D col)
    {
        return new Vector2(
            Random.Range(col.bounds.min.x, col.bounds.max.x),
            Random.Range(col.bounds.min.y, col.bounds.max.y)
        );
    }

    public static Vector3 GetRandomPointInRange(this UnityEngine.MonoBehaviour mono, float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        return new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            Random.Range(minZ, maxZ)
        );
    }
    public static Vector2 GetRandomPointInRange2D(this UnityEngine.MonoBehaviour mono, float minX, float maxX, float minY, float maxY)
    {
        return new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
    }

    public static void CreateObject(this GameObject obj, string name, Vector3 spawnPos, Quaternion spawnRot)
    {
        GameObject g = new GameObject(name);
        g.transform.position = spawnPos;
        g.transform.rotation = spawnRot;
    }

    public static void CreateObject(this GameObject obj, string name, Vector3 spawnPos, Quaternion spawnRot, params System.Type[] components)
    {
        GameObject g = new GameObject(name);
        g.transform.position = spawnPos;
        g.transform.rotation = spawnRot;
        if(components.Length > 0)
        {
            foreach(System.Type comp in components)
            {
                g.AddComponent(comp);
            }
        }
    }
}
