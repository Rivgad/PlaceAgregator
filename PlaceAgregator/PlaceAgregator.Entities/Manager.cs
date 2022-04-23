namespace PlaceAgregator.Entities
{
    public class Manager : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FamilyName { get; private set; }

        public long AccountId { get; private set; }
        public Account Account { get; private set; }
    }
}
