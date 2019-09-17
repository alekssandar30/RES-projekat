///////////////////////////////////////////////////////////
//  Historical.cs
//  Implementation of the Class Historical
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:35 PM
//  Original author: sale
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using KesMemorija.Database;

public class Historical
{

    private Dictionary<int, Description> descriptionDic;
    private bool[] upisanDataset = new bool[5];
    //za bazu
    public HistoricalDbContext dbContext = new HistoricalDbContext();
    public bool[] UpisanDataset { get => upisanDataset; set => upisanDataset = value; }
    public Dictionary<int, Description> DescriptionDic { get => descriptionDic; set => descriptionDic = value; }

    public Historical()
    {
        DescriptionDic = new Dictionary<int, Description>();
        for (int i = 0; i < 5; i++)
        {
            DescriptionDic.Add(i, new Description(i));
            UpisanDataset[i] = false;
        }
    }

    ~Historical()
    {

    }

    public void ReadDirectlyFromClient(string code, Value value, int dataset)
    {
        if (value == null)
            throw new ArgumentNullException("Prosledjena struktura podataka Value ne sme da ima vrednost null");
        if (code == "")
            throw new ArgumentException("Prosledjeni kod nije u dobrom formatu prosledjen");
        if (dataset < 0 || dataset > 4)
            throw new ArgumentException("Prosledjeni dataset mora biti izmedju 1-5.");

        if (DescriptionDic[dataset].HistoricalList[0].Code == code)
        {
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.IDGeoPolozaja = value.IDGeoPolozaja;
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Potrosnja = value.Potrosnja;
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Timestamp = value.Timestamp;
            DescriptionDic[dataset].HistoricalList[0].Code = code;
        }
        else if (DescriptionDic[dataset].HistoricalList[1].Code == code)
        {
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.IDGeoPolozaja = value.IDGeoPolozaja;
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.Potrosnja = value.Potrosnja;
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.Timestamp = value.Timestamp;
            DescriptionDic[dataset].HistoricalList[1].Code = code;
        }
        else if (DescriptionDic[dataset].HistoricalList[0].Code != null && DescriptionDic[dataset].HistoricalList[0].Code != code)
        {
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.IDGeoPolozaja = value.IDGeoPolozaja;
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.Potrosnja = value.Potrosnja;
            DescriptionDic[dataset].HistoricalList[1].HistoricalValue.Timestamp = value.Timestamp;
            DescriptionDic[dataset].HistoricalList[1].Code = code;
        }
        else
        {
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue = new Value();
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.IDGeoPolozaja = value.IDGeoPolozaja;
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Potrosnja = value.Potrosnja;
            DescriptionDic[dataset].HistoricalList[0].HistoricalValue.Timestamp = value.Timestamp;
            DescriptionDic[dataset].HistoricalList[0].Code = code;
        }

        UpisanDataset[dataset] = true;
    }
    public void ReadFromHistoricalConverter(Dictionary<int, Description> dic)
    {
        if (dic == null)
            throw new ArgumentNullException("Prosledjena struktura ne sme biti vrednosti null");

        FillHistoryComponent(dic);

        for (int i = 0; i < 5; i++)
        {
            if (dic.ContainsKey(i))
            {
                if (dic[i].HistoricalList[0].Code != null)
                {
                    if (CheckDeadBand(dic[i].HistoricalList[0]))
                    {
                        AddToDatabase(dic[i].HistoricalList[0]);
                        StreamWriter sw = new StreamWriter("Logfile.txt");
                        sw.WriteLine("Sending data from Historical to database" + Environment.NewLine);
                        sw.Close();
                    }

                    if (CheckDeadBand(dic[i].HistoricalList[1]))
                    {
                        AddToDatabase(dic[i].HistoricalList[1]);
                        StreamWriter sw = new StreamWriter("Logfile.txt");
                        sw.WriteLine("Sending data from Historical to database" + Environment.NewLine);
                        sw.Close();
                    }
                }
            }
        }

    }
    public void RemoveDataset(Dictionary<int, Description> pom)
    {
        if (pom == null)
            throw new ArgumentNullException("Prosledjeni dictionary ne sme biti null.");

        for (int i = 0; i < 5; i++)
        {
            if (pom.ContainsKey(i))
            {
                if (pom[i].HistoricalList[0].Code != null)
                {
                    //DeleteFromDatabase(pom[i].HistoricalList[0]);
                    //DeleteFromDatabase(pom[i].HistoricalList[1]);

                    DescriptionDic[i].HistoricalList[0].Code = null;
                    DescriptionDic[i].HistoricalList[1].Code = null;
                    DescriptionDic[i].HistoricalList[0].HistoricalValue = null;
                    DescriptionDic[i].HistoricalList[1].HistoricalValue = null; 
                }
            }
        }


    }
    public bool CheckDeadBand(HistoricalProperty hp)
    {
        if (hp == null)      //OVDE MORAS PROVERAVATI OKO NULL-OVA
            throw new ArgumentNullException("Prosledjena struktura ne sme biti vrednosti null");

        if (hp.Code.Equals("CODE_DIGITAL"))        //ZA CODE DIGITAL UVEK DIREKTNO ULAZI U XML FAJL
        {
            return true;
        }

        using (var ctx = new HistoricalDbContext())
        {
            var data = ctx.dbSet.Where(d => d.Code == hp.Code)
                .OrderByDescending(d => d.HistoricalValue.Timestamp)
                .FirstOrDefault();

            if (data == null)
                return true;

            if (hp.HistoricalValue.Potrosnja > data.HistoricalValue.Potrosnja + (data.HistoricalValue.Potrosnja * 0.02))
                return true;
        }

        return false;
    }
    public bool DatasetAlreadyExists(int dataset)
    {
        if (dataset < 0 || dataset > 4)
            throw new ArgumentException("Prosledjeni dataset mora biti u intervalu 1-5.");

        return UpisanDataset[dataset];
    }
    public void FillHistoryComponent(Dictionary<int, Description> converterDictionary)
    {
        if (converterDictionary == null)
            throw new ArgumentNullException("Prosledjena dictionary struktura ne sme imati vrednost null.");

        for (int i = 0; i < 5; i++)
        {
            if (converterDictionary.ContainsKey(i))
            {
                if (converterDictionary[i].HistoricalList[0].Code != null)
                {
                    DescriptionDic[i].Dataset = converterDictionary[i].Dataset;
                    DescriptionDic[i].ID = converterDictionary[i].ID;

                    DescriptionDic[i].HistoricalList[0].Code = converterDictionary[i].HistoricalList[0].Code;
                    DescriptionDic[i].HistoricalList[1].Code = converterDictionary[i].HistoricalList[1].Code;

                    DescriptionDic[i].HistoricalList[0].HistoricalValue = new Value();
                    DescriptionDic[i].HistoricalList[0].HistoricalValue.IDGeoPolozaja = converterDictionary[i].HistoricalList[0].HistoricalValue.IDGeoPolozaja;
                    DescriptionDic[i].HistoricalList[0].HistoricalValue.Potrosnja = converterDictionary[i].HistoricalList[0].HistoricalValue.Potrosnja;
                    DescriptionDic[i].HistoricalList[0].HistoricalValue.Timestamp = converterDictionary[i].HistoricalList[0].HistoricalValue.Timestamp;

                    DescriptionDic[i].HistoricalList[1].HistoricalValue = new Value();
                    DescriptionDic[i].HistoricalList[1].HistoricalValue.IDGeoPolozaja = converterDictionary[i].HistoricalList[1].HistoricalValue.IDGeoPolozaja;
                    DescriptionDic[i].HistoricalList[1].HistoricalValue.Potrosnja = converterDictionary[i].HistoricalList[1].HistoricalValue.Potrosnja;
                    DescriptionDic[i].HistoricalList[1].HistoricalValue.Timestamp = converterDictionary[i].HistoricalList[1].HistoricalValue.Timestamp;
                }
            }
        }
    }
    public void AddToDatabase(HistoricalProperty hProperty)
    {
        if(hProperty.HistoricalValue.Timestamp!=null)
        {
            dbContext.dbSet.Add(hProperty);
            dbContext.SaveChanges();
        }
    }
    public void DeleteFromDatabase(HistoricalProperty hProperty)
    {
        if(hProperty.Code!=null)
        {
            dbContext.dbSet.Remove(hProperty);
            dbContext.SaveChanges();
        }
    }
    public void UpdateInDatabase()
    {
        
    }



}//end Historical