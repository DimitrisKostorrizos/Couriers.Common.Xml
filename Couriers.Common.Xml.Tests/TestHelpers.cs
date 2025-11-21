using System;

namespace Couriers.Common.Xml.Tests
{
    /// <summary>
    /// Contains the helper methods used across the tests
    /// </summary>
    public static class TestHelpers
    {
        #region Private Fields

        /// <summary>
        /// The digits and letters contained in the English alphabet
        /// </summary>
        private static readonly char[] _lettersAndDigits =
        [
            'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z',
            '0', '1', '2', '3',
            '4', '5', '6', '7', '8', '9'
        ];

        /// <summary>
        /// The letters contained in the English alphabet
        /// </summary>
        private static readonly char[] _letters =
        [
            'a', 'b', 'c', 'd', 'e', 'f',
            'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z'
        ];

        #endregion


        #region Public Methods

        /// <summary>
        /// Generates a random <see cref="string"/> of length <paramref name="length"/>
        /// </summary>
        /// <param name="length">The length of the returned string</param>
        /// <returns></returns>
        public static string GenerateRandomString(int length)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

            Span<char> span = stackalloc char[length];

            for (int i = 0; i < length; i++)
            {
                var randomCharacterIndex = Random.Shared.Next(_lettersAndDigits.Length - 1);

                span[i] = _lettersAndDigits[randomCharacterIndex];
            }

            return new string(span);
        }

        /// <summary>
        /// Generates a random <see cref="string"/> of length <paramref name="length"/>
        /// </summary>
        /// <param name="length">The length of the returned string</param>
        /// <returns></returns>
        public static string RenerateRandomWord(int length)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length);

            Span<char> span = stackalloc char[length];

            for (int i = 0; i < length; i++)
            {
                var randomCharacterIndex = Random.Shared.Next(_letters.Length - 1);

                span[i] = _letters[randomCharacterIndex];
            }

            return new string(span);
        }

        #endregion
    }
}