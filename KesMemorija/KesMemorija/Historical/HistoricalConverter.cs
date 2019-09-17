using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class HistoricalConverter
{
    Historical history = new Historical();
    private Dictionary<int, Description> desc;

    public Historical History { get => history; set => history = value; }

    public HistoricalConverter()
    {
        desc = new Dictionary<int, Description>();
        for (int i = 0; i < 5; i++)
        {
            desc.Add(i, new Description(i));
            desc[i].Dataset = i;
        }
    }
    public void ReadFromDumpingBuffer(DeltaCD cd)
    {
        if (cd == null)
            throw new ArgumentNullException("Prosledjena DeltaCD struktura ne sme biti null");

        if (!CheckIfDicIsEmpty(cd.AddDic))
        {
            if (!CheckDatasetAndCode(cd.AddDic))
                throw new ArgumentException("Prosledjeni CollectionDescriptionAdd nema odgovarajuci dataset za date kodove");
            FillDescription(desc, cd.AddDic);
            History.ReadFromHistoricalConverter(desc);
            CleanDescription(desc);
        }
        if(!CheckIfDicIsEmpty(cd.UpdateDic))
        {
            if (!CheckDatasetAndCode(cd.UpdateDic))
                throw new ArgumentException("Prosledjeni CollectionDescriptionUpdate nema odgovarajuci dataset za date kodove");
            FillDescription(desc, cd.UpdateDic);
            History.ReadFromHistoricalConverter(desc);
            CleanDescription(desc);
        }

        FillDescription(desc, cd.RemoveDic);
        History.RemoveDataset(desc);
        CleanDescription(desc);

        using (var mutex = new Mutex(false, "All_Process_Mutex"))
        {
            mutex.WaitOne();
            File.AppendAllText("Logfile.txt", "Historical Converter is sending Description structure to Historical component" + Environment.NewLine);
            mutex.ReleaseMutex();
        }
    }
    public bool CheckDatasetAndCode(Dictionary<int, CollectionDescription> arg)
    {
        if (arg == null)
            throw new ArgumentNullException("Prosledjena struktura ne sme biti null");

        for (int i = 0; i < 5; i++)
        {
            if (arg.ContainsKey(i))
            {
                if (arg[i].Dpc.dumpingPropertyList[0].Code != null && arg[i].Dpc.dumpingPropertyList[1].Code != null)
                {
                    switch (arg[i].Dataset)
                    {
                        case 0:
                            {
                                return (arg[0].Dpc.dumpingPropertyList[0].Code == "CODE_ANALOG" && arg[0].Dpc.dumpingPropertyList[1].Code == "CODE_DIGITAL"
                                        || arg[0].Dpc.dumpingPropertyList[0].Code == "CODE_DIGITAL" && arg[0].Dpc.dumpingPropertyList[1].Code == "CODE_ANALOG");
                            }

                        case 1:
                            {
                                return (arg[1].Dpc.dumpingPropertyList[0].Code == "CODE_CUSTOM" && arg[1].Dpc.dumpingPropertyList[1].Code == "CODE_LIMITSET"
                                       || arg[1].Dpc.dumpingPropertyList[0].Code == "CODE_LIMITSET" && arg[1].Dpc.dumpingPropertyList[1].Code == "CODE_CUSTOM");
                            }


                        case 2:
                            {
                                return (arg[2].Dpc.dumpingPropertyList[0].Code == "CODE_SINGLENODE" && arg[2].Dpc.dumpingPropertyList[1].Code == "CODE_MULTIPLENODE"
                                       || arg[2].Dpc.dumpingPropertyList[0].Code == "CODE_MULTIPLENODE" && arg[2].Dpc.dumpingPropertyList[1].Code == "CODE_SINGLENODE");
                            }

                        case 3:
                            {
                                return (arg[3].Dpc.dumpingPropertyList[0].Code == "CODE_SOURCE" && arg[3].Dpc.dumpingPropertyList[1].Code == "CODE_CONSUMER"
                                       || arg[3].Dpc.dumpingPropertyList[0].Code == "CODE_CONSUMER" && arg[3].Dpc.dumpingPropertyList[1].Code == "CODE_SOURCE");
                            }

                        case 4:
                            {
                                return (arg[4].Dpc.dumpingPropertyList[0].Code == "CODE_MOTION" && arg[4].Dpc.dumpingPropertyList[1].Code == "CODE_SENSOR"
                                        || arg[4].Dpc.dumpingPropertyList[0].Code == "CODE_SENSOR" && arg[4].Dpc.dumpingPropertyList[1].Code == "CODE_MOTION");
                            }

                    }
                }
            }
        }

        return false;
    }
    public bool DatasetAlreadyExist(int dataset)
    {
        if (dataset < 0 || dataset > 4)
            throw new ArgumentException("Prosledjeni dataset mora biti u intervalu 1-5");
        return History.DatasetAlreadyExists(dataset);
    }
    public void FillDescription(Dictionary<int, Description> dic, Dictionary<int, CollectionDescription> deltaCD_Dic)
    {
        if (dic == null || deltaCD_Dic == null)
            throw new ArgumentNullException("Prosledjene strukture ne smeju biti null");

        for (int i = 0; i < 5; i++)
        {
            if(deltaCD_Dic[i].Dpc.dumpingPropertyList[0].Code!=null && deltaCD_Dic[i].Dpc.dumpingPropertyList[1].Code!=null)
            {
                dic[i].Dataset = deltaCD_Dic[i].Dataset;
                dic[i].ID = deltaCD_Dic[i].ID;

                dic[i].HistoricalList[0].Code = deltaCD_Dic[i].Dpc.dumpingPropertyList[0].Code;
                dic[i].HistoricalList[0].HistoricalValue = new Value();
                dic[i].HistoricalList[0].HistoricalValue.IDGeoPolozaja = deltaCD_Dic[i].Dpc.dumpingPropertyList[0].DumpingValue.IDGeoPolozaja;
                dic[i].HistoricalList[0].HistoricalValue.Potrosnja = deltaCD_Dic[i].Dpc.dumpingPropertyList[0].DumpingValue.Potrosnja;
                dic[i].HistoricalList[0].HistoricalValue.Timestamp = deltaCD_Dic[i].Dpc.dumpingPropertyList[0].DumpingValue.Timestamp;

                dic[i].HistoricalList[1].HistoricalValue = new Value();
                dic[i].HistoricalList[1].Code = deltaCD_Dic[i].Dpc.dumpingPropertyList[1].Code;
                dic[i].HistoricalList[1].HistoricalValue.IDGeoPolozaja = deltaCD_Dic[i].Dpc.dumpingPropertyList[1].DumpingValue.IDGeoPolozaja;
                dic[i].HistoricalList[1].HistoricalValue.Potrosnja = deltaCD_Dic[i].Dpc.dumpingPropertyList[1].DumpingValue.Potrosnja;
                dic[i].HistoricalList[1].HistoricalValue.Timestamp = deltaCD_Dic[i].Dpc.dumpingPropertyList[1].DumpingValue.Timestamp;
            }
        }
    }
    public void ReadDirectlyFromDB(string code, Value value)
    {
        int dataset = -1;
        switch (code)
        {
            case "CODE_ANALOG": dataset = 0; break;
            case "CODE_DIGITAL": dataset = 0; break;
            case "CODE_CUSTOM": dataset = 1; break;
            case "CODE_LIMITSET": dataset = 1; break;
            case "CODE_SINGLENODE": dataset = 2; break;
            case "CODE_MULTIPLENODE": dataset = 2; break;
            case "CODE_CONSUMER": dataset = 3; break;
            case "CODE_SOURCE": dataset = 3; break;
            case "CODE_MOTION": dataset = 4; break;
            case "CODE_SENSOR": dataset = 4; break;
        }
        Value sendingValue = new Value();
        sendingValue.IDGeoPolozaja = value.IDGeoPolozaja;
        sendingValue.Potrosnja = value.Potrosnja;
        sendingValue.Timestamp = value.Timestamp;

        History.ReadDirectlyFromClient(code, sendingValue, dataset);
    }
    public void CleanDescription(Dictionary<int, Description> dic)
    {
        for(int i=0;i<5;i++)
        {
            if (dic.ContainsKey(i))
            {
                dic[i].Dataset = 0;
                dic[i].ID = 0;

                dic[i].HistoricalList[0].Code = null;
                dic[i].HistoricalList[0].HistoricalValue = null;
                dic[i].HistoricalList[1].Code = null;
                dic[i].HistoricalList[1].HistoricalValue = null;
            }
        }
    }
    public bool CheckIfDicIsEmpty(Dictionary<int, CollectionDescription> dic)
    {
        for(int i=0;i<5;i++)
        {
            if (dic.ContainsKey(i))
            {
                if (dic[i].Dpc.dumpingPropertyList[0].Code != null && dic[i].Dpc.dumpingPropertyList[1].Code != null)
                    return false;
            }
        }

        return true;
    }
}

