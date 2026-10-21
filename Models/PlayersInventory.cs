using System;

[Serializable]
public class PlayersInventory
{
    public PlayerInventory playerInventory;
}

[Serializable]
public struct PlayerInventory
{
    public int wood;
    public int leaf;
    public int plastic;
    public int stone;
    public int iron;
    public int cannon;

    public int this[int index]
    {
        get => index switch
        {
            0 => wood,
            1 => leaf,
            2 => plastic,
            3 => stone,
            4 => iron,
            5 => cannon,
            _ => throw new IndexOutOfRangeException()
        };
        set
        {
            switch(index)
            {
                case 0:
                    wood = value;
                    break;
                case 1:
                    leaf = value;
                    break;
                case 2:
                    plastic = value;
                    break;
                case 3:
                    stone = value;
                    break;
                case 4:
                    iron = value;
                    break;
                case 5:
                    cannon = value;
                    break;
            }
        }
    }
}