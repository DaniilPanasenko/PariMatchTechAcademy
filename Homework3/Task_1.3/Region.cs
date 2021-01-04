using System;
namespace Task_1._3
{
    public class Region : IRegion
    {
        public string Brand { get; }

        public string Country { get; }

        public Region(string brand, string country)
        {
            Brand = brand;
            Country = country;
        }

        public override int GetHashCode()
        {
            return Brand.GetHashCode() * 13 ^ Country.GetHashCode() * 17;
        }

        public override bool Equals(object obj)
        {
            return ((Region)obj).Brand == Brand && ((Region)obj).Country == Country;
        }

        public override string ToString()
        {
            return Brand + ", " + Country;
        }
    }
}