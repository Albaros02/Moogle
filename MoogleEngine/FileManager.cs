namespace MoogleEngine;
class FileManager{
    static string CurrentDir = Directory.GetParent(Directory.GetCurrentDirectory()).FullName; 
    static string subdir = "";
    public static DirectoryInfo dinfo = new DirectoryInfo(CurrentDir);
    public static List<string> allthedocs = new List<string>();
    
    //Metodo Readall:
    //Toma de parametro el nombre de la carpeta donde estan los documentos en la direccion actual
    //del proyecto.
    //Por cada documento que encuentre lo guarda como un string y los devuleve.
    public static List<string> Readall(string s){
        subdir = s;
        dinfo = new DirectoryInfo(CurrentDir+subdir);
        foreach(var file in dinfo.GetFiles())
        {
            string t = File.ReadAllText(file.FullName);
            allthedocs.Add(t);
        }
        return allthedocs;
    }
}