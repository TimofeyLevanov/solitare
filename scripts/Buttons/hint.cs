using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    GameObject place;

    public void Hint()
    {
        place = GameObject.Find("place").gameObject;
    }
    public void hint()
    {
     
        Transform hint;
        for (int i = 1; i < 7; i++)
        {
            if (CayLockFordBackFacd(place.transform.GetChild(i)))
            {
                Debug.Log(lockForBackFace(place.transform.GetChild(i)).name);
                hint = lockForBackFace(place.transform.GetChild(i));
            }
            break;
        }
    }

    private bool CayLockFordBackFacd(Transform place)
    {
        if (place.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "рубашка")
        {
            return true;
        }
        return false;
    }
    Transform lockForBackFace(Transform place)
    {
        for (int i = 1; i < place.childCount; i++)
        {
            if (place.GetChild(i).GetComponent<SpriteRenderer>().sprite.name != "рубашка")
            {
                return place.GetChild(i);
            }
        }
        Debug.Log("что-то пошло не так, это часть кода не дожна никогда работать");
        return place;
    }
}
