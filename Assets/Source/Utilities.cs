using System;

namespace DungeonCrawl
{
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class Utilities
    {
        public static (int x, int y) ToVector(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return (0, 1);
                case Direction.Down:
                    return (0, -1);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Right:
                    return (1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public static (int, int)[] GetAdjecentCoordinates((int x, int y) position)
        {
            var coords = new (int, int)[4];
            var directions = Direction.GetValues(typeof(Direction));

            foreach (Direction direction in directions)
                coords[(int)direction] =
                    (position.x + direction.ToVector().x, position.y + direction.ToVector().y);

            return coords;
        }
    }
}
