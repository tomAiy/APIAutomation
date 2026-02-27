using System.Collections.Generic;

namespace APIFramework
{
    public partial class Book
    {
        public string BookId { get; set; }
        public BookData BookData { get; set; }
        public Details Details { get; set; }
        public List<Features> Features { get; set; }
        public bool Available { get; set; }
        public bool OnSale { get; set; }
    }

    public partial class Details
    {
        public long Pages { get; set; }
        public string Format { get; set; }
        public bool Illustrated { get; set; }
        public bool SpecialEdition { get; set; }
    }

    public partial class BookData
    {
        public string Title { get; set; }
        public bool Fiction { get; set; }
    }

    public partial class Features
    {
        public string Name { get; set; }
        public bool Extended { get; set; }
    }   
}
