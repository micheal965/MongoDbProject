using MongoDB.Bson;

namespace MongoDbProject.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsRemoteWorker { get; set; }
        public Address Address { get; set; }
        public List<string> Tags { get; set; }
        public List<int> Grades { get; set; }
        public int Experience { get; set; }
    }
}
