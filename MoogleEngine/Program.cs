namespace MoogleEngine;
static public class Program{
    public static bool precomputed = false;
    public static List<Docinfo> docs = new List<Docinfo>();
    public static Dictionary<string,int> dic = new Dictionary<string, int>();
    public static List<string> allthewords = new List<string>();
    public static int Vectorsize ;
    public static TF_IDF tfsidfs; 
    public static List<List<float>> Vectores;
    public static List<float> documentsidf ;
    //Clase Program
    //Metodo Main:
    //Destinado para "cargar" los documentos, crea un grupo de de vectores donde guarda los 
    //tf , idf de los documentos , tambien se crea un diccionario que guarda la relacion 
    //palabra-indice de cada palabra con su indice en el vector de los tfidfs.
        public static void Main(){
            if (!precomputed)
            {
                precomputed = true;
            List<List<string>> normalized = new List<List<string>>();
            int k = 0;

            foreach (var item in FileManager.Readall(@"\Content"))
            {
                Docinfo inf = new Docinfo(k,item);
                docs.Add(inf);
                k++;
                inf.TF = new List<float>();
                for (int i = 0; i < allthewords.Count; i++)
                {
                    inf.TF.Add(0);
                }
                for (int i = 0; i < inf.normalized.Count; i++)
                {
                    string s = inf.normalized[i];
                    if(!dic.ContainsKey(s)){
                        allthewords.Add(s);
                        dic.Add(s,allthewords.Count-1);
                        inf.TF.Add(1);
                    }else{
                        inf.TF[dic[s]]++;
                    }
                }   
                normalized.Add(inf.normalized);
            }

            Vectorsize = allthewords.Count;
            tfsidfs  = new TF_IDF(normalized,allthewords);

            foreach (var item in docs)
            {
                int oldsize = item.TF.Count;
                for (int i = 0; i < Vectorsize - oldsize; i++)
                {
                    item.TF.Add(0);
                }
            }

            documentsidf = new List<float>();
            documentsidf = tfsidfs.idf;

            Vectores = new List<List<float>>();
            foreach (var item in docs)
            {
                Vectores.Add(new List<float>());
                for (int i = 0; i < Vectorsize; i++)
                {
                Vectores.Last().Add(item.TF[i]*documentsidf[i]);
                }
            }
            }
        }
    }
