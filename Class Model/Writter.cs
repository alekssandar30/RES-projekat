///////////////////////////////////////////////////////////
//  Writter.cs
//  Implementation of the Class Writter
//  Generated by Enterprise Architect
//  Created on:      08-May-2019 2:32:36 PM
//  Original author: sale
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



public class Writter : IClient {

	public DumpingBuffer m_DumpingBuffer;
	public Value m_Value;

	public Writter(){

	}

	~Writter(){

	}

	/// 
	/// <param name="code"></param>
	/// <param name="value"></param>
	public void ManualWriteToHistory(string code, Value value){

	}

	/// 
	/// <param name="code"></param>
	/// <param name="value"></param>
	public void WriteToDumpingBuffer(string code, Value value){

	}

	public void ReadData(){

	}

}//end Writter