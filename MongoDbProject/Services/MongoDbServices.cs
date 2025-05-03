using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbProject.Models;

namespace MongoDbProject.Services
{
    public class MongoDbServices : IMongoDbServices
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoDbServices(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new MongoClient(configuration.GetConnectionString("client"));
            database = client.GetDatabase(configuration.GetConnectionString("database"));
        }

        //1
        public async Task CreateCollectionAsync()
        {
            await database.CreateCollectionAsync("Users");
            await database.CreateCollectionAsync("Products");
        }
        public async Task InsertDataIntoUsersCollectionAsync()
        {
            //Insert Users
            var users = new List<User>
                {
                    new User { Name = "Micheal", Age = 20,Experience=10, Address = new Address { Street = "A", City = "Cairo", ZipCode = "41521" }, Tags = new List<string>{"backend","frontend"},Grades=new List<int>{10, 20, 30 ,40,40},IsRemoteWorker=false },
                    new User { Name = "Felopater", Age = 20, Experience=20,Address = new Address { Street = "B", City = "Giza", ZipCode = "31521" }, Tags = new List<string>{ "developer" ,"Machine Learner"},Grades=new List<int>{5, 10, 15 ,20,20},IsRemoteWorker=true },
                    new User { Name = "Carol", Age = 21,Experience=21, Address = new Address { Street = "c", City = "Giza", ZipCode = "615221" }, Tags = new List<string>{"Machine Learner","AI Engineer"},Grades=new List<int>{100, 25, 30 ,40,50},IsRemoteWorker=true },
                    new User { Name = "Kerolos", Age = 21, Experience=15,Address = new Address { Street = "d", City = "Giza", ZipCode = "51521" }, Tags = new List<string>{"Mongo","Express","React"},Grades=new List<int>{10, 20, 25,30 ,40},IsRemoteWorker=true},
                    new User { Name = "Youstina", Age = 21, Experience=10,Address = new Address { Street = "e", City = "Giza", ZipCode = "781521" }, Tags = new List<string>{"UIUX"},Grades=new List<int>{40, 50, 10 ,70,10},IsRemoteWorker=true },
                    new User { Name = "Pola", Age = 21,Experience=20, Address = new Address { Street = "f", City = "Giza", ZipCode = "91521" }, Tags = new List<string>{"tester"} ,Grades=new List<int>{10,15, 20, 30 ,40},IsRemoteWorker=true},
                    new User { Name = "TestForDelete", Age = 20,Experience=10, Address = new Address { Street = "Test", City = "Test", ZipCode = "Test" }, Tags = new List<string>{ "Test","TestForDelete" } ,Grades=new List<int>{10, 20, 30 ,40,0},IsRemoteWorker=true}
                };
            var usersCollection = database.GetCollection<User>("Users");
            await usersCollection.InsertManyAsync(users);

        }
        public async Task InsertDataIntoProductCollectionAsync()
        {

            //Insert Products
            var products = new List<Product>
                {
                    new Product { Name = "Laptop", Price = 1000, Categories = new List<string>{"Electronics", "Computing"} },
                    new Product { Name = "PC", Price = 10000, Categories = new List<string>{ "Electronics" } },
                    new Product { Name = "Mobile", Price = 7000, Categories = new List<string>{ "Electronics","Smart"} },
                    new Product { Name = "Chair", Price = 500, Categories = new List<string>{"Furniture"} },
                    new Product { Name = "TestForDelete", Price = 500, Categories = new List<string>{ "TestForDelete" } }
                };

            var productsCollection = database.GetCollection<Product>("Products");
            await productsCollection.InsertManyAsync(products);

        }
        //21`

        public async Task UpdateDocumentAsync()
        {
            var Users = database.GetCollection<User>("Users");

            //$Set
            var filterForSet = Builders<User>.Filter.Eq(p => p.Name, "Micheal");
            var updateFotSet = Builders<User>.Update.Set(p => p.Name, "Micheal Magdy");
            await Users.UpdateOneAsync(filterForSet, updateFotSet);


            //$Push
            var filterForPush = Builders<User>.Filter.Eq(p => p.Name, "Felopater");
            var updateForPush = Builders<User>.Update.Push(p => p.Tags, "Full Stack Developer .NET");
            await Users.UpdateOneAsync(filterForPush, updateForPush);


            //$inc
            var filterForInc = Builders<User>.Filter.Eq(p => p.Name, "Pola");
            var updateForInc = Builders<User>.Update.Inc(p => p.Age, 1);
            await Users.UpdateOneAsync(filterForInc, updateForInc);


        }

        //3
        public async Task DeleteDocumentsAsync()
        {
            var Users = database.GetCollection<User>("Users");
            var Products = database.GetCollection<Product>("Products");

            //Delete From User Collection
            var FilterForUsers = Builders<User>.Filter.Eq(u => u.Name, "TestForDelete");
            await Users.DeleteOneAsync(FilterForUsers);


            //Delete From Product Collection
            var FilterForProducts = Builders<Product>.Filter.Eq(p => p.Name, "TestForDelete");
            await Products.DeleteOneAsync(FilterForProducts);


        }

        //4

        #region Find FirstPoint

        public async Task<List<User>> Find_Or()
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Name, "Carol"),
                Builders<User>.Filter.Lt(u => u.Age, 21)
                );

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();
        }
        public async Task<List<User>> Find_And()
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(u => u.IsRemoteWorker, true),
                Builders<User>.Filter.Lt(u => u.Age, 21)
                );

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();

        }
        #endregion

        #region Find SecondPoint

        public async Task<List<User>> Find_in()
        {
            var filter = Builders<User>.Filter.In(u => u.Name, new[] { "Kerolos", "Youstina", "Pola" });

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();
        }
        public async Task<List<User>> Find_all()
        {
            var filter = Builders<User>.Filter.All(u => u.Tags, new[] { "backend", "frontend" });

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();

        }
        public async Task<List<User>> Find_size()
        {
            var filter = Builders<User>.Filter.Size(p => p.Tags, 3);

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();

        }

        #endregion

        #region Find ThirdPoint
        public async Task<List<User>> Find_exp()
        {
            var filter = new BsonDocument
            {
                { "$expr", new BsonDocument{{ "$eq", new BsonArray { "$Age", "$Experience" } } }
                }
            };

            var Users = database.GetCollection<User>("Users");
            return await Users.Find(filter).ToListAsync();
        }
        #endregion

        //5
        public async Task CreateSingleFieldIndex()
        {
            var Usercollection = database.GetCollection<User>("Users");

            var nameIndex = Builders<User>.IndexKeys.Ascending(u => u.Name);
            var indexOptions = new CreateIndexOptions { Name = "SingleIndexOnUserCollection" };
            await Usercollection.Indexes.CreateOneAsync(new CreateIndexModel<User>(nameIndex, indexOptions));
        }
        public async Task CreateCompoundIndex()
        {
            var Usercollection = database.GetCollection<User>("Users");

            var nameIndex = Builders<User>.IndexKeys.Ascending(u => u.Name).Ascending(u => u.Age);
            var indexOptions = new CreateIndexOptions { Name = "CompoundIndexOnUserCollection" };
            await Usercollection.Indexes.CreateOneAsync(new CreateIndexModel<User>(nameIndex, indexOptions));
        }
        public async Task CreateUniqueIndex()
        {
            var Usercollection = database.GetCollection<Product>("Products");

            var nameIndex = Builders<Product>.IndexKeys.Ascending(u => u.Name);
            var indexOptions = new CreateIndexOptions { Name = "UniqueIndexOnProductCollection", Unique = true };
            await Usercollection.Indexes.CreateOneAsync(new CreateIndexModel<Product>(nameIndex, indexOptions));
        }

        //6
        public async Task MultiplyAllArrayElements()
        {
            var UserCollection = database.GetCollection<User>("Users");

            // Step 1: Fetch all documents with grades
            var usersWithGrades = await UserCollection.Find(Builders<User>.Filter.Exists(u => u.Grades)).ToListAsync();

            // Step 2: For each user, calculate totalGrade and update the document
            foreach (var user in usersWithGrades)
            {
                var total = user.Grades.Sum();

                var update = Builders<User>.Update.Set("totalGrade", total);

                await UserCollection.UpdateOneAsync(
                    Builders<User>.Filter.Eq("_id", user.Id),
                    update
                );
            }
        }


    }
}