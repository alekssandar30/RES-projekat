///////////////////////////////////////////////////////////
//  DumpingBuffer.cs
//  Implementation of the Class DumpingBuffer
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:35 PM
//  Original author: sale
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class DumpingBuffer {

	private Historical m_Historical;
	private CollectionDescription m_CollectionDescription;
	public DeltaCD m_DeltaCD;

	public DumpingBuffer(){

	}

	~DumpingBuffer(){

	}

	/// 
	/// <param name="CD"></param>
	public DeltaCD CDtoDeltaCD(CollectionDescription CD){

		return null;
	}

	/// 
	/// <param name="deltaCD"></param>
	public void SendToHistorical(DeltaCD deltaCD){

	}

}//end DumpingBuffer