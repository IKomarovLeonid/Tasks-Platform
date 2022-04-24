using Bogus;

namespace Integration.Src.Helpers
{
    public class Generator
    {
        private readonly Faker _faker;

        public Generator()
        {
            _faker = new Faker();
        }

        public string GenerateString(int length = 15) => _faker.Random.String2(length);
    }
}
