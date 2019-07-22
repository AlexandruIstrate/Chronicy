using Chronicy.Data;
using Chronicy.Data.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronicy.Standard.Data.Storage
{
    public class FileDataSource : IDataSource<Notebook>
    {
        private readonly List<Notebook> notebooks;

        public string Path { get; set; }

        public FileDataSource(string path)
        {
            Path = path;
            notebooks = LoadFromFile() ?? new List<Notebook>();
        }

        public void Create(Notebook item)
        {
            item.ID = GetNextID();
            notebooks.Add(item);

            WriteToFile(notebooks);
        }

        public async Task CreateAsync(Notebook item)
        {
            item.ID = GetNextID();
            notebooks.Add(item);

            await WriteToFileAsync(notebooks);
        }

        public void Delete(int id)
        {
            notebooks.RemoveAll((notebook) => notebook.ID == id);
            WriteToFile(notebooks);
        }

        public async Task DeleteAsync(int id)
        {
            notebooks.RemoveAll((notebook) => notebook.ID == id);
            await WriteToFileAsync(notebooks);
        }

        public Notebook Get(int id)
        {
            return notebooks.Find((notebook) => notebook.ID == id);
        }

        public Task<Notebook> GetAsync(int id)
        {
            return Task.FromResult(Get(id));
        }

        public IEnumerable<Notebook> GetAll()
        {
            return notebooks;
        }

        public Task<IEnumerable<Notebook>> GetAllAsync()
        {
            return Task.FromResult(notebooks.AsEnumerable());
        }

        public void Update(Notebook item)
        {
            int index = notebooks.FindIndex((notebook) => notebook.ID == item.ID);

            if (index < 0)
            {
                throw new DataSourceException($"The Notebook with ID { item.ID } could not be found");
            }

            notebooks[index] = item;

            WriteToFile(notebooks);
        }

        public async Task UpdateAsync(Notebook item)
        {
            int index = notebooks.FindIndex((notebook) => notebook.ID == item.ID);

            if (index < 0)
            {
                throw new DataSourceException($"The Notebook with ID { item.ID } could not be found");
            }

            notebooks[index] = item;

            await WriteToFileAsync(notebooks);
        }

        private int GetNextID()
        {
            List<int> ids = notebooks.ConvertAll((notebook) => notebook.ID);

            if (ids.Count < 1)
            {
                return 1;
            }

            ids.Sort();

            int lastId = ids.Last();
            return lastId + 1;
        }

        private List<Notebook> LoadFromFile()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                using (FileStream stream = new FileStream(Path,
                    FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, bufferSize: 4096))
                {
                    byte[] buffer = new byte[stream.Length];
                    int bytesRead = 0;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        stringBuilder.Append(text);
                    }
                }

                string json = stringBuilder.ToString();
                return JsonConvert.DeserializeObject<List<Notebook>>(json);
            }
            catch (IOException e)
            {
                throw new DataSourceException("Could not read notebooks file", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not deserialize notebooks JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("An unknown error occurred while loading the notebooks file", e);
            }
        }

        private async Task<List<Notebook>> LoadFromFileAsync()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();

                using (FileStream stream = new FileStream(Path,
                    FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    byte[] buffer = new byte[stream.Length];
                    int bytesRead = 0;

                    while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        stringBuilder.Append(text);
                    }
                }

                string json = stringBuilder.ToString();
                return JsonConvert.DeserializeObject<List<Notebook>>(json);
            }
            catch (IOException e)
            {
                throw new DataSourceException("Could not read notebooks file", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not deserialize notebooks JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("An unknown error occurred while loading the notebooks file", e);
            }
        }

        private void WriteToFile(List<Notebook> notebooks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(notebooks);
                byte[] encodedText = Encoding.UTF8.GetBytes(json);

                using (FileStream stream = new FileStream(Path,
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize: 4096))
                {
                    stream.SetLength(0);
                    stream.Write(encodedText, 0, encodedText.Length);
                }
            }
            catch (IOException e)
            {
                throw new DataSourceException("Could not write notebooks file", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not serialize notebooks JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("An unknown error occurred while loading the notebooks file", e);
            }
        }

        private async Task WriteToFileAsync(List<Notebook> notebooks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(notebooks);
                byte[] encodedText = Encoding.UTF8.GetBytes(json);

                using (FileStream stream = new FileStream(Path,
                    FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    stream.SetLength(0);
                    await stream.WriteAsync(encodedText, 0, encodedText.Length);
                }
            }
            catch (IOException e)
            {
                throw new DataSourceException("Could not write notebooks file", e);
            }
            catch (JsonException e)
            {
                throw new DataSourceException("Could not serialize notebooks JSON", e);
            }
            catch (Exception e)
            {
                throw new DataSourceException("An unknown error occurred while loading the notebooks file", e);
            }
        }
    }
}
