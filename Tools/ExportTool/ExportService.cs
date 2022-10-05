﻿using CsvHelper;
using Models;
using System.Globalization;
using System.IO.Pipes;
using System.Text;

namespace ExportTool
{
    public class ExportService
    {
        private string _pathToDirectory { get; set; }
        private string _csvFileName { get; set; }

        public ExportService(string pathToDirectory, string csvFileName)
        {
            _pathToDirectory = pathToDirectory;
            _csvFileName = csvFileName;
        }

        public void WriteClientToCsv(List<Client> clients)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirectory);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            string fullPath = GetFullPathToFile(_pathToDirectory, _csvFileName);

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
            string fullPath = GetFullPathToFile(_pathToDirectory, _csvFileName);
            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {
                    using (var reader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                    {
                        var clients = reader.GetRecords<Client>();
                        return clients.ToList();
                    }
                }
            }            
        }

        private string GetFullPathToFile(string pathToFile, string fileName)
        {
            return Path.Combine(pathToFile, fileName);
        }
    }
}