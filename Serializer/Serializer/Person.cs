using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    [Serializable]
    class Person : ISerializable, IDeserializationCallback
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        private DateTime recordingTime;
        public static int counter = 0;
        [NonSerialized] public int serial;

        public Person (string name, string address, string phone) 
        {
            Name = name;
            Address = address;
            Phone = phone;
            counter++;
            serial = counter;
            recordingTime = DateTime.Now;
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            Phone = (string)info.GetValue("Phone", typeof(string));
            recordingTime = (DateTime)info.GetValue("recordingTime", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Address", Address);
            info.AddValue("Phone", Phone);
            info.AddValue("recordingTime", recordingTime);
        }

        public void Serialize(string FilePath = "Person1.dat")
        {
            FilePath = "Person" + serial + ".dat";
            Stream stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, this);
            stream.Close();
        }

        public static Person Deserialize(string FilePath = "Person1.dat")
        {
            Stream stream = File.Open(FilePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            Person person = (Person)bf.Deserialize(stream);
            stream.Close();
            return person;
        }

        void IDeserializationCallback.OnDeserialization(Object sender)
        {
            //still needs to be implemented
        }

    }
}

