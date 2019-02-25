namespace Roadrunner.Types
{
    public class NewTrip
    {
        public string PassengerId { get; private set; }
        public Position Destiny { get; private set; }
        public Position Origin { get; private set; }

        private NewTrip()
        {}

        public static NewTrip Create(string passengerId, Position origin, Position destiny)
        {
            return new NewTrip
            {
                PassengerId = passengerId,
                Origin = origin,
                Destiny = destiny
            };
        }

    }
}
