using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Automation
{
    // TODO: Change this to use the database as well
    public class AutomationManager : IAutomationManager
    {
        public List<AutomationAction> Actions { get; set; }

        public AutomationManager()
        {
            Actions = new List<AutomationAction>();
        }

        public void Create(AutomationAction item)
        {
            if (Actions.Count < 1)
            {
                List<int> indices = Actions.ConvertAll((iter) => iter.ID);
                indices.Sort();
                item.ID = indices.Last();
            }

            Actions.Add(item);
        }

        public Task CreateAsync(AutomationAction item)
        {
            return Task.Run(() => Create(item));
        }

        public void Delete(int id)
        {
            Actions.RemoveAll((item) => item.ID == id);
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() => Delete(id));
        }

        public AutomationAction Get(int id)
        {
            return Actions.Find((item) => item.ID == id);
        }

        public Task<AutomationAction> GetAsync(int id)
        {
            return Task.Run(() => Get(id));
        }

        public IEnumerable<AutomationAction> GetAll()
        {
            return Actions;
        }

        public Task<IEnumerable<AutomationAction>> GetAllAsync()
        {
            return Task.FromResult(Actions.AsEnumerable());
        }

        public void Update(AutomationAction item)
        {
            int index = Actions.FindIndex((iter) => iter.ID == item.ID);
            Actions[index] = item;
        }

        public Task UpdateAsync(AutomationAction item)
        {
            return Task.Run(() => Update(item));
        }
    }
}
