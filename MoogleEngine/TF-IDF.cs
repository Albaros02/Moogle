namespace MoogleEngine;
public class TF_IDF{
    public List<float> idf;
    public List<float> TF(List<string> unv , List<string> doc){
        //Recibe como parametros el universo de palabras y las palabras del documento normalizado 
        List<float> result = new List<float>();
        for (int i = 0; i < unv.Count; i++)
            result.Add(0);
        for (int i = 0; i < doc.Count; i++)
        {
            if(Program.dic.ContainsKey(doc[i]))
                result[Program.dic[doc[i]]]++;
        }
        return result;
    }
    public List<float> IDF(List<List<string>>allthedocs , List<string> allthewords){
        //Recibe como parametros Todos los documentos sobre los que trabajar, y su universo de palabras.
        List<float> result = new List<float>();
        for (int i = 0; i < allthewords.Count; i++)
            result.Add(0);

        foreach (var item in allthedocs)
        {
            bool[] bl = new bool[allthewords.Count];
            for (int i = 0; i < allthewords.Count; i++)
            {
                bl[i] = false;
            }
            foreach (var i in item)
            {
                if(Program.dic.ContainsKey(i))
                bl[Program.dic[i]] = true;
            }
            for (int i = 0; i < allthewords.Count; i++)
            {
                if(bl[i]){
                    result[i]++;
                }
            }
        }
        for (int i = 0; i < allthewords.Count; i++)
        {
            if(result[i]!=0)
            result[i] = ((float)Math.Log10(((allthedocs.Count+1)/result[i])));
        }
        return result;
    }
    public TF_IDF(List<List<string>>allthedocs , List<string> allthewords){
        idf = new List<float>();
        idf = IDF( allthedocs , allthewords);
    }
}