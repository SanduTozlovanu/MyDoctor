namespace MyDoctor.Tests.Helpers
{
    public static class RandomGenerators
    {
        public static string CreateRandomEmail()
        {
            return string.Format("{0}@{1}.com", GenerateRandomAlphabetString(10), GenerateRandomAlphabetString(10));
        }
        private static string GenerateRandomAlphabetString(int length)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rnd.Next(allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
