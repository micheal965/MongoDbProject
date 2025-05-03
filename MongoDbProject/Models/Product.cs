using MongoDB.Bson;

namespace MongoDbProject.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Categories { get; set; }
    }

}
