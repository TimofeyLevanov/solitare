using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticValues : MonoBehaviour
{
    public Stack<Transform> BackMoveParents = new Stack<Transform>();
    public Stack<Transform> BackMoveCurrentParents = new Stack<Transform>();
    public Stack<Transform> thisCard = new Stack<Transform>();
    public Stack<bool> ChangeBackFace = new Stack<bool>();
    public Stack<bool> comeBackDeck = new Stack<bool>();
    public Stack<bool> changeRubashkaDeckCards = new Stack<bool>();
    public Stack<Vector3> lastPosition = new Stack<Vector3>();
    public Stack<int> BackMoveName = new Stack<int>();
}
