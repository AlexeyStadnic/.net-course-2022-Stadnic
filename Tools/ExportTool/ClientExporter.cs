using CsvHelper;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportTool
{
    public class ClientExporter
    {
        private string _pathToDirecory { get; set; }
        private string _csvFileName { get; set; }

        public ClientExporter(string pathToDirectory, string csvFileName)
        {
            _pathToDirecory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        public void WriteClientToCsv(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirecory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = GetFullPathToFile(_pathToDirecory, _csvFileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    using (var writer = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                }
            }
        }

        public List<Client> ReadClientFromCsv()
        {
            var resultClients = new List<Client>();
            string fullPath = GetFullPathToFile(_pathToDirecory, _csvFileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                    {
                        var clients = reader.EnumerateRecords(new Client());

                        foreach (var c in clients)
                        {
                            resultClients.Add(new Client
                            {                                
                                Name = c.Name,                            
                                Passport = c.Passport,                             
                                Phone = c.Phone,
                                Birthday = c.Birthday,
                                Bonus = c.Bonus,
                            });
                        }
                    }
                }
            }
            return resultClients;
        }


        private string GetFullPathToFile(string pathToFile, string fileName)
        {
            return Path.Combine(pathToFile, fileName);
        }
    }
}
