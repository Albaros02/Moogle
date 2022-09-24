namespace MoogleEngine;
public class Operators{
    public  List<string> opexcl = new List<string>();
    static List<string> opexist = new List<string>();
    static List<KeyValuePair<string,int>> opast = new List<KeyValuePair<string, int>>();
    static List<KeyValuePair<string,string>> opclose = new List<KeyValuePair<string, string>>();
    static List<string> regular = new List<string>();

    //Constructor de Operators:
    //Toma como parametro la query, y va agregando cada uno de los operadores encontrados a
    //las variables de cada uno respectivamente (opexcl,opast,opexist,opclose)

    public  Operators(string query){
        List<string> normalizedwithoperators  = Normalizer.Normal(query , "");
        opexcl = new List<string>();
        opast = new List<KeyValuePair<string, int>>();
        opexist = new List<string>();
        opclose = new List<KeyValuePair<string, string>>();
        for (int i = 0; i < normalizedwithoperators.Count; i++)
        {
            string item = normalizedwithoperators[i];
            int j = 0;
            while(j<item.Length && item[j]=='*'){
                j++;
            }
            if(j==item.Length){
                if(i+1<normalizedwithoperators.Count){
                    normalizedwithoperators[i+1] = item + normalizedwithoperators[i+1];
                    normalizedwithoperators.Remove(item);
                    i--;
                }
                continue;
            }
            if(item=="!" || item=="^"){
                if(i+1<normalizedwithoperators.Count){
                    normalizedwithoperators[i+1] = item + normalizedwithoperators[i+1];
                    normalizedwithoperators.Remove(item);  
                    i--; 
                } 
                continue;
            }
            if(item=="~"){
                if(i+1<normalizedwithoperators.Count && i>=1){
                    string a =normalizedwithoperators[i-1];
                    string b =normalizedwithoperators[i+1];
                    normalizedwithoperators.Add(a + item + b);
                }
                continue;
            }
        }

        foreach (var item in normalizedwithoperators)
        {
            if(query.Contains("~")){
                string first = "" , second = "";
                List<string> words = query.Split('~',100,StringSplitOptions.RemoveEmptyEntries).ToList();
                if(words.Count()>1)
                    for (int i = 1; i < words.Count; i++)
                    {
                        first = Normalizer.Normal(words[i-1],"~!^*").Last();
                        second = Normalizer.Normal(words[i],"~!^*").First();
                        opclose.Add(KeyValuePair.Create(first,second));
                    }
            }
            if(item[0]=='!'){
                opexcl.Add(item.Remove(0,1));
                continue;
            }
            else 
            if(item[0]=='^'){
                opexist.Add(item.Remove(0,1));
            }
            else
            if(item[0]=='*'){
                int howmany = 0;
                for (int i = 0; i < item.Length; i++)
                {
                    if(item[i]!='*'){
                        break;
                    }
                    howmany++;        
                }
                if(item.Remove(0,howmany)!=""){
                    opast.Add(KeyValuePair.Create(item.Remove(0,howmany),howmany));
                }else{
                    
                }
            }else{
                regular.Add(item);
            }
        }
    }

    //Metodo smartsearch:
    //Su funcion es modificar el score de cada documento devuelto como resultado de busqueda
    //mediante los operadores presentes en la query.
    //Toma como parametro el indice del documento en cuestion, y le aplica cambios a su valor 
    //de score respectivamente.
    public  float smartsearch(int indice){
        float result = Program.docs[indice].score;
        foreach (var item in opexcl)
        {
            if(Program.docs[indice].normalized.Contains(item)){
                return result = 0;
            }
        }
        foreach (var item in opexist)
        {
            if(!Program.docs[indice].normalized.Contains(item)){
                return result = 0;
            }
        }
        foreach(var item in opast)
        {
            if(Program.docs[indice].normalized.Contains(item.Key)){
                result*=Program.docs[indice].TF[Program.dic[item.Key]] * (item.Value+1);
            }
        }
        foreach (var item in opclose)
        {
            string first = item.Key;
            string second = item.Value;
            if(!Program.docs[indice].normalized.Contains(first) || !Program.docs[indice].normalized.Contains(second)){
                return result = 0;
            }
            int min = int.MaxValue-10;
            int pointleft = 0;
            int pointright = 0;
            List<int> pos = new List<int>();
            for (int i = 0; i < Program.docs[indice].normalized.Count; i++)
            {
                string word = Program.docs[indice].normalized[i];
                if(word==first || word==second){
                                pos.Add(i);
                }
            }
            for (int i = 0; i < pos.Count-1; i++)
            {
                pointleft = pos[i];
                pointright = pos[i+1];
                    if(pointleft<pointright && Program.docs[indice].normalized[pointleft]!=Program.docs[indice].normalized[pointright]){
                        int temp = 0;
                        for (int j = pointleft+1; j < pointright; j++)
                        {
                            temp+=Program.docs[indice].normalized[j].Length;
                        }
                        min = Math.Min(min,temp);
                    }
            }     
            result +=100*result/(min+1);
        }
        return result ;
    }
}