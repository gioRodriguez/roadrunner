using System;
using System.Collections.Generic;
using System.Text;

namespace Roadrunner.DriverClient
{
    public class TripRequestAnswer
    {
        private TripRequestAnswer()
        {}

        public static TripRequestAnswer Accepted()
        {
            return new TripRequestAnswer
            {
                Answer = TripRequestAnswers.Accepted
            };
        }

        public TripRequestAnswers Answer { get; private set; }


        public enum TripRequestAnswers
        {
            Accepted
        }
    }
}
