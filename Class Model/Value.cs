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



public class Value {

	private string idGeoPolozaja;
	private double potrosnja;
	private DateTime timestamp;

	public Value(){

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

}//end Value