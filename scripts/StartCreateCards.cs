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

    public List<GameObject> cards = new List<GameObject>();

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


    List<MyType> placesMy1 = new List<MyType>();
    List<MyType> placesMy2 = new List<MyType>();
    List<MyType> placesMy3 = new List<MyType>();
    List<MyType> placesMy4 = new List<MyType>();
    List<MyType> placesMy5 = new List<MyType>();
    List<MyType> placesMy6 = new List<MyType>();
    List<MyType> placesMy7 = new List<MyType>();
    List<MyType> placesMy8 = new List<MyType>();

    List<Stack<MyType>> placesFinal = new List<Stack<MyType>>();
    Stack<MyType> final1 = new Stack<MyType>();
    Stack<MyType> final2 = new Stack<MyType>();
    Stack<MyType> final3 = new Stack<MyType>();
    Stack<MyType> final4 = new Stack<MyType>();

    int count_z = 0;
    int tt = 0;

    bool ttt = false;
    bool STOP = false;

    List<List<int>> identicalCount = new List<List<int>>();
    List<List<string>> identicalLastName = new List<List<string>>();

    Transform LastParentCard;
    Transform LastChildrenCard;
    List<string> AntiBag = new List<string>();
    int value = 0;

    string s;
    Transform objTrans;
    List<Transform> AllCards = new List<Transform>();
    GameObject obj;
    GameObject obj2;
    Transform obj3;

    Transform lastParent;
    List<GameObject> thisGO = new List<GameObject>();
    List<Transform> ThisGOparent = new List<Transform>();

    List<GameObject> StackMovedCards = new List<GameObject>();
    List<GameObject> Lastcards = new List<GameObject>();
    List<GameObject> Lastcards1 = new List<GameObject>();
    List<List<GameObject>> bagcontoller = new List<List<GameObject>>();



    List<Transform> randomCards(List<Transform> array)

    {

        for (int i = array.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            // обменять значения data[j] и data[i]
            var temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
        return array;
    }


    void Start()
    {


        for (int j = 0; j < 13; j++)
        {

            AllCards.Add(buby.transform.GetChild(j));
        }

        for (int j = 0; j < 13; j++)
        {
            AllCards.Add(piki.transform.GetChild(j));
        }
        
               for (int j = 0; j < 13; j++)
               {
                   AllCards.Add(черви.transform.GetChild(j));
               }

               for (int j = 0; j < 13; j++)
               {
                    AllCards.Add(трефы.transform.GetChild(j));
               }
               
        int count = 0;
          AllCards = randomCards(AllCards);
          AllCards = randomCards(AllCards);


        for (int i = 1; i < 8; i++)
        {
            for (int x = i; x < 8 && count < AllCards.Count; x++)
            {
                s = x.ToString();
                obj2 = AllCards[count].gameObject;
                obj3 = place.transform.Find(s);

                obj = Instantiate(obj2, new Vector3(obj3.position.x, obj3.position.y, count_z), Quaternion.identity);
                objTrans = obj.transform;
                if (x == 1)
                {
                    place1.Add(objTrans);
                }
                if (x == 2)
                {
                    place2.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place2.Count - 1)), count_z);
                    }
                    rubashka(place2, 2);
                }
                if (x == 3)
                {
                    place3.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place3.Count - 1)), count_z);
                    }
                    rubashka(place3, 3);
                }
                if (x == 4)
                {
                    place4.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place4.Count - 1)), count_z);
                    }
                    rubashka(place4, 4);
                }
                if (x == 5)
                {
                    place5.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place5.Count - 1)), count_z);
                    }
                    rubashka(place5, 5);
                }
                if (x == 6)
                {
                    place6.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place6.Count - 1)), count_z);
                    }
                    rubashka(place6, 6);
                }
                if (x == 7)
                {
                    place7.Add(objTrans);
                    if (i != 1)
                    {
                        objTrans.position = new Vector3(objTrans.position.x, objTrans.position.y - (0.3f * (place7.Count - 1)), count_z);
                    }
                    rubashka(place7, 7);
                }

                if (x == i)
                {
                    obj.GetComponent<BoxCollider2D>().isTrigger = true;
                }

                objTrans.GetComponent<move>().thisDeckCards = false;
                objTrans.GetComponent<move>().mouseStop = true;
                // obj.GetComponent<SpriteRenderer>().sortingOrder = obj3.GetComponent<SpriteRenderer>().sortingOrder + 1;
                cards.Add(obj);
                count++;
                objTrans.parent = obj3;

            }
            count_z = count_z - 1;
        }
        int orderCount = 2;
        count_z = -1;
        Vector3 deckPosition = deck.position;
        for (int i = count; i < AllCards.Count; i++)
        {
            obj = Instantiate(AllCards[i].gameObject, new Vector3(deckPosition.x, deckPosition.y, count_z), Quaternion.identity);
            objTrans = obj.transform;
            place8.Add(objTrans);

            objTrans.parent = deck;
            PlaceDeck.Push(objTrans);
            sprites.Add(obj.GetComponent<SpriteRenderer>().sprite);
            obj.GetComponent<SpriteRenderer>().sprite = sprite;
            //obj.GetComponent<SpriteRenderer>().sortingOrder = orderCount;
            objTrans.GetComponent<move>().thisDeckCards = true;
            objTrans.GetComponent<move>().mouseStop = false;
            cards.Add(obj);
            orderCount++;
            if (AllCards.Count - 1 > i)
            {
                obj.GetComponent<BoxCollider2D>().enabled = false;
            }

            count_z--;
        }
        count = 0;
       // reqursion(CheckMoveCards());
       // Debug.Log("КОНЕЦ " + count + " "+ testBullshit);


    }

    private void rubashka(List<Transform> list, int lastValue)
    {
       
            if (list.Count < lastValue)
            {
                sprites.Add(obj.GetComponent<SpriteRenderer>().sprite);
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
                obj.GetComponent<BoxCollider2D>().enabled = false;
            }
    }
    private List<List<MyType>> CheckMoveCards()
    {
        Debug.Log("start");
        placesFinal.Add(final1);
        placesFinal.Add(final2);
        placesFinal.Add(final3);
        placesFinal.Add(final4);

        placesMy1.Add(new MyType(place.transform.GetChild(0).GetComponent<Index>().index, place.transform.GetChild(0).name));
        placesMy2.Add(new MyType(place.transform.GetChild(1).GetComponent<Index>().index, place.transform.GetChild(1).name));
        placesMy3.Add(new MyType(place.transform.GetChild(2).GetComponent<Index>().index, place.transform.GetChild(2).name));
        placesMy4.Add(new MyType(place.transform.GetChild(3).GetComponent<Index>().index, place.transform.GetChild(3).name));
        placesMy5.Add(new MyType(place.transform.GetChild(4).GetComponent<Index>().index, place.transform.GetChild(4).name));
        placesMy6.Add(new MyType(place.transform.GetChild(5).GetComponent<Index>().index, place.transform.GetChild(5).name));
        placesMy7.Add(new MyType(place.transform.GetChild(6).GetComponent<Index>().index, place.transform.GetChild(6).name));

        List<List<MyType>> pMy = new List<List<MyType>>();
        pMy.Add(placesMy1);
        pMy.Add(placesMy2);
        pMy.Add(placesMy3);
        pMy.Add(placesMy4);
        pMy.Add(placesMy5);
        pMy.Add(placesMy6);
        pMy.Add(placesMy7);
        pMy.Add(placesMy8);
        List<List<Transform>> p = new List<List<Transform>>();
        p.Add(place1);
        p.Add(place2);
        p.Add(place3);
        p.Add(place4);
        p.Add(place5);
        p.Add(place6);
        p.Add(place7);
        p.Add(place8);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < p[i].Count; j++)
            {
                pMy[i].Add(new MyType(p[i][j].GetComponent<Index>().index, p[i][j].name, p[i][j].GetComponent<move>().lear_bubi, p[i][j].GetComponent<move>().lear_piki, p[i][j].GetComponent<move>().lear_chervi, p[i][j].GetComponent<move>().lear_trefy, p[i][j].GetComponent<BoxCollider2D>().enabled));
            }
        }
        return pMy;
    }
    void reqursion(List<List<MyType>> places)
    {
        int countMove = 0;
        count++;
        if (count > 200)
        {
            STOP = true;
        }
        for (int thisPlace = 0; thisPlace < places.Count; thisPlace++)////////////////////////
        {
            if (STOP)
            {
                break;
            }

            for (int NumberCards = 1; NumberCards < places[thisPlace].Count; NumberCards++)//==========================
            {
                if (places[thisPlace][NumberCards].box || thisPlace == 7)
                {
                  //  Debug.Log("взяли карту " + places[thisPlace][NumberCards].name);

                } else
                {
                    continue;
                }
                if (STOP)
                {
                    break;
                }
                if(NumberCards == 1 && places[thisPlace][NumberCards].index == 13)
                {
                    continue;
                }
                for (int NumberOnPlaceFinal = 0; NumberOnPlaceFinal < 4; NumberOnPlaceFinal++)
                {
                    if (places[thisPlace].Count-1==NumberCards || thisPlace == 7)
                    {
                        if (placesFinal[NumberOnPlaceFinal].Count > 0)
                        {
                            value = placesFinal[NumberOnPlaceFinal].Peek().index;
                        } else
                        {
                            value = 0;
                        }

                        if(value + 1 == places[thisPlace][NumberCards].index)
                        {

                            if (value == 0)
                            {
                                Debug.Log(thisPlace + " " + places[thisPlace][NumberCards].name + " > final");
                                placesFinal[NumberOnPlaceFinal].Push(places[thisPlace][NumberCards]);
                                places[thisPlace].RemoveAt(NumberCards);

                                ///////////////////////////////////////
                                recursionLevel++;
                      
                                    Debug.Log(recursionLevel);

                           
                                reqursion(places);
                                recursionLevel--;
                                if (STOP)
                                {
                                    Debug.Log("прекращенно досрочно 3");
                                    break;
                                }
                                Debug.Log("BACK " + places[thisPlace][places[thisPlace].Count - 1].name + " < " + placesFinal[NumberOnPlaceFinal].Peek().name+"final");

                                places[thisPlace].Add(placesFinal[NumberOnPlaceFinal].Pop());
                                break;
                            } else
                            {
                                bool lear_bubi = (placesFinal[NumberOnPlaceFinal].Peek().bubi && true) && places[thisPlace][NumberCards].bubi;
                                bool lear_chervi = (placesFinal[NumberOnPlaceFinal].Peek().chervi && true) && places[thisPlace][NumberCards].chervi;
                                bool lear_piki = (placesFinal[NumberOnPlaceFinal].Peek().piki && true) && places[thisPlace][NumberCards].piki;
                                bool lear_trefy = (placesFinal[NumberOnPlaceFinal].Peek().kresti && true) && places[thisPlace][NumberCards].kresti;
                                if(lear_bubi || lear_chervi || lear_piki || lear_trefy)
                                {
                                    Debug.Log(thisPlace+" "+ places[thisPlace][NumberCards].name + " > " + placesFinal[NumberOnPlaceFinal].Peek().name+"final");
                                    placesFinal[NumberOnPlaceFinal].Push(places[thisPlace][NumberCards]);
                                    places[thisPlace].RemoveAt(NumberCards);

                                    //////////////////////////
                                    recursionLevel++;
                         
                                     Debug.Log(recursionLevel);
//
                           

                                    reqursion(places);
                                    recursionLevel--;
                                    if (STOP)
                                    {
                                        Debug.Log("прекращенно досрочно 3");
                                        break;
                                    }
                                    Debug.Log("BACK " + thisPlace + " "+ places[thisPlace][places[thisPlace].Count - 1].name + " < " + placesFinal[NumberOnPlaceFinal].Peek().name+"final");

                                    places[thisPlace].Add(placesFinal[NumberOnPlaceFinal].Pop());
                                    break;
                                }
                            }
                            
                        }
                    }
                }
                    for (int NumberOnPlace = 0; NumberOnPlace < places.Count - 1; NumberOnPlace++)//============================================
                {
               
                    if (STOP)
                    {
                        break;
                    }

                    if (thisPlace != NumberOnPlace)//карты берем из одной колонки, и кладем на другие(Но не на эту же)
                    {
                        value = places[NumberOnPlace][places[NumberOnPlace].Count - 1].index;
                        bool a;
                        if (places[NumberOnPlace].Count - 1 == 0)
                        {
                            a = true;
                        } else
                        {
                            bool red1 = places[NumberOnPlace][places[NumberOnPlace].Count - 1].chervi || places[NumberOnPlace][places[NumberOnPlace].Count - 1].bubi;
                            bool red2 = places[thisPlace][NumberCards].chervi || places[thisPlace][NumberCards].bubi; 
                            a = red1 != red2;
                        }
                        if (places[thisPlace][NumberCards - 1].box == false || NumberCards == 1)
                        {
                            if (a && places[thisPlace][NumberCards].index + 1 == value)
                            {
                                if (thisPlace != 7)
                                {
                                      Debug.Log(thisPlace + " " + places[thisPlace][NumberCards].name + " > " + places[NumberOnPlace][places[NumberOnPlace].Count - 1].name+" "+ NumberOnPlace);

                                    while (true)
                                    {
                                        if (places[thisPlace].Count - 1 >= NumberCards)
                                        {
                                            places[NumberOnPlace].Add(places[thisPlace][NumberCards]);
                                            places[thisPlace].RemoveAt(NumberCards);
                                            countMove++;
                                        } else
                                        {
                                            break;
                                        }
                                    }
                                } else
                                {
                                       Debug.Log(thisPlace + " " + places[thisPlace][NumberCards].name + " > " + places[NumberOnPlace][places[NumberOnPlace].Count - 1].name + " " + NumberOnPlace);
                                    places[NumberOnPlace].Add(places[thisPlace][NumberCards]);
                                    places[thisPlace].RemoveAt(NumberCards);
                                    countMove++;
                                }

                                // Debug.Log("REQURSION ENTER ENTER ENTER ENTER " +"Bag i=" +i+ " t=" + t + " places[i].Count=" + places[i].Count);
                                recursionLevel++;
                           
                                   Debug.Log(recursionLevel);

                             

                                reqursion(places);
                                recursionLevel--;
                                if (STOP)
                                {
                                    Debug.Log("прекращенно досрочно 3");
                                    break;
                                }

                                BackMove(countMove, places, thisPlace, NumberOnPlace);
                                countMove = 0;
                                //Debug.Log("REQURSION EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT EXIT");
                            } else
                            {
                                // Debug.Log("ПОТОМ РЕШИМ");
                            }
                        } 
                    }
                }
            }
        }
    }
    void BackMove(int countMove, List<List<MyType>> places, int thisPlace, int NumberOnPlace)
    {
        for (int r = countMove; r > 0; r--)
        {
            // AntiBag.RemoveAt(AntiBag.Count - 1);
            Debug.Log("BACK " + thisPlace + " " + places[thisPlace][places[thisPlace].Count - 1].name + " < " + places[NumberOnPlace][places[NumberOnPlace].Count - countMove].name + " " + NumberOnPlace);
            places[thisPlace].Add(places[NumberOnPlace][places[NumberOnPlace].Count - r]);
            places[NumberOnPlace].RemoveAt(places[NumberOnPlace].Count - r);
        }
    }
    bool isItUniqueName(List<string> list)
    {
        for (int j = 0; j < identicalLastName.Count; j++)
        {
            if (identicalLastName[j].Count == list.Count)
            {
                if (identicalLastName[j].Compare(list, false))
                {
                    return false;
                }
            }
        }
        if (identicalLastName.Count == 0)
        {
            return true;
        }
        return true;

    }
    bool isItUnique(List<int> CurrentCount)
    {
        for (int j = 0; j < identicalCount.Count - 1; j++)
        {
            if (identicalCount[j].Compare(CurrentCount, false))
            {
                return false;
            }

        }
        if (identicalCount.Count == 0)
        {
            return true;
        }
        return true;
    }
    bool lastBoxClose(MyType current, List<List<MyType>> places)
    {
        MyType a;
        bool stop = false;
        for (int i = 0; i < places.Count - 1; i++)
        {
            if (stop)
            {
                break;
            }
            for (int j = places[i].Count - 1; j > 1; j--)
            {
                if (places[i][j].box == false)
                {
                    if (places[i][j].index == current.index - 1 && places[i][j].red != current.red)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
public static class Jopa
{
    public static bool Compare<T>(this IList<T> list1, IList<T> list2, bool isOrdered = true)
    {
        if (list1 == null && list2 == null)
            return true;
        if (list1 == null || list2 == null || list1.Count != list2.Count)
            return false;

        if (isOrdered)
        {
            for (int i = 0; i < list2.Count; i++)
            {
                var l1 = list1[i];
                var l2 = list2[i];
                if (
                     (l1 == null && l2 != null) ||
                     (l1 != null && l2 == null) ||
                     (!l1.Equals(l2)))
                {
                    return false;
                }
            }
            return true;
        } else
        {
            List<T> list2Copy = new List<T>(list2);
            //Can be done with Dictionary without O(n^2)
            for (int i = 0; i < list1.Count; i++)
            {
                if (!list2Copy.Remove(list1[i]))
                    return false;
            }
            return true;
        }
    }
}
public class MyType
{
    public int index = 0;
    public string name = "";
    public bool bubi = false;
    public bool piki = false;
    public bool chervi = false;
    public bool kresti = false;
    public bool box = false;
    public bool red = false;
    public MyType(int index = 0, string name = "", bool bubi=false, bool piki=false, bool chervi=false, bool kresti=false, bool box = false)
    {
        this.index = index;
        this.name = name;
        this.bubi = bubi;
        this.piki = piki;
        this.chervi = chervi;
        this.kresti = kresti;
        this.box = box;
        this.red = bubi || chervi;
    }
}

