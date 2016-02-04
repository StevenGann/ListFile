using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

public class ListFile<T> : List<T>
{

    new public int Count = 0;
    private string className;
    private Type c;
    private int index = 0;
    public string Path;

    public ListFile()
    {
        Path = "cache/";
    }

    public ListFile(int i, string path)
    {
        this.index = i;
        Path = path;
    }

    public ListFile(string path)
    {
        Path = path;
    }

    public ListFile(int i)
    {
        this.index = i;
        Path = "cache/";
    }

    new public T this[int index]
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

        string filename = className + "." + index + "." + i + ".xml";

        XmlSerializer writer = new XmlSerializer(typeof(T));

        StreamWriter file = new StreamWriter(Path + filename);
        writer.Serialize(file, o);
        file.Close();
    }

    private T read(int index)
    {
        string filename = Path + className + "." + this.index + "." + index + ".xml";
        XmlSerializer mySerializer = new XmlSerializer(typeof(T));

        FileStream myFileStream = new FileStream(filename, FileMode.Open);
        T obj;
        obj = (T)mySerializer.Deserialize(myFileStream);

        myFileStream.Close();
        return obj;
    }

    new public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return read(i);
        }
    }

    new public void Add(T o)
    {
        this.className = o.GetType().Name;
        this.c = o.GetType();
        this.Count += 1;
        this.write(Count - 1, o);

    }

    new public void Insert(int i, T o)
    {
        this.write(i, o);
    }

    new public void Clear()
    {
        for (int i = 0; i < Count; i++)
        {
            string filename = Path + className + "." + this.index + "." + i + ".xml";
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