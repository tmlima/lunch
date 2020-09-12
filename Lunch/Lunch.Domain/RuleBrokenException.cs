using System;

namespace Lunch.Domain
{
    public class RuleBrokenException : Exception
    {
        public RuleBrokenException(string message) : base(message) { }
    }
}
