namespace DungeonCrawl
{
    public class AdjecentCoordinates
    {
        private readonly (int x, int y) _position;
        public AdjecentCoordinates((int x, int y) position)
        {
            _position = position;
        }

        public (int, int)[] GetAdjecentCoordinates()
        {
            var coords = new (int, int)[4];
            var directions = Direction.GetValues(typeof(Direction));

            foreach (Direction direction in directions)
            {
                coords[(int)direction] = (_position.x + direction.ToVector().x, _position.y + direction.ToVector().y);
            }

            return coords;
        }
    }
}
