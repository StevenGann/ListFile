using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

[Serializable]
public class ListFile<T>
{

    public int Count = 0;
    public string TypeName;
    [XmlIgnore]public Type Type;
    public int ID = 0;
    public string Path;

    public ListFile()
    {
        Path = "cache/";
    }

    public ListFile(int i, string path)
    {
        this.ID = i;
        Path = path;
    }

    public ListFile(string path)
    {
        Path = path;
    }

    public ListFile(int i)
    {
        this.ID = i;
        Path = "cache/";
    }

    public T this[int index]
    {
        get
        {
            return read(index);
        }
        set
        {
            write(index, value);
        }
    }


    public T Get(int index)
    {
        return this.read(index);
    }

    private void write(int i, T o)
    {
        //TODO make this suck less
        //I think I made it suck less. ~Steven
        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }

        string filename = TypeName + "." + ID + "." + i + ".xml";

        XmlSerializer writer = new XmlSerializer(typeof(T));

        StreamWriter file = new StreamWriter(Path + filename);
        writer.Serialize(file, o);
        file.Close();
    }

    private T read(int index)
    {
        string filename = Path + TypeName + "." + this.ID + "." + index + ".xml";
        XmlSerializer mySerializer = new XmlSerializer(typeof(T));

        FileStream myFileStream = new FileStream(filename, FileMode.Open);
        T obj;
        obj = (T)mySerializer.Deserialize(myFileStream);

        myFileStream.Close();
        return obj;
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return read(i);
        }
    }

    public void Add(T o)
    {
        this.TypeName = o.GetType().Name;
        this.Type = o.GetType();
        this.Count += 1;
        this.write(Count - 1, o);

    }

    public void Insert(int i, T o)
    {
        this.write(i, o);
    }

    public void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            string filename = Path + TypeName + "." + this.ID + "." + i + ".xml";
            File.Delete(filename);
        }
        Count = 0;
    }

    public void Destroy()
    {
        Clear();
        if (IsDirectoryEmpty(Path))
        {
            try
            {
                File.Delete(Path);
            }
            catch { }
        }
    }

    private bool IsDirectoryEmpty(string path)
    {
        return !Directory.EnumerateFileSystemEntries(path).Any();
    }

}