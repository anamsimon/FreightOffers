using System;

namespace FreightOffers.Model
{
    public class Package
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }

        public bool IsValid()
        {
            return Height > 0 && Width > 0 && Depth > 0;
        }
    }
}
