//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace Microsoft.Practices.Unity.Utility
{
    /// <summary>
    ///   A helper class that encapsulates two different
    ///   data items together into a a single item.
    /// </summary>
    public class Pair<TFirst, TSecond>
    {
        private readonly TFirst first;
        private readonly TSecond second;

        /// <summary>
        ///   Create a new <see cref = "Pair{TFirst, TSecond}" /> containing
        ///   the two values give.
        /// </summary>
        /// <param name = "first">First value</param>
        /// <param name = "second">Second value</param>
        public Pair(TFirst first, TSecond second)
        {
            this.first = first;
            this.second = second;
        }

        /// <summary>
        ///   The first value of the pair.
        /// </summary>
        public TFirst First
        {
            get { return first; }
        }

        /// <summary>
        ///   The second value of the pair.
        /// </summary>
        public TSecond Second
        {
            get { return second; }
        }
    }

    /// <summary>
    ///   Container for a Pair helper method.
    /// </summary>
    public static class Pair
    {
        /// <summary>
        ///   A helper factory method that lets users take advantage of type inference.
        /// </summary>
        /// <typeparam name = "TFirstParameter">Type of first value.</typeparam>
        /// <typeparam name = "TSecondParameter">Type of second value.</typeparam>
        /// <param name = "first">First value.</param>
        /// <param name = "second">Second value.</param>
        /// <returns>A new <see cref = "Pair{TFirstParameter, TSecondParameter}" /> instance.</returns>
        public static Pair<TFirstParameter, TSecondParameter> Make<TFirstParameter, TSecondParameter>(
            TFirstParameter first, TSecondParameter second)
        {
            return new Pair<TFirstParameter, TSecondParameter>(first, second);
        }
    }
}