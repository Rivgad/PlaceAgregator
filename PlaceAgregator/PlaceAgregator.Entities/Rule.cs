using System.Collections.Generic;

namespace PlaceAgregator.Entities
{
    public class Rule : Entity
    {
        public string Title { get; private set; }
        public IEnumerable<Place>?  Places  { get; private set; }
    }
}
