using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public AudioClip trr;

    int currentSortOrder;
    int place_index;
    int thisChildCount;
    float LastPosition_z;

    bool MouseDown = false;
    Vector3 lastPositionThisGo;

    GameObject place_deck;
    GameObject thisPlaceCards;
    GameObject thisPlaceFinal;
    GameObject father;
    GameObject deck;

    Transform lastParent;

    Transform finalParent1;
    Transform finalParent2;
    Transform finalParent3;
    Transform finalParent4;

    Transform lastThis;

    bool cardsOrPlace;
    bool backFace = false;
    public bool lear_bubi;
    public bool lear_piki;
    public bool lear_chervi;
    public bool lear_trefy;

    public bool mouseStop;
    public bool thisDeckCards;
    public bool red;
    public Sprite spriteRubashka;
    Sprite sprite_test;
    public GameObject place;
    public GameObject place_deck_tuz;

    GameObject camer;
    List<GameObject> placeNumber = new List<GameObject>();
    List<GameObject> cards_isTrigger_true = new List<GameObject>();
    List<GameObject> place_isTrigger_true = new List<GameObject>();
    List<Sprite> sprites = new List<Sprite>();

    Stack<Transform> finalPlace1 = new Stack<Transform>();
    Stack<Transform> finalPlace2 = new Stack<Transform>();
    Stack<Transform> finalPlace3 = new Stack<Transform>();
    Stack<Transform> finalPlace4 = new Stack<Transform>();

    Stack<Transform> placeDeck;
    Stack<Transform> placeDeckNext;

    public Stack<bool> ChangeBackFace;
    public Stack<Transform> BackMoveParents;
    public Stack<int> BackMoveName;
    public Stack<Transform> BackMoveCurrentParents;
    public Stack<Transform> thisCard;
    public Stack<Vector3> lastPosition;
    public Stack<bool> comeBackDeck;
    public Stack<bool> changeRubashkaDeckCards;
    StartCreateCards колода;
    StaticValues staticvalues;

    bool comeBackDeckBool = false;
    bool changeRubashkaDeckCardsBool = false;
    //move moved;

    private void Start()
    {
        camer = GameObject.Find("Camera");
        deck = GameObject.Find("колода").gameObject;
        place_deck = GameObject.Find("place_deck").gameObject;
        place = GameObject.Find("place").gameObject;
        
        staticvalues = camer.GetComponent<StaticValues>();
        колода = deck.GetComponent<StartCreateCards>();

        finalPlace1 = колода.finalPlace1;
        finalPlace2 = колода.finalPlace2;
        finalPlace3 = колода.finalPlace3;
        finalPlace4 = колода.finalPlace4;

        placeDeckNext = колода.PlaceDeckNext;
        placeDeck = колода.PlaceDeck;

        BackMoveParents = staticvalues.BackMoveParents;
        BackMoveName = staticvalues.BackMoveName;
        BackMoveCurrentParents = staticvalues.BackMoveCurrentParents;
        ChangeBackFace = staticvalues.ChangeBackFace;
        comeBackDeck = staticvalues.comeBackDeck;
        changeRubashkaDeckCards = staticvalues.changeRubashkaDeckCards;
        thisCard = staticvalues.thisCard;
        lastPosition = staticvalues.lastPosition;
        place_deck_tuz = GameObject.Find("GameObject").gameObject;//место под складывания колод

        finalParent1 = place_deck_tuz.transform.GetChild(0);
        finalParent2 = place_deck_tuz.transform.GetChild(1);
        finalParent3 = place_deck_tuz.transform.GetChild(2);
        finalParent4 = place_deck_tuz.transform.GetChild(3);


        sprites = колода.sprites;
        place = колода.place.gameObject;
    
        ChildrenTrueCardsDeck(place_deck);
    }
    bool parentFinalPlace()
    {
        if (this.transform.parent.gameObject.name == "туз1" || this.transform.parent.gameObject.name == "туз2" || this.transform.parent.gameObject.name == "туз3" || this.transform.parent.gameObject.name == "туз4")
        {
            return true;
        } else
        {
            return false;
        }
    }
    void iinitialization()
    {
        lastPositionThisGo = this.transform.position;//текущая позиция карты, нужна если потребуется вернуть карту на место.
        LastPosition_z = lastPositionThisGo.z;
    }
    void PutMove()
    {
        Debug.Log(backFace);
        Debug.Log("почему false?");
        ChangeBackFace.Push(backFace);
        BackMoveParents.Push(lastParent.transform);
        thisCard.Push(this.transform);
        lastPosition.Push(lastPositionThisGo);
        Debug.Log(comeBackDeckBool);
        comeBackDeck.Push(comeBackDeckBool);
        changeRubashkaDeckCards.Push(changeRubashkaDeckCardsBool);
      
    }
    void OnMouseDown()//зажал лкм
    {

       
        iinitialization();//сохраняем состояние карт, для возврата к их исходному состоянию.
        if (!thisDeckCards)//карта не относится к колоде(та что справа, все остальные карты не разложенные, ну ты понял)
        {
            camer.GetComponent<sound1>().take();
            cardsIsTriggerAddArray();//определяем на какие карты можно положить карту, создаём массив этих карт.
            SaveLastValue(); //Сохраняем состояние карт перед ходом.

            if (!parentFinalPlace() && this.transform.parent.gameObject.name != "place_deck")
            {
                SearchFatherThisCard();//father присваиваем значение(это предыдущая карта, от той которую мы взяли)
                sortingOrderChildren();//sortingOrder++ изменение родителя на текущую карту.
            }//только для рабочих карт, 7 столбцов.

           // if (mouseStop)
            //{
                MouseDown = true;
            //}//для того что бы в update можно было тоскать карту при зажатой клавише.
        }
    }
    void OnMouseUp()//отпустил лкм
    {
        camer.GetComponent<sound1>().put();
        MouseDown = false;//что бы карта больше не следовала за курсором.

        if (thisDeckCards)
        {
            changeFaceCard();//берем из колоды карту, меняем ей рубашку и родителя.
        } else
        {
            if (checkPositionCards())//сверка коардинат, попали ли мы на карту или нет.
            {
                CheckNumberCards();//проверка достоинства карт и места, и соответсвующие действия
            } else
            {
                if (this.transform.parent.gameObject.name == "place_deck" || parentFinalPlace())//простое действие для одиночных карт.
                {
                    this.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    this.transform.position = new Vector3(lastPositionThisGo.x, lastPositionThisGo.y, LastPosition_z);
                } else
                {
                    CardCameBack();//если карту не положили, и она вернулась обратно.
                }
            }
        }
    }

    void SearchFatherThisCard()
    {
        if (IndexThisInPlace(this.gameObject) >= 1)
        {
            Transform thisParent = this.transform.parent;
            for (int i = 0; i < thisParent.childCount; i++)
            {
                if (thisParent.GetChild(i).name == this.name)
                {
                    father = thisParent.GetChild(i - 1).gameObject;
                    Debug.Log("blya " + father);
                }
            }
        } else
        {
            father = this.transform.parent.gameObject;
        }
    }
    bool ChildrnDoNotGo(GameObject a, GameObject b)
    {
        if (b.transform.childCount > 0)
        {
            if (b.transform.GetChild(0).gameObject == a)
            {
                return false;
            }

            if (ChildrnDoNotGo(a, b.transform.GetChild(0).gameObject) == false)
            {
                return false;
            }
        }
        return true;
    }

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
    int IndexThisInPlace(GameObject thisGo)
    {
        string s = thisGo.name;
        Transform thisParent = this.transform.parent;
        int Index = thisParent.childCount;
        for (int i = Index - 1; i >= 0; i--)
        {
            if (s == thisParent.GetChild(i).name)
            {
                return i;
            }
        }
        Debug.Log("произошла ошибка в методе IndexThisInPlace " + s);
        return -1;
    }

    void ChildrenTrueCardsDeck(GameObject go)
    {
        Transform goTransform = go.transform;
        if (goTransform.childCount > 0)
        {
            goTransform.GetChild(0).GetComponent<move>().thisDeckCards = true;

            if (goTransform.GetChild(0).childCount > 0)
            {
                ChildrenTrueCardsDeck(goTransform.GetChild(0).gameObject);
            }
        }

    }

    void sortingOrderChildren()//повышение иерархии всем детям.
    {
        int x = 50;
        this.transform.GetComponent<SpriteRenderer>().sortingOrder = x;

        //List<Transform> place = researchArray(this.gameObject);
        int index = IndexThisInPlace(this.gameObject);
        Transform thisParent = this.transform.parent;
        int childCount = thisParent.childCount;

        for (int i = index + 1; i < thisParent.childCount;)//когда мы меняем родителя карты, то childcount уменьшается на единицу, следовательно i++ здесь не нужен
        {
            Debug.Log(i + " < " + childCount);
            Debug.Log(i + " " + thisParent.GetChild(i).name);
            thisParent.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = x + i;
            thisParent.GetChild(i).parent = this.transform;
        }

    }
    void BacksortingOrderChildren()
    {
        int x = 2;
        Transform child = this.transform;

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, LastPosition_z);
        child.GetComponent<SpriteRenderer>().sortingOrder = x;
        child.position = new Vector3(child.position.x, child.position.y, LastPosition_z);

        child.parent = lastParent.transform;
        for (int i = 0; ; i++)
        {
            LastPosition_z--;
            if (child.childCount > 0)
            {
                Transform child1 = this.transform.GetChild(0);
                child1.GetComponent<SpriteRenderer>().sortingOrder = x;
                child1.parent = lastParent.transform;

                child1.position = new Vector3(child1.position.x, child1.position.y, LastPosition_z);
            } else
            {
                break;
            }
        }
    }
    GameObject reserchNumberPlace(int a)
    {
        for (int i = 0; i < place.transform.childCount; i++)
        {
            if (a.ToString() == place.transform.GetChild(i).name)
            {
                return place.transform.GetChild(i).gameObject;
            }
        }
        Debug.Log("ошибка " + a);
        return this.gameObject;
    }

    void IsTriggerAdd(GameObject place)
    {
        string IsNotChekIsTrigger = this.transform.parent.name;
        for (int i = 0; i < place.transform.childCount; i++)
        {
            if (place.transform.GetChild(i).name == IsNotChekIsTrigger)
            {
                continue;
            }
            if (place.transform.GetChild(i).childCount == 0)
            {
                cards_isTrigger_true.Add(place.transform.GetChild(i).gameObject);
                Debug.Log("isTrigger = " + place.transform.GetChild(i).name);
            } else
            {
                Transform parentChild = place.transform.GetChild(i);
                for (int j = 0; j < parentChild.childCount; j++)
                {
                    if (this.name != parentChild.GetChild(j).name && parentChild.GetChild(j).GetComponent<BoxCollider2D>().isTrigger)
                    {
                        //Debug.Log("isTrigger = " + parentChild.GetChild(j).name);
                        cards_isTrigger_true.Add(parentChild.GetChild(j).gameObject);
                    }
                }
            }
        }
    }

    void cardsIsTriggerAddArray()
    {
        cards_isTrigger_true.Clear();
        place_isTrigger_true.Clear();

        int a;
        if (!parentFinalPlace() && this.transform.parent.gameObject.name != "place_deck")
        {
            a = fatherPlaceNumber(this.transform.parent.gameObject);
        } else
        {
            a = 0;
        }

        IsTriggerAdd(place);
        if (this.transform.parent != finalParent1)
        {
            if (finalParent1.childCount == 0)
            {
                place_isTrigger_true.Add(finalParent1.gameObject);
            } else
            {
                place_isTrigger_true.Add(finalPlace1.Peek().gameObject);
            }
        }

        if (this.transform.parent != finalParent2)
        {
            if (finalParent2.childCount == 0)
            {
                place_isTrigger_true.Add(finalParent2.gameObject);
            } else
            {
                place_isTrigger_true.Add(finalPlace2.Peek().gameObject);
            }
        }

        if (this.transform.parent != finalParent3)
        {
            if (finalParent3.childCount == 0)
            {
                place_isTrigger_true.Add(finalParent3.gameObject);
            } else
            {
                place_isTrigger_true.Add(finalPlace3.Peek().gameObject);
            }
        }

        if (this.transform.parent != finalParent4)
        {
            if (finalParent4.childCount == 0)
            {
                place_isTrigger_true.Add(finalParent4.gameObject);
            } else
            {
                place_isTrigger_true.Add(finalPlace4.Peek().gameObject);
            }
        }

    }//создание массива из карт на которые можно положить текущую карту.

    void deckCardsExchangePut()
    {
        if (this.transform.parent)
        {
            mouseStop = true;
            thisDeckCards = false;
            int placeDeckCount = place_deck.transform.childCount;
            if (placeDeckCount > 0)
            {
                Debug.Log(placeDeckNext.Peek().transform.position.z + " " + placeDeckNext.Peek().name);
                this.transform.position = new Vector3(place_deck.transform.position.x, place_deck.transform.position.y, place_deck.transform.GetChild(place_deck.transform.childCount-1).position.z - 1);
                
            } else
            {
                this.transform.position = new Vector3(place_deck.transform.position.x, place_deck.transform.position.y, -1);
            }
            this.transform.parent = place_deck.transform;
            placeDeckNext.Push(placeDeck.Pop());
            if (placeDeck.Count > 0)
            {
                placeDeck.Peek().GetComponent<BoxCollider2D>().enabled = true;
                if (placeDeckCount == 1)
                {
                    deck.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            father = this.gameObject;
            Debug.Log("here?");
            ParentThisCardExchangeSprite();
        }

    }//вынимание карт из колоды

    bool checkPositionCards()
    {
        for (int i = 0; i < cards_isTrigger_true.Count; i++)
        {

            float x = this.transform.position.x;
            float y = this.transform.position.y;

            float x_trigger = cards_isTrigger_true[i].transform.position.x;
            float y_trigger = cards_isTrigger_true[i].transform.position.y;
            if (abs(x - x_trigger) < 1 && abs(y - y_trigger) < 1.2)
            {
                Debug.Log(cards_isTrigger_true[i].name + "урa?");
                thisPlaceCards = cards_isTrigger_true[i];
                cardsOrPlace = true;
                place_index = thisPlaceCards.GetComponent<Index>().index;
                Debug.Log(place_index + "#1" + thisPlaceCards.name);
                return true;
            }
        }
        for (int i = 0; i < place_isTrigger_true.Count; i++)
        {
            float x = this.transform.position.x;
            float y = this.transform.position.y;

            float x_trigger1 = place_isTrigger_true[i].transform.position.x;
            float y_trigger1 = place_isTrigger_true[i].transform.position.y;
            //   Debug.Log(place_isTrigger_true[i] + "place_isTrigger");

            if (abs(x - x_trigger1) < 1 && abs(y - y_trigger1) < 1.2)
            {
                Debug.Log(place_isTrigger_true[i].name + "урa?");
                thisPlaceFinal = place_isTrigger_true[i];
                place_index = thisPlaceFinal.GetComponent<Index>().index;
                Debug.Log(place_index + "#2");
                cardsOrPlace = false;
                return true;
            }
        }
        return false;
    }//Проверка позиции текущей карты с коардинатами карт из массива triggers
    void FinalStackAdd()
    {
        bool a = this.transform.parent.gameObject.name == "place_deck";
        GameObject thisPlaceHere;
        if (thisPlaceFinal.name == "туз1" || thisPlaceFinal.name == "туз2" || thisPlaceFinal.name == "туз3" || thisPlaceFinal.name == "туз4")
        {
            thisPlaceHere = thisPlaceFinal;
        } else
        {
            thisPlaceHere = thisPlaceFinal.transform.parent.gameObject;
        }

        if (thisPlaceHere.name == finalParent1.name)
        {
            if (a)
            {
                finalPlace1.Push(placeDeckNext.Pop());
            } else
            {
                finalPlace1.Push(this.transform);
            }

            this.transform.parent = finalParent1;
        }
        if (thisPlaceHere.name == finalParent2.name)
        {
            if (a)
            {
                finalPlace2.Push(placeDeckNext.Pop());
            } else
            {
                finalPlace2.Push(this.transform);
            }
            this.transform.parent = finalParent2;
        }
        if (thisPlaceHere.name == finalParent3.name)
        {
            finalPlace3.Push(this.transform);
            if (a)
            {
                finalPlace3.Push(placeDeckNext.Pop());
            } else
            {
                finalPlace3.Push(this.transform);
            }
            this.transform.parent = finalParent3;
        }
        if (thisPlaceHere.name == finalParent4.name)
        {
            if (a)
            {
                finalPlace4.Push(placeDeckNext.Pop());
            } else
            {
                finalPlace4.Push(this.transform);
            }
            this.transform.parent = finalParent4;
        }
    }

    Stack<Transform> reserchStack()
    {
        string s = this.transform.parent.gameObject.name;
        if (s == "туз1")
        {
            return finalPlace1;
        }
        if (s == "туз2")
        {
            return finalPlace2;
        }
        if (s == "туз3")
        {
            return finalPlace3;
        }
        if (s == "туз4")
        {
            return finalPlace4;
        }
        Debug.Log("ошибка");
        return finalPlace4;
    }

    void CheckNumberCards()//проверка достоинста карты, и достоинства места.
    {
        if (!cardsOrPlace)//finalPlace
        {
            Debug.Log(this.transform.childCount + " " + this.name);
            if (this.transform.childCount == 0)
            {
                bool mast = rightMast(); //определяем подходит ли масть. true or false

                if (mast && this.GetComponent<Index>().index - 1 == place_index)
                {

                    FinalStackAdd();

                    this.GetComponent<SpriteRenderer>().sortingOrder = 2;

                    father.GetComponent<BoxCollider2D>().enabled = true;
                    father.GetComponent<BoxCollider2D>().isTrigger = true;
                    Debug.Log("Отработало " + father.name + "=======================================================================");
                    if (thisPlaceFinal.name == "GameObject")
                    {
                        this.transform.position = new Vector3(thisPlaceFinal.transform.position.x, thisPlaceFinal.transform.position.y, thisPlaceFinal.transform.position.z - 1);
                    } else
                    {
                        this.transform.position = new Vector3(thisPlaceFinal.transform.position.x, thisPlaceFinal.transform.position.y, thisPlaceFinal.transform.position.z - 1);
                    }
                    if (father.GetComponent<SpriteRenderer>().sprite.name == spriteRubashka.name)
                    {
                        Debug.Log("here");
                        ParentThisCardExchangeSprite();
                    } else
                    {
                        PutMove();
                    }
                } else
                {
                    this.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    this.transform.position = new Vector3(lastPositionThisGo.x, lastPositionThisGo.y, LastPosition_z);
                }
            } else
            {
                CardCameBack();//возвращаем потому что, карта должна быть одна.
            }
        } else
        {
            Transform timeHellpValue = this.transform.parent;

            bool mast;
            Debug.Log(this.GetComponent<Index>().index + " " + place_index);
            if (place_index != 14)
            {
                mast = thisPlaceCards.GetComponent<move>().red != red;//red это true или false нашей карты, взависимости красная она или нет. 
            } else
            {
                mast = true;//для королей не важно какого они цвета
            }

           // Debug.Log("TUT? " + " this.name=" + this.name + " value_cards = " + this.GetComponent<Index>().index + " localparam = " + place_index + "mast=" + mast);
            if (mast && this.GetComponent<Index>().index + 1 == place_index)
            {   
                thisPlaceCards.GetComponent<BoxCollider2D>().isTrigger = false;
                
                //действия если из колоды, или из финальных колод
                if (this.transform.parent.gameObject.name == "place_deck")
                {
                    placeDeckNext.Pop();
                } else
                {

                    if (this.transform.parent.parent && this.transform.parent.parent.name == "GameObject")// из финальных колод, в игровое поле. Удаляем выбранную карту, и добавлем её в нужную колоду.
                    {


                        if (this.transform.parent.name == "туз1")
                        {
                            finalPlace1.Pop();
                        }
                        if (this.transform.parent.name == "туз2")
                        {
                            finalPlace2.Pop();
                        }
                        if (this.transform.parent.name == "туз3")
                        {
                            finalPlace3.Pop();
                        }
                        if (this.transform.parent.name == "туз4")
                        {
                            finalPlace4.Pop();
                        }
                    }
                }

               
                if (this.GetComponent<Index>().index == 13)
                {
                    //если король
                    this.transform.position = new Vector3(thisPlaceCards.transform.position.x, thisPlaceCards.transform.position.y, thisPlaceCards.transform.transform.parent.position.z - 1);

                } else
                {
                    //не  король
                    this.transform.position = new Vector3(thisPlaceCards.transform.position.x, thisPlaceCards.transform.position.y - 0.5f, thisPlaceCards.transform.transform.parent.position.z - 1);
                }

                this.GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (this.transform.childCount > 0)
                {
                    for (int i = 0; i < this.transform.childCount; i++)
                    {
                        this.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                }


                GOtoParentPlace();
                father.GetComponent<BoxCollider2D>().enabled = true;
                father.GetComponent<BoxCollider2D>().isTrigger = true;
                Debug.Log("Отработало " + father.name + "=======================================================================");

                if (father.GetComponent<SpriteRenderer>().sprite.name == spriteRubashka.name)
                {
                    Debug.Log("here");
                    ParentThisCardExchangeSprite();
                } else
                {
                    PutMove();
                }

            } else
            {
                if (this.transform.parent.parent && this.transform.parent.parent.name == "GameObject")
                {
                    this.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    this.transform.position = new Vector3(lastPositionThisGo.x, lastPositionThisGo.y, LastPosition_z);
                } else
                {
                    CardCameBack();

                }
            }
        }
    }
    bool rightMast()
    {
        if (place_index != 0)
        {
            return (thisPlaceFinal.GetComponent<move>().lear_bubi && lear_bubi) || (thisPlaceFinal.GetComponent<move>().lear_piki && lear_piki) || (thisPlaceFinal.GetComponent<move>().lear_chervi && lear_chervi) || (thisPlaceFinal.GetComponent<move>().lear_trefy && lear_trefy);
        } else
        {
            return true;
        }
    }
    void GOtoParentPlace()
    {
        List<Transform> list = new List<Transform>();
        Transform parent = thisPlaceCards.transform.parent;
        if (thisPlaceCards.transform.parent.gameObject.name == "place")
        {
            parent = thisPlaceCards.transform;
        }
        float zz = thisPlaceCards.transform.position.z;
        list.Add(this.transform);
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            list.Add(child);
        }

        for (int i = 0; i < list.Count; i++)
        {
            zz--;
            list[i].parent = parent;
            list[i].transform.position = new Vector3(list[i].transform.position.x, list[i].transform.position.y, zz);
        }
    }
    void ParentThisCardExchangeSprite()
    {
     
        for (int i = 0; i < sprites.Count; i++)
        {

            if (sprites[i].name + "(Clone)" == father.name)
            {
                Transform ThisParent = father.transform.parent;
                Debug.Log("глупость");
                backFace = true;
                father.transform.parent = null;
                father.transform.parent = ThisParent;
                father.GetComponent<SpriteRenderer>().sprite = sprites[i];
                father.GetComponent<BoxCollider2D>().enabled = true;
                // father.GetComponent<Rigidbody2D>().simulated = true;
                break;
            }
        }
        PutMove();
    }
    void CardCameBack()
    {
        this.transform.position = lastPositionThisGo;

        BacksortingOrderChildren();

    }
    void ChildrenSortingOrder(Transform GO)
    {

        if (GO.childCount > 0)
        {
            Transform help = GO.GetChild(0);
            help.transform.position = new Vector3(help.transform.position.x, help.transform.position.y, GO.transform.parent.position.z - 1);
            // help.gameObject.GetComponent<SpriteRenderer>().sortingOrder = GO.GetComponent<SpriteRenderer>().sortingOrder + 1;
            if (GO.GetChild(0).transform.childCount > 0)
            {
                ChildrenSortingOrder(GO.GetChild(0));
            }
        }

    }
    void Update()
    {
        if (MouseDown)
        {
            Vector3 Cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Cursor.z = -20;
            this.transform.position = Cursor;
        }
    }
    void SaveLastValue()
    {
        lastParent = this.transform.parent;
        lastPositionThisGo = this.transform.position;//текущая позиция карты, нужна если потребуется вернуть карту на место.
        LastPosition_z = lastPositionThisGo.z;
    }
    void changeFaceCard()
    {
        lastParent = колода.transform;
        
        if (placeDeck.Count > 0)
        {
            comeBackDeckBool = true;
            deckCardsExchangePut();
        } else //если колода закончилась, возврощаем карты
        {
            deck.GetComponent<BoxCollider2D>().enabled = false;
            int z_count = -1;
            for (; 0 < placeDeckNext.Count;)
            {

                placeDeckNext.Peek().GetComponent<move>().thisDeckCards = true;
                //lastChildDeck.GetComponentGetComponent<move>().thisDeckCards = true;
                placeDeckNext.Peek().transform.parent = deck.transform;
               // lastChildDeck.parent = deck.transform;
                placeDeckNext.Peek().position = new Vector3(deck.transform.position.x, deck.transform.position.y, z_count);
                //lastChildDeck.position = new Vector3(deck.transform.position.x, deck.transform.position.y, z_count);
                placeDeckNext.Peek().GetComponent<SpriteRenderer>().sprite = spriteRubashka;
                //lastChildDeck.GetComponent<SpriteRenderer>().sprite = spriteRubashka;
                placeDeck.Push(placeDeckNext.Pop());
                z_count--;
            }
            //placeDeck.Peek().GetComponent<BoxCollider2D>().enabled = true;
            changeRubashkaDeckCardsBool = true;
        }
    }
    float abs(float a)
    {
        if (a > 0)
        {
            return a;
        } else
        {
            return a * (-1);
        }
    }
}
