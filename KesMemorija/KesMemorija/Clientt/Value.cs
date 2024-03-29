///////////////////////////////////////////////////////////
//  Value.cs
//  Implementation of the Class Value
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:36 PM
//  Original author: Matija
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

public class Value {

	private string idGeoPolozaja;
	private double potrosnja;
	private DateTime timestamp;

    public Value()
    {

    }
	public Value(string id, double p){
        
        CheckGeoId(id);

        if (p < 0)
            throw new ArgumentException("Potrosnja ne sme biti manja od 0.");
        this.IDGeoPolozaja = id;
        this.Potrosnja = p;
        Timestamp = DateTime.Now;
	}

    public void CheckGeoId(string id)
    {
        //Regex reg = new Regex(@"^\d{4}$");
        int num;
        bool isNum = int.TryParse(id, out num);
        if(isNum && id.Length != 4)
        {
            throw new ArgumentException("ID geografskog podrucja mora imati 4 karaktera");
        }
        //if (!reg.IsMatch(id))
            
    }

	~Value(){

	}

	public string IDGeoPolozaja{
		get{
			return idGeoPolozaja;
		}
		set{
			idGeoPolozaja = value;
		}
	}

	public double Potrosnja{
		get{
			return potrosnja;
		}
		set{
			potrosnja = value;
		}
	}

	public DateTime Timestamp{
		get{
			return timestamp;
		}
		set{
			timestamp = value;
		}
	}

    public override string ToString()
    {
        return IDGeoPolozaja + " " + Potrosnja + " " + Timestamp;
    }

}//end Value