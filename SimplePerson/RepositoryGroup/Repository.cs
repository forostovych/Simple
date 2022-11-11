using Simple.Bank;

namespace Simple
{
    public class Repository<T> where T : BaseModel
    {
        public List<T> entities { get; protected set; }

        public Repository()
        {
            entities = Load<T>();
        }

        protected List<T> Load<T>()
        {
            return new List<T>();
        }

        public void Add(T entity)
        {
            var existedEntity = entities.Where(x => x.Id.Equals(entity.Id)).FirstOrDefault();
            if (existedEntity != null)
            {
                entities.Remove(existedEntity);
            }

            entities.Add(entity);
        }
    }
}
