namespace DungeonCrawl
{
    public class AdjecentCoordinates
    {
        private readonly (int, int) _position;
        public AdjecentCoordinates((int x, int y) position)
        {
            _position = position;
        }

        public (int, int)[] GetAdjecentCoordinates()
        {
            return new (int, int)[] 
            { 
                (_position.Item1 + 1, _position.Item2),
                (_position.Item1, _position.Item2 + 1),
                (_position.Item1 - 1, _position.Item2),
                (_position.Item1, _position.Item2 - 1)

            };
        }
    }
}
