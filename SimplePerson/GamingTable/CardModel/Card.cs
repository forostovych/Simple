namespace Simple.CardTableModel.CardModel
{
    public class Card
    {
        public Suits Suit { get; set; }
        public Ranks Rank { get; set; }

        public Card(Ranks ranks, Suits suits)
        {
            Rank = ranks;
            Suit = suits;
        }
        public Card() {}
    }

    public enum Suits
    {
        Hearts,                     // Чирва    '♥'     U+2665
        Diamonds,                   // Бубна    '♦'     U+2666
        Clubs,                      // Трефа    '♣'     U+2663
        Spades,                      // Пика    '♠'     U+2660 
        //Joker
    }

    public enum Ranks               // 12 Ranks and Joker
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        J,
        Q,
        K,
        A,
        //Joker
    }
}
