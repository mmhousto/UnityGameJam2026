using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCat : MonoBehaviour, IInteractable
{
    public bool IsInteractable { get; set; }

    public GameObject shopUI;
    public GameObject shopContent;
    public GameObject shopItem;
    public GameObject brokeMenu;
    public TextMeshProUGUI playersNipCoins;
    public TextMeshProUGUI costUI;
    public Button buyButton;
    public Button exitButton;
    private PlayingCard selectedCard;

    public void Start()
    {
        foreach (PlayingCard card in CardManager.Instance.uniqueCards)
        {
            GameObject newCard = Instantiate(shopItem, shopContent.transform);
            Card theCard = newCard.GetComponent<Card>();
            theCard.SetUpCard(card);
            newCard.GetComponent<Button>().onClick.AddListener(() => SelectCard(theCard));
        }

        costUI.text = $"Select a card to see the cost";
        playersNipCoins.text = $"Nip Coin: {Player.Instance.nipCoin}";

        buyButton.onClick.AddListener(BuyCard);
        exitButton.onClick.AddListener(HideShopUI);
    }

    private void Update()
    {
        if(playersNipCoins.text != $"Nip Coin: {Player.Instance.nipCoin}")
            playersNipCoins.text = $"Nip Coin: {Player.Instance.nipCoin}";
    }

    public void Interact()
    {
        shopUI.SetActive(true);
    }

    public void HideShopUI()
    {
        shopUI.SetActive(false);
    }

    public void SelectCard(Card cardSelected)
    {
        selectedCard = cardSelected.GetPlayingCard();
        costUI.text = $"Cost: {cardSelected.GetPlayingCard().costToBuy} Nip Coin";
    }

    public void BuyCard()
    {
        if(selectedCard == null) return;

        if(Player.Instance.nipCoin >= selectedCard.costToBuy)
        {
            Player.Instance.nipCoin -= selectedCard.costToBuy;
            CardManager.Instance.AddNewCard(selectedCard);
        }
        else
        {
            // NOT ENOUGH COINS ERROR MESSAGE
            brokeMenu.SetActive(true);
        }
    }

}
