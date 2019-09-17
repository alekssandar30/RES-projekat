using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KesMemorija.DumpingBuffer
{
    public class DumpingBufferConverter
    {
        public void AddCDtoDictionary(string code, Value value, Dictionary<int, CollectionDescription> dic, int dataset)
        {
            if (value == null || dic == null)
                throw new ArgumentNullException("Prosledjeni argumenti ne smeju imati vrednost null");
            if (dataset < 0 || dataset > 4)
                throw new ArgumentException("Prosledjena dataset vrednost mora biti u intervalu 1-5");
            if (code == "")
                throw new ArgumentException("Prosledjeni code ne sme biti prazan string");

            if (dic[dataset].Dpc.dumpingPropertyList[0].Code == code)
                dic[dataset].Dpc.dumpingPropertyList[0].DumpingValue = value;
            else if(dic[dataset].Dpc.dumpingPropertyList[1].Code==code)
                dic[dataset].Dpc.dumpingPropertyList[1].DumpingValue = value;
            else if(dic[dataset].Dpc.dumpingPropertyList[0].Code==null)
            {  
                dic[dataset].Dpc.dumpingPropertyList[0].Code = code;
                dic[dataset].Dpc.dumpingPropertyList[0].DumpingValue=value;
            }else
            {
                dic[dataset].Dpc.dumpingPropertyList[1].Code = code;
                dic[dataset].Dpc.dumpingPropertyList[1].DumpingValue = value;
            }

            dic[dataset].ID += 1;

            using (var mutex = new Mutex(false, "All_Process_Mutex"))
            {
                mutex.WaitOne();
                File.AppendAllText("Logfile.txt", "Dumping Buffer Converter is converting data from Dumping Buffer" + Environment.NewLine);
                mutex.ReleaseMutex();
            }
        }
    }
}
