using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool <T>: MonoBehaviour
{
    T[] objects;

    Dictionary<T, Vector3> positions = new Dictionary<T, Vector3>();

    


   /* T Instatntiate (T obj)
    {
        foreach (T t in objects)
        {
            if (Compare(t, obj))
            {
                
            }
        }
    }*/


    public bool Compare(T a, T b)
    {
        return EqualityComparer<T>.Default.Equals(a, b);
    }
}

// object pooling under construction
// This will be completed next time.
// In the meantime, please enjoy the gameplay.
// Thank you for your patience.
// Regards
// Zain and Farooq