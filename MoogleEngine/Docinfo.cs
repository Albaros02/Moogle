namespace MoogleEngine;
public class Docinfo{
    public string document;
    public string snippet = "";
    public List<string> normalized = new List<string>();
    public string name;
    public List<float> TF = new List<float>();
    public float score = new float();  
    // Clase que guarda informacion importante de cada documento para acceder 
    // a ella con mayor facilidad
    public Docinfo(int indice, string doc){
        document = doc;
        name = "";
        name = FileManager.dinfo.GetFiles()[indice].Name.Substring(0,FileManager.dinfo.GetFiles()[indice].Name.Length-4);
        this.normalized = Normalizer.Normal(document,"");
    }
}