///////////////////////////////////////////////////////////
//  DeltaCD.cs
//  Implementation of the Class DeltaCD
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:35 PM
//  Original author: Matija
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class DeltaCD {

	private CollectionDescription add;
	private CollectionDescription remove;
	private int transactionID;
	private CollectionDescription update;

	public DeltaCD(){

	}

	~DeltaCD(){

	}

	public CollectionDescription Add{
		get{
			return add;
		}
		set{
			add = value;
		}
	}

	public CollectionDescription Remove{
		get{
			return remove;
		}
		set{
			remove = value;
		}
	}

	public int TransactionID{
		get{
			return transactionID;
		}
		set{
			transactionID = value;
		}
	}

	public CollectionDescription Update{
		get{
			return update;
		}
		set{
			update = value;
		}
	}

}//end DeltaCD