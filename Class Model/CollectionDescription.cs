///////////////////////////////////////////////////////////
//  CollectionDescription.cs
//  Implementation of the Class CollectionDescription
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:35 PM
//  Original author: Matija
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class CollectionDescription {

	private DumpingPropertyCollection dataset;
	private int dpc;
	private int id;
	public DumpingPropertyCollection m_DumpingPropertyCollection;

	public CollectionDescription(){

	}

	~CollectionDescription(){

	}

	public DumpingPropertyCollection Dataset{
		get{
			return dataset;
		}
		set{
			dataset = value;
		}
	}

	public int Dpc{
		get{
			return dpc;
		}
		set{
			dpc = value;
		}
	}

	public int ID{
		get{
			return id;
		}
		set{
			id = value;
		}
	}

}//end CollectionDescription