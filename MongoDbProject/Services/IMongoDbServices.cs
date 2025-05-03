using MongoDbProject.Models;

namespace MongoDbProject.Services
{
    public interface IMongoDbServices
    {
        Task CreateCollectionAsync();
        Task InsertDataIntoUsersCollectionAsync();
        Task InsertDataIntoProductCollectionAsync();
        Task UpdateDocumentAsync();
        Task DeleteDocumentsAsync();
        Task<List<User>> Find_Or();
        Task<List<User>> Find_And();
        Task<List<User>> Find_in();
        Task<List<User>> Find_all();
        Task<List<User>> Find_size();
        Task<List<User>> Find_exp();
        Task CreateSingleFieldIndex();
        Task CreateCompoundIndex();
        Task CreateUniqueIndex();
        Task MultiplyAllArrayElements();

    }
}
