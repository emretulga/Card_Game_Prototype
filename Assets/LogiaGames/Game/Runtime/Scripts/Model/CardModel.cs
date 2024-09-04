namespace LogiaGames.Game.Runtime.Model
{
    public class CardModel
    {
        public int TypeID;
        public int ListId;
        public bool IsRevealing = false;
        public bool IsRemoved = false;

        public CardModel(int typeId)
        {
            TypeID = typeId;
        }
    }
}