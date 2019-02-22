using System;
using System.Collections.Generic;
using System.Text;

namespace Roadrunner.DriverClient
{
    public class TripRequestAnswer
    {
        private TripRequestAnswer()
        {}

        public static TripRequestAnswer Ok()
        {
            return new TripRequestAnswer
            {
                Answer = TripRequestAnswers.Ok
            };
        }

        public TripRequestAnswers Answer { get; private set; }


        public enum TripRequestAnswers
        {
            Ok
        }
    }
}
