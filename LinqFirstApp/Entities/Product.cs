using System.Globalization;

namespace LinqFirstApp.Entities
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

        public override string ToString()
        {
            return $"| Id: {Id} | Name: {Name} | Price: R$ {Price.ToString("F2", CultureInfo.InvariantCulture)} | Category: {Category.Name} | Category Tier: {Category.Tier} |";
        }
    }
}
