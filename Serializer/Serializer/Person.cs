using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    [Serializable]
    class Person : ISerializable
        
        {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        private DateTime recordingTime;
        private static int serialIndex;
        [NonSerialized] public int serial;

        public Person (string name, string address, string phone) 
        {
            Name = name;
            Address = address;
            Phone = phone;
            serial = PersonCount() + 1;
            recordingTime = DateTime.Now;
        }

        public Person(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Address = (string)info.GetValue("Address", typeof(string));
            Phone = (string)info.GetValue("Phone", typeof(string));
            recordingTime = (DateTime)info.GetValue("RecordingTime", typeof(DateTime));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Address", Address);
            info.AddValue("Phone", Phone);
            info.AddValue("RecordingTime", recordingTime);
        }

        public void Serialize()
        {
            string FilePath = "Person" + serial + ".dat";
            Stream stream = File.Open(FilePath, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, this);
            stream.Close();
        }

        public static Person Deserialize(int serialIndex)
        {
            string FilePath = "person" + serialIndex.ToString() + ".dat";
            Stream stream = File.Open(FilePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            Person person = (Person)bf.Deserialize(stream);
            stream.Close();
            return person;
        }

        public static int PersonCount()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DirectoryInfo di = new DirectoryInfo(path);

            int personNum = di.GetFiles("*.dat", SearchOption.AllDirectories).Length;

            return personNum;
        }

        public static Person ShowNext()
        {
            serialIndex++;

            int highestSerial = PersonCount();
            if (serialIndex > highestSerial)
            {
                serialIndex = highestSerial;
            }
            return Deserialize(serialIndex);
        }

        public static Person ShowPrevious()
        {
            serialIndex--;

            if (serialIndex == 0)
            {
                serialIndex = 1;
            }
            return Deserialize(serialIndex);
        }

        public static Person ShowFirst()
        {
            serialIndex = 1;
            return Deserialize(1);
        }

        public static Person ShowLast()
        {
            int highestSerial = PersonCount();
            serialIndex = highestSerial;
            return Deserialize(highestSerial);

        }
    }
}

