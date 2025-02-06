using MongoDB.Bson.Serialization.Attributes;

namespace UdemyMicroservice.Catalog.Api.Repositories
{
    public abstract class BaseEntity
    {
        [BsonElement("_id")]
        public Guid Id { get; set; }
        protected BaseEntity()
        {
            Id = NewId.NextSequentialGuid();
        }
    }
}
