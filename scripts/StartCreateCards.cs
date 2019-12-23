using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartCreateCards : MonoBehaviour
{
    int count = 0;//счетчик карт.

    public Transform place; // родитель мест под карты, через него мы выходим на них.
    public Transform buby;
    public Transform piki;
    public Transform черви;
    public Transform трефы;

    public Sprite sprite; // рубашка.
    public Transform deck;//Родитель для всех карт оствашихся после раскладки.

    public Stack<Transform> PlaceDeck = new Stack<Transform>(); //стэк карт для колоды.
    public Stack<Transform> PlaceDeckNext = new Stack<Transform>();//стек карт колоды, куда перекладываются карты, после открытия рубашки в основной колоде.
    //переменные для складывания карт по масти. туз, двойка, тройка....король. Выиграш.
    public Stack<Transform> finalPlace1 = new Stack<Transform>();
    public Stack<Transform> finalPlace2 = new Stack<Transform>();
    public Stack<Transform> finalPlace3 = new Stack<Transform>();
    public Stack<Transform> finalPlace4 = new Stack<Transform>();
    //при смене спрайта карты на рубашку, сюда записываются их лицевые спрайты, что бы потом их найти. Можно заменить на HashMap, или создать класс для создания объекта карт.
    public List<Sprite> sprites = new List<Sprite>();

    GameObject CurrentCard; // текущая карта
    Transform CurrentCardTransform; // текущая карта Transform, что бы иметь доступ к коардинатам.
    Transform placeForCard; // Объкт на сцене, на котором лежат карты.

    List<Transform> AllCards = new List<Transform>(); 
    List<GameObject> places = new List<GameObject>();

    //В Unity3D запускается автоматически при запуске игры.
    void Start()
    {
        //объединяю карты в одну коллекцию, что бы их перемешать, и разложить в случайном порядке.
        createCard(buby.transform);
        createCard(piki.transform);
        createCard(черви.transform);
        createCard(трефы.transform);
        //перемешиваю карты случайным образом.
        randomCards(AllCards);
      
        layOutCards();
        // все оставшиеся карты кладем в колоду.
        layOutCardsToDeck();
    }
    // Создаем коллекцию всех карт, что бы их можно было перемешать.
    private void createCard(Transform suit){
        for (int j = 0; j < suit.childCount; j++)
        {
            AllCards.Add(suit.GetChild(j)); 
        }
    }
    // переворачиваем карту рубашкой вверх.
    private void exchangeSprite()
    {
            sprites.Add(CurrentCard.GetComponent<SpriteRenderer>().sprite);
            CurrentCard.GetComponent<SpriteRenderer>().sprite = sprite;
            CurrentCard.GetComponent<BoxCollider2D>().enabled = false;
    }
    private void layOutCardsToDeck(){
        int count_z = 0;// Коардината z на сцене, для слоёв.
        for (int i = count; i < AllCards.Count; i++)
        {
            CurrentCard = Instantiate(AllCards[i].gameObject, new Vector3(deck.position.x, deck.position.y, count_z), Quaternion.identity);// создаём объкт на сцене.
            CurrentCardTransform = CurrentCard.transform;

            CurrentCardTransform.parent = deck;// присваевам всем картам общего родителя.
            PlaceDeck.Push(CurrentCardTransform);// заполняем стек.
            sprites.Add(CurrentCard.GetComponent<SpriteRenderer>().sprite);// Запоминаем какие спрайты нам понадобятся для открытия рубашек.
            CurrentCard.GetComponent<SpriteRenderer>().sprite = sprite;// закрываем карту рубашкой вверх.
            CurrentCardTransform.GetComponent<move>().thisDeckCards = true; // текущая карта принадлежит к колоде.
            count_z--; // меняем слой.
        }
        CurrentCard.GetComponent<BoxCollider2D>().enabled = true; //Последнию карту в колоде делаем кликабельной.
    }
    private void layOutCards(){
       int count_z = 0; //коардината карт z, отвечающая за слои карт. 
       int amountPlace = 7;//места под раскладывание карт. 
       //раскладываем карты по одной на каждое место, затем по одной на шесть мест, на пять...на  одно.
        for (int i = 0; i < amountPlace; i++)
        {
            for (int x = i; x < amountPlace; x++)
            {
                CurrentCard = AllCards[ count ].gameObject;// текущая карта
                placeForCard = place.GetChild(x);// текущее место под карту
                CurrentCard = Instantiate(CurrentCard, new Vector3(placeForCard.position.x, placeForCard.position.y, count_z), Quaternion.identity);//создали карту ,с коардинатами места.
                Transform CurrentCardTransform = CurrentCard.transform;
                CurrentCardTransform.parent = placeForCard;//присвоили текущей карте родителя в качестве места.

                if (i != 0){// Если карты раскладываютя поверх других, то меняим их коардинаты, взависимости от того, сколько карт уже есть.
                    CurrentCardTransform.position = new Vector3(CurrentCardTransform.position.x, CurrentCardTransform.position.y - (0.3f * (placeForCard.childCount - 1)), count_z);
                    Debug.Log(CurrentCardTransform.position.y);
                }
                if(x != i){// Если карта первая в цикле, то в текущем столбце она является последней, следоваетльно рубашка у неё должна быть открыта, для всех остальных карт заркываем рубашку.
                    exchangeSprite();
                }else{
                    //если карта последняя в стобце, то активируем флажки, что бы карту можно было передвигать, и класть на них другие карты.
                    CurrentCard.GetComponent<BoxCollider2D>().isTrigger = true;
                    CurrentCard.GetComponent<BoxCollider2D>().enabled = true;
                }
                count++;// увеличиваем счетчик карт.
            }
            count_z--;// изменили слой, для будущих карт. Что бы карты перекрывали друг друга в правильном порядке.
        }
    }
        // Перемешиваем карты.
    private void randomCards(List<Transform> array)
    {
        for (int i = array.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            // обменять значения array[j] и array[i]
            var temp = array[j];
            array[j] = array[i];
            array[i] = temp;
        }
        //return array;
    }
}