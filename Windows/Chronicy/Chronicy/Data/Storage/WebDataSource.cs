using Chronicy.Web;
using System;
using System.Threading.Tasks;

namespace Chronicy.Data.Storage
{
    public class WebDataSource : IDataSource<Notebook>
    {
        private ChronicyWebApi api = new ChronicyWebApi();

        public Notebook PullData()
        {
            throw new NotImplementedException();
        }

        public async Task<Notebook> PullDataAsync()
        {
            throw new NotImplementedException();
        }

        public void PushData(Notebook data)
        {
            throw new NotImplementedException();
        }

        public async Task PushDataAsync(Notebook data)
        {
            throw new NotImplementedException();
        }

        public bool HasChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
