
[System.Serializable]
public class Item 
{
   public static int TYPEPFOOD = 1;
   public const int TYPEPLOW = 2;

   public string name;
   public string imgUrl;
   public int type;
   public int count;
   public int price;
   public int lvlWhenUnlock;
   public float timeToGrow;
   public int durability;

   public Item(string name, string imgUrl, int count, int type, int price, int lvlWhenUnlock, float timeToGrow, int durability)
   {
    this.name = name;
    this.imgUrl = imgUrl;
    this.count = count;
    this.type = type;
    this.price = price;
    this.lvlWhenUnlock = lvlWhenUnlock;
    this.timeToGrow = timeToGrow;
    this.durability = durability;
   }
}
