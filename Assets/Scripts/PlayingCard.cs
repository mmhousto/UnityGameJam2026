public enum CardType { Attack, Block, Heal }

public class PlayingCard
{
    
    public CardType type;
    public string cardName;
    public int damage;
    public int stamina;
    public int block;
    public int luckyNumber;
    public int costToBuy;

    public PlayingCard(CardType cardType, string nameOfCard, int dmg, int stm, int blck, int luckyNum, int cost)
    {
        type = cardType;
        cardName = nameOfCard;
        damage = dmg;
        stamina = stm;
        block = blck;
        luckyNumber = luckyNum;
        costToBuy = cost;
    }

}
