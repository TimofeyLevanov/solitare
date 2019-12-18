using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartCreateCards : MonoBehaviour
{
    int testBullshit = 0;
    public int CountAntibag1 = 0;
    public int CountAntibag2 = 0;

    public Transform place_deck;
    public Transform place;
    public Transform buby;
    public Transform piki;
    public Transform черви;
    public Transform трефы;

    public Sprite sprite;
    public Transform deck;
    public int count = 0;
    int recursionLevel = 0;

    public Stack<Transform> PlaceDeck = new Stack<Transform>();
    public Stack<Transform> PlaceDeckNext = new Stack<Transform>();

    public Stack<Transform> finalPlace1 = new Stack<Transform>();
    public Stack<Transform> finalPlace2 = new Stack<Transform>();
    public Stack<Transform> finalPlace3 = new Stack<Transform>();
    public Stack<Transform> finalPlace4 = new Stack<Transform>();

    public List<Sprite> sprites = new List<Sprite>();

    public List<Transform> place1 = new List<Transform>();
    public List<Transform> place2 = new List<Transform>();
    public List<Transform> place3 = new List<Transform>();
    public List<Transform> place4 = new List<Transform>();
    public List<Transform> place5 = new List<Transform>();
    public List<Transform> place6 = new List<Transform>();
    public List<Transform> place7 = new List<Transform>();
    public List<Transform> place8 = new List<Transform>();

    public Stack<Transform> BackMoveParents = new Stack<Transform>();
    public Stack<int> BackMoveName = new Stack<int>();
    public Stack<Transform> BackMoveCurrentParents = new Stack<Transform>();
    public Stack<Transform> thisCard = new Stack<Transform>();
    public Stack<bool> ChangeBackFace = new Stack<bool>();
    public Stack<Vector3> lastPosition = new Stack<Vector3>();
    public Stack<bool> comeBackDeck = new Stack<bool>();
    public Stack<bool> changeRubashkaDeckCards = new Stack<bool>();

    int count_z = 0;
    int tt = 0;

    bool STOP = false;

    List<List<int>> identicalCount = new List<List<int>>();
    List<List<string>> identicalLastName = new List<List<string>>();

    Transform LastParentCard;
    Transform LastChildrenCard;

    string s;
    Transform CurrentCardTransform;
    List<Transform> AllCards = new List<Transform>();

    GameObject CurrentCard;
    Transform placesForCards;

    Transform lastParent;
    List<GameObject> thisGO = new List<GameObject>();
    List<Transform> ThisGOparent = new List<Transform>();

    List<GameObject> StackMovedCards = new List<GameObject>();
    List<GameObject> Lastcards = new List<GameObject>();
    List<GameObject> Lastcards1 = new List<GameObject>();
    List<List<GameObject>> bagcontoller = new List<List<GameObject>>();
    List<GameObject> places = new List<GameObject>();

    private List<Transform> randomCards(List<Transform> array)
    {
        for (int i = array.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            // обменять значения array[j] и array[i]
            var temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
        return array;
    }


    void Start()
    {
        //объединяю карты в одну коллекцию, что бы их перемешать, и разложить в случайном порядке.
        createCard(buby.transform);
        createCard(piki.transform);
        createCard(черви.transform);
        createCard(трефы.transform);
        //перемешиваю карты случайным образом.
        AllCards = randomCards(AllCards);

       int count = 0;
        for (int i = 1; i < 8; i++)
        {
            for (int x = i; x < 8; x++)
            {
                CurrentCard = AllCards[ count ].gameObject;
                placesForCards = place.GetChild(x-1);
                CurrentCard = Instantiate(CurrentCard, new Vector3(placesForCards.position.x, placesForCards.position.y, count_z), Quaternion.identity);
                Transform CurrentCardTransform = CurrentCard.transform;

                Debug.Log(placesForCards.childCount);
                CurrentCardTransform.parent = placesForCards;

                if (i != 1){
                    CurrentCardTransform.position = new Vector3(CurrentCardTransform.position.x, CurrentCardTransform.position.y - (0.3f * (placesForCards.childCount - 1)), count_z);
                    Debug.Log(CurrentCardTransform.position.y);
                }
                if(x != i){
                    exchangeSprite();
                }else{
                    //если карта последняя в стобце, то активируем флажки, что бы карту можно было передвигать, и класть на них другие карты.
                    CurrentCard.GetComponent<BoxCollider2D>().isTrigger = true;
                    CurrentCard.GetComponent<BoxCollider2D>().enabled = true;
                }
                count++;
                CurrentCardTransform.parent = placesForCards;

            }
            count_z--;
        }
        int orderCount = 2;
        count_z = -1;
        for (int i = count; i < AllCards.Count; i++)
        {
            CurrentCard = Instantiate(AllCards[i].gameObject, new Vector3(deck.position.x, deck.position.y, count_z), Quaternion.identity);
            CurrentCardTransform = CurrentCard.transform;

            CurrentCardTransform.parent = deck;
            PlaceDeck.Push(CurrentCardTransform);
            sprites.Add(CurrentCard.GetComponent<SpriteRenderer>().sprite);
            CurrentCard.GetComponent<SpriteRenderer>().sprite = sprite;
            CurrentCardTransform.GetComponent<move>().thisDeckCards = true;
            CurrentCardTransform.GetComponent<move>().mouseStop = false;
            orderCount++;
            count_z--;
        }
        CurrentCard.GetComponent<BoxCollider2D>().enabled = true;
            
        count = 0;
    }
    private void createCard(Transform suit){
      
        for (int j = 0; j < 13; j++)
        {
            AllCards.Add(suit.GetChild(j));
        }
    }
    private void exchangeSprite()
    {
            sprites.Add(CurrentCard.GetComponent<SpriteRenderer>().sprite);
            CurrentCard.GetComponent<SpriteRenderer>().sprite = sprite;
            CurrentCard.GetComponent<BoxCollider2D>().enabled = false;
    }
}

