using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clicks : MonoBehaviour
{
    public AudioSource fuck;
    GameObject place;

    List<Sprite> sprites = new List<Sprite>();
    public AudioClip trr;
    GameObject deck;
    GameObject deck_next;
    Transform lastCard;
    public Sprite sprite;


    Stack<Transform> placeDeck;
    Stack<Transform> placeDeckNext;
    Stack<Transform> finalPlace1 = new Stack<Transform>();
    Stack<Transform> finalPlace2 = new Stack<Transform>();
    Stack<Transform> finalPlace3 = new Stack<Transform>();
    Stack<Transform> finalPlace4 = new Stack<Transform>();
    StartCreateCards колода;
    move help;
    GameObject help2;
    Stack<Transform> BackMoveParents;
    Stack<int> BackMoveName;
    Stack<Transform> BackMoveCurrentParents;
    Stack<Transform> thisCard;
    Stack<bool> ChangeBackFace;
    Stack<Vector3> lastPosition;
    Stack<bool> comeBackDeck;
    Stack<bool> changeRubashkaDeckCards;

    private void Start()
    {
        inicilization();
    }
    private void inicilization()
    {
        place = GameObject.Find("place").gameObject;
        help2 = GameObject.Find("колода").gameObject;
        колода = help2.GetComponent<StartCreateCards>();
        help = help2.GetComponent<move>();
        sprites = колода.sprites;

        placeDeckNext = колода.PlaceDeckNext;
        placeDeck = колода.PlaceDeck;

        deck = GameObject.Find("колода").gameObject;
        deck_next = GameObject.Find("place_deck").gameObject;

        BackMoveParents = help.BackMoveParents;
        BackMoveName = help.BackMoveName;
        BackMoveCurrentParents = help.BackMoveCurrentParents;
        ChangeBackFace = help.ChangeBackFace;
        thisCard = help.thisCard;
        lastPosition = колода.lastPosition;
        comeBackDeck = колода.comeBackDeck;
        changeRubashkaDeckCards = колода.changeRubashkaDeckCards;

        finalPlace1 = колода.finalPlace1;
        finalPlace2 = колода.finalPlace2;
        finalPlace3 = колода.finalPlace3;
        finalPlace4 = колода.finalPlace4;
        Debug.Log("завершенно");

    }
    public void back()
    {
        GameObject cam1 = GameObject.Find("Camera").gameObject;
        
        if (BackMoveParents.Count == 0)
        {
            Debug.Log("нечего отменять");
            return;
        }
        if (placeDeckNext.Count == 0)
        {
            cam1.GetComponent<sound1>().Koloda();
            deckToStart();
            return;
        }
        Transform card = thisCard.Pop();

        Transform parent = BackMoveParents.Pop();
        Vector3 positionCard = lastPosition.Pop();
        bool a = ChangeBackFace.Pop();
        Transform LastRubashka;
 
        if (parent.childCount > 0)
        {
            LastRubashka = parent.GetChild(parent.childCount - 1);
        } else
        {
            LastRubashka = parent;
        }

        deckAction(card);
        Transform LastParentForTrigger = card.parent;
     
        if (card.parent.parent)
        {
            if (card.parent.parent.name == "place")
            {
                clidParentToCard(card, parent);//меняем родителя на текущую карту, что бы изменив коардинаты, вся стопка перемещалась.
            }
        }
     
        card.transform.position = new Vector3(positionCard.x, positionCard.y, positionCard.z - 1);
        card.parent = parent;
        if(parent.name == "колода")
        {
            if (placeDeckNext.Count == 0)
            {
                Debug.Log("I Lock for it, bith!A found!");
            } else
            {
                placeDeck.Push(placeDeckNext.Pop());
            }
        }
        if (LastParentForTrigger.childCount > 0)
        {
            LastParentForTrigger.GetChild(LastParentForTrigger.childCount - 1).GetComponent<BoxCollider2D>().isTrigger = true; //что бы на карту, с которой убрали карту можно было ложить карты...
        } else
        {
            LastParentForTrigger.GetComponent<BoxCollider2D>().isTrigger = true; //что бы на карту, с которой убрали карту можно было ложить карты...
        }
        if (card.childCount > 0)
        {
            for(int i = 0; i < card.childCount; i++)
            {
                card.GetChild(i).parent = parent;
            }
        }

        if (a)
        {
            Debug.Log("РУБАШКА ПОМЕНЯЛАСЬ "+parent.name +" "+sprite.name);
            if(LastRubashka.name != колода.name)//костыль, колоде меняется спрайт на рубашку, почему?
            {
                LastRubashka.GetComponent<SpriteRenderer>().sprite = sprite;
                LastRubashka.GetComponent<BoxCollider2D>().isTrigger = false;
            }
          
        } else
        {
            Debug.Log(a + "=(");
        }
      
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void hint()
    {
        Hint hint = new Hint();
        hint.hint();
    }
       /*
        *
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
        if(place.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "рубашка")
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
    */
    int fatherPlaceNumber(GameObject ThisGoParent)
    {

        for (int i = 1; i < 8; i++)
        {
            if (i.ToString() == ThisGoParent.name)
            {
                return i;
            }
        }
        Debug.Log("произошла ошибка в методе fatherPlaceNumber цикл не нашел элемент " + ThisGoParent.name);
        return 0;
    }
    void clidParentToCard(Transform card, Transform parent)
    {
        Debug.Log(card.name);
        int count = card.parent.childCount;
        int index = 0;
        for(int i = 0; i < count; i++)
        {
            if(card.name == card.parent.GetChild(i).name)
            {
                index = i+1;//следующий элемент
            }
        }
        Debug.Log("count = " + count + " index = " + index +"count - index=" +(count-index));  
            int b = 0;
            while(true){ 
                b++;
                if (b > 20)
                {
                    break;
                    Debug.Log("бесконечный цикл");
                }
                if (card.parent.childCount>index)
                {
                    Debug.Log(card.parent.childCount);
                    Debug.Log(card.parent.GetChild(index).name + " изменили родителя");
                    card.parent.GetChild(index).parent = card;
                } else
                {
                    break;
                }
           }
    }
    void deckAction(Transform card)
    {
        
        if (comeBackDeck.Pop())
        {
            card.GetComponent<SpriteRenderer>().sprite = sprite;
            card.GetComponent<move>().thisDeckCards = true;
        }
    }
    void deckToStart()
    {
        Debug.Log("HM");
        int z = -2;
        for (; placeDeck.Count > 0;)
        {
 
            placeDeck.Peek().position = new Vector3(deck_next.transform.position.x, deck_next.transform.position.y, z);
            placeDeck.Peek().parent = deck_next.transform;
            exchangeSpriteCards(placeDeck.Peek());
            placeDeck.Peek().GetComponent<move>().thisDeckCards = true;
            Debug.Log(placeDeckNext.Count);
            placeDeckNext.Push(placeDeck.Pop());
            z--;
        }
        колода.GetComponent<BoxCollider2D>().enabled = true;
    }
    void exchangeSpriteCards(Transform card)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (sprites[i].name + "(Clone)" == card.name)
            {
                card.GetComponent<SpriteRenderer>().sprite = sprites[i];
                break;
            }
        }
    }
}
