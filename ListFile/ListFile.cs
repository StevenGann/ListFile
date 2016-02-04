using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class ListFile<T> : List<T>
{

    public int Count = 0;
    private string className;
    private string directory;
    private Type c;
    private int index = 0;
    private string folder = "data";

    public ListFile()
    {

    }

    public ListFile(int i, string folder)
    {
        this.index = i;
        this.folder = folder;
    }

    public ListFile(string folder)
    {
        this.folder = folder;
    }

    public ListFile(int i)
    {
        this.index = i;
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


    public T get(int index)
    {
        return this.read(index);

    }

    private void write(int i, T o)
    {
        //TODO make this suck less
        //I think I made it suck less. ~Steven
        string dir = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/Desktop/" + "ListFile/" + this.folder + "/" + this.directory + "/";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string filename = className + "." + i + ".xml";

        XmlSerializer writer = new XmlSerializer(typeof(T));

        StreamWriter file = new StreamWriter(dir + filename);
        writer.Serialize(file, o);
        file.Close();
    }

    private T read(int index)
    {
        string filename = System.Environment.GetEnvironmentVariable("USERPROFILE") + "/Desktop/" + "ListFile/" + this.folder + "/" + this.directory + "/" + className + "." + index + ".xml";
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
            // Yield each day of the week.
            yield return read(i);
        }
    }

    new public void Add(T o)
    {
        this.className = o.GetType().Name;
        this.directory = className + "." + index;
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
        throw new NotImplementedException();
    }
}