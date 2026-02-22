using System.Collections.Generic;

namespace APIFramework
{
    public partial class UpdateBooks
    {
        public string BookId { get; set; }
        public BookData BookData { get; set; }
        public Details Details { get; set; }
        public List<Features> Features { get; set; }
        public bool Available { get; set; }
        public bool OnSale { get; set; }
    }
}