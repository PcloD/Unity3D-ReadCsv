using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReadCsv : MonoBehaviour {

    public Text textUI;
    //存数数据
    protected Hashtable DataTable;

	// Use this for initialization
	void Start () 
    {
        ReadCsvText();
	}

    void ReadCsvText()
    {
        DataTable = new Hashtable();
        int DataRow=0;

        TextAsset textCsv = Resources.Load("test", typeof(TextAsset)) as TextAsset;
        

        //一行数据
        string[] lineArray = textCsv.text.Split('\r');

        string[][] itemArray = new string[lineArray.Length][];

        //把csv中的数据储存在二位数组中
        for (int i = 0; i < lineArray.Length; i++)
        {
            itemArray[i] = lineArray[i].Split(',');
        }

        //将数据存储到哈希表中，存储方法：Key为name+id，Value为值
        int nRow = itemArray.Length;//总行数
        Debug.Log("Row:" + nRow);
        int nCol = itemArray[0].Length;//总列数
        Debug.Log("nCol:" + nCol);

        DataRow = nRow - 1;//第一行为说明行

        for (int i = 1; i < nRow; ++i)//从文本第二行开始
        {
            if (itemArray[i][0] == "\n" || itemArray[i][0] == "")//排空
            {
                nRow--;
                DataRow = nRow - 1;
                continue;
            }

            //ID信息
            string id = itemArray[i][0].Trim();

            for (int j = 1; j < nCol; ++j)
            {
                DataTable.Add(itemArray[0][j] + "_" + id, itemArray[i][j]);//网格存取信息
            }
        }

        textUI.text = GetProperty("物品名字",2);
    }

    //根据name和id获取相关属性，返回string类型
    protected virtual string GetProperty(string name, int id)
    {
        return GetProperty(name, id.ToString());
    }

    /// <summary>
    /// 获得信息
    /// </summary>
    /// <param name="name">键</param>
    /// <param name="id">ID</param>
    /// <returns></returns>
    protected virtual string GetProperty(string name, string id)
    {
        string key = name + "_" + id;
        if (DataTable.ContainsKey(key))
            return DataTable[key].ToString();
        else
            return "";
    }
}
