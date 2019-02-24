namespace Roadrunner.Types
{
    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private Position()
        {}

        public static Position Create(int x, int y)
        {
            return new Position
            {
                X = x,
                Y = y
            };
        }        
    }
}
