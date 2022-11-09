namespace Booking.API.Utilities
{
    public class RandomGenerator
    {
        private static Random random = new();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
