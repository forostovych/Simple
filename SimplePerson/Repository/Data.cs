using Simple.Bank;
using Simple.Bank.AccountModels;
using Simple.PersonModel.PersonModels;
using Simple.PersonModel.PersonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Repository
{

    public class Repository<T> where T: BaseModel
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
    public class AccountRepository : Repository<Account>
    {

    }

    public class PersonRepository : Repository<Person>
    {

    }
    public static class Data
    {
        public static AccountRepository AccountRepository = new AccountRepository();

        public static PersonRepository PersonRepository = new PersonRepository();
    }
}
