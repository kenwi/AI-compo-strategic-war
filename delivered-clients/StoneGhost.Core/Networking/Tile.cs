namespace StoneGhost.Core.Networking
{
    public class Tile
    {
        public int[] position;
        public bool is_wall;
        public bool has_food;

        public Spawner spawner;
        public Unit unit;
    }
}