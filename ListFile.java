
import java.io.*;
import java.util.Iterator;
import java.util.NoSuchElementException;
import javax.xml.bind.JAXB;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.Unmarshaller;

public class ListFile<T> implements Serializable, Iterable<T> {

    private int size = 0;
    private String className;
    private String directory;
    private Class c;
    private Integer index = 0;
    private String folder;

    ListFile(int i, String folder) {
        this.index = i;
        this.folder = folder;
    }

    ListFile(String folder) {
        this.folder = folder;
    }

    public T get(int index) {
        return this.read(index);

    }

    private void write(T object) throws ArrayIndexOutOfBoundsException {
        //TODO make this suck less
        File par = new File(System.getProperty("user.home") + "/Desktop/" + "ListFile");

        if (!par.exists()) {
            par.mkdir();
        }

        File par2 = new File(par, this.folder);

        if (!par2.exists()) {
            par2.mkdir();
        }

        File dir = new File(par2, this.directory);

        if (!dir.exists()) {
            dir.mkdir();
        }
        try {
            FileOutputStream fos = new FileOutputStream(new File(dir, className + "." + (size - 1) + ".xml"));

            try {
                JAXBContext context = JAXBContext.newInstance(object.getClass());
                Marshaller m = context.createMarshaller();
                m.setProperty(Marshaller.JAXB_FORMATTED_OUTPUT, Boolean.TRUE);
                m.marshal(object, fos);
            } catch (JAXBException e) {
                System.out.println("Write Inner");
            }

        } catch (IOException e) {
            System.out.println("Write Outer");
        }

    }

    private T read(int index) {
        File par = new File(System.getProperty("user.home") + "/Desktop/" + "ListFile/" + this.folder + "/" + this.directory + "/" +className + "." + index + ".xml");
        try {
            JAXBContext context = JAXBContext.newInstance(c);
            Unmarshaller un = context.createUnmarshaller();
            T obj = (T) un.unmarshal(par);
            return obj;
        } catch (JAXBException e) {
            System.out.println("Read: " +par.toString());
        }
        return null;
    }

    public void add(T object) {
        this.className = object.getClass().getName();
        this.directory = className + "." + index;
        this.c = object.getClass();
        this.size += 1;
        this.write(object);

    }

    public int size() {
        return size;
    }

    @Override
    public Iterator<T> iterator() {
        return new GenIt<T>();
    }

    private class GenIt<T> implements Iterator {

        private int cursor;

        public GenIt() {
            this.cursor = 0;
        }

        @Override
        public boolean hasNext() {
            return this.cursor < ListFile.this.size;
        }

        @Override
        public Object next() {
            if (this.hasNext()) {
                int current = cursor;
                cursor++;
                return (T) ListFile.this.read(current);
            }
            throw new NoSuchElementException();
        }

    }
}
