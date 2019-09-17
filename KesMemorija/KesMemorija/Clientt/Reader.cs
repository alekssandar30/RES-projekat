///////////////////////////////////////////////////////////
//  Reader.cs
//  Implementation of the Class Reader
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:36 PM
//  Original author: sale
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Linq;
using KesMemorija.Database;
using System.Data.SqlClient;

public class Reader
{
    Historical historical;
    

	public Reader()
    {
        historical = new Historical();
    }

	~Reader(){

	}

    //get data
    public void DisplayData(string code)
    {
        //var query = (from q in historical.db.Descriptions
        //             where q.HistoricalList[0].Code == code
        //             select q).FirstOrDefault();

        if (code == "")
            throw new ArgumentException("Code ne sme biti prazan string.");
        if (code == null)
            throw new ArgumentNullException("Code ne sme biti null.");

        using (var ctx = new HistoricalDbContext())
        {
            var data = ctx.dbSet.Where(d => d.Code == code)
                .OrderByDescending(d => d.HistoricalValue.Timestamp)
                .FirstOrDefault();

            if (data != null)
            {
                Console.WriteLine(data);
            }
        }

        //foreach(Description item in historical.db.Descriptions)
        //{
        //    Console.WriteLine(item);
        //}


    }
   
}//end Reader