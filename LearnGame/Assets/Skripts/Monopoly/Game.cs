using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text Win;
    public GameObject Cube;
    public static Game Instance;

    private List<MonopolyPlayer> players;
    private List<MonopolyItem> items;
    private Step currentStepPlayer;
    private Dictionary<MonopolyPlayer, Step> StepsInfo = new Dictionary<MonopolyPlayer, Step>();

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        players = getPlayers();
        currentStepPlayer = new Step(players.Count);
        items = GetComponentsInChildren<MonopolyItem>()
            .ToList();
        for(int i = 0; i < players.Count; i++)
            StepsInfo.Add(players[i], new Step(items.Count));
    }

    private void Update()
    {
        foreach(var player in players)
        {
            var countItems = items.Where(x => x.GetColor() == player.GetColor()).Count();
            var allItems = items.Count;
            var percent = countItems / (float)allItems;
            if(percent >= 0.8f)
            {
                Win.gameObject.SetActive(true);
                Win.text = "Выиграл " + player.GetName() + " !!!";
                Cube.SetActive(false);
            }
        }
    }

    private List<MonopolyPlayer> getPlayers()
    {
        var countPlayer = 3;//PhotonNetwork.CurrentRoom.PlayerCount;
        var allPlayers = GetComponentsInChildren<MonopolyPlayer>()
            .ToList();
        allPlayers
            .Skip(countPlayer)
            .ToList()
            .ForEach(p => p.gameObject.SetActive(false));
        return allPlayers
            .Take(countPlayer)
            .ToList();
    }

    public void MoveStep(int count)
    {
        var player = players[currentStepPlayer.currentStep];
        StartCoroutine(movePlayer(player, count));
        currentStepPlayer.NextStep();
    }

    private IEnumerator movePlayer(MonopolyPlayer player, int countStep)
    {
        var currentStep = StepsInfo[player];
        for (int i = 0; i < countStep; i++)
        {
            currentStep.NextStep();
            SetStep(player, currentStep);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);

        var onStart = items[currentStep.currentStep].GetComponent<MonopolyItem>().OnStart;
        if (onStart)
        {
            currentStep.currentStep = 0;
            SetStep(player, currentStep);
        }
        else
        {
            var monopolyWindow = (MonopolyWindow)Window.Instance;
            monopolyWindow.StartPlay("Monopoly", player);
        }
    }

    private void SetStep(MonopolyPlayer player, Step currentStep)
    {
        var indexItem = currentStep.currentStep;
        var item = items[indexItem];
        var piece = player.GetPiece();
        piece.SetActive(true);
        piece.transform.position = item.transform.position;
    }

    public void RegisterAnswer(MonopolyPlayer player)
    {//вызывается только при правильном ответе
        var indexItem = StepsInfo[player].currentStep;
        var item = items[indexItem];
        var itemColor = item.GetColor();
        var PlayerColor = player.GetColor();
        if (itemColor != item.StartColor)
        {
            if (itemColor != PlayerColor)
            {
                var otherPlayer = players.Where(x => x.GetColor() == itemColor).First();
                otherPlayer.GetScore().Decrement();
                player.GetScore().Increment();
                item.GetComponent<Image>().color = PlayerColor;
            }
        }
        else
        {
            player.GetScore().Increment();
            item.GetComponent<Image>().color = PlayerColor;
        }
    }

    public class Step
    {
        public int currentStep { get; set; }

        private int size;
        public Step(int size)
        {
            this.size = size;
            currentStep = 0;
        }

        public void NextStep()
        {
            currentStep += 1;
            if (currentStep == size) currentStep = 0;
        }
    }
}
