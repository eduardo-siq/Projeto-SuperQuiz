using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[System.Serializable]
class BncQFile {
	// Question
	public List<string> t;
	public List<int> tp;
	public List<string> a;
	public List<string> b;
	public List<string> c;
	public List<string> d;
	public List<string> e;
	public List<string> u;
	public List<string> s;
	// Client
	public string client;
	public List<string> sName;
	public List<string> uName;
}