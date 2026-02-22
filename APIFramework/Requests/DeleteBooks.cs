using System.Collections.Generic;

namespace APIFramework
{
    public partial class DeleteBooks
    {
        public long BookId { get; set; }
        public BookData BookData { get; set; }
        public Details Details { get; set; }
        public List<Features> Features { get; set; }
        public bool Available { get; set; }
        public bool OnSale { get; set; }
    }
}