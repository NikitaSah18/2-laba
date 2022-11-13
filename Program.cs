using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializeBasic
{

    public class GuaranteeAgreement
    {
        public int NumberOfContract { get; set; }
        public string DateOfContract { get; set; }

        public TheGuarantor Id { get; set; }

        public GuaranteeAgreement() { }

        public GuaranteeAgreement(int numberOfContract, string dateOfContract,TheGuarantor id)
        {
            Id = id;
            NumberOfContract = numberOfContract;
            DateOfContract = dateOfContract;
        }
    }
    public class TheGuarantor
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public TheGuarantor() { }

        public TheGuarantor(int id, string fio, string address, string phoneNumber)
        {
            Id = id;
            FIO = fio;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
    public class JsonHandler<T> where T : class
    {
        private string fileName;
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


        public JsonHandler() { }

        public JsonHandler(string fileName)
        {
            this.fileName = fileName;
        }


        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            if (new FileInfo(fileName).Length == 0)
            {
                File.WriteAllText(fileName, jsonString);
            }
            else
            {
                Console.WriteLine("Specified path file is not empty");
            }
        }

        public void Delete()
        {
            File.WriteAllText(fileName, string.Empty);
        }

        public void Rewrite(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);
        }

        public void Read(ref List<T> list)
        {
            if (File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string jsonString = File.ReadAllText(fileName);
                    list = JsonSerializer.Deserialize<List<T>>(jsonString);
                }
                else
                {
                    Console.WriteLine("Specified path file is empty");
                }
            }
        }

        public void OutputJsonContents()
        {
            string jsonString = File.ReadAllText(fileName);

            Console.WriteLine(jsonString);
        }

        public void OutputSerializedList(List<T> list)
        {
            Console.WriteLine(JsonSerializer.Serialize(list, options));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<GuaranteeAgreement> partsList = new List<GuaranteeAgreement>();

            JsonHandler<GuaranteeAgreement> partsHandler = new JsonHandler<GuaranteeAgreement>("partsFile.json");

            partsList.Add(new GuaranteeAgreement(232, "(3,2,2022)", new TheGuarantor(1, "Sidorin daniel", "Vishevay", "89913456789")));
            partsList.Add(new GuaranteeAgreement(237, "(7,5,2021)", new TheGuarantor(2, "Korneev Vadim", "Obshaga 3", "89156186278")));
            partsList.Add(new GuaranteeAgreement(234, "(4,3,2020)", new TheGuarantor(3,"Bardin Maksim", "Vokzalnay", "898982123454")));
            partsList.Remove(new GuaranteeAgreement(234, "(4,3,2020)", new TheGuarantor(3, "Bardin Maksim", "Vokzalnay", "898982123454")));
           

            partsHandler.Rewrite(partsList);
            partsHandler.OutputJsonContents();
        }
       
        }

}

