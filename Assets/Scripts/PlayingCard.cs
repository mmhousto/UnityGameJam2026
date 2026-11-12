using UnityEngine.Android;

public enum CardType { Attack, Block, Heal }

public class PlayingCard
{
    
    public CardType type;
    public string cardName;
    public string chineseName;
    public string info;
    public int damage;
    public int stamina;
    public int block;
    public int luckyNumber;
    public int costToBuy;

    public PlayingCard(CardType cardType, string nameOfCard, string chinese, int dmg, int stm, int blck, int luckyNum, int cost, string cardInfo)
    {
        type = cardType;
        cardName = nameOfCard;
        chineseName = chinese;
        damage = dmg;
        stamina = stm;
        block = blck;
        luckyNumber = luckyNum;
        costToBuy = cost;
        info = cardInfo;
    }

}
