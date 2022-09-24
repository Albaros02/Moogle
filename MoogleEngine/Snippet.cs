namespace MoogleEngine;
class Snippet{
    public string snip; 
    //Cosntructor de la clase Snippet
    //
    public Snippet(Docinfo doc , List<string> query){
        snip = "";
        snip = Snippetsearch(doc.document,query);
        snip = "..."+snip+"...";
    }
    private string Snippetsearch(string doc ,List<string> query){
        string result = "";
        if(doc.Length<=805){
            result = doc;
        }else{
            int mid  = doc.Length/2;
            string docizq = doc.Substring(0,mid+400);
            string docder = doc.Substring(mid-400,doc.Length-(mid-400));
            List<List<string>> docs = new List<List<string>>();
            docs.Add(Normalizer.Normal(docizq,""));
            docs.Add(Normalizer.Normal(docder,""));
            if(Scoredoc(docs,query)){
                result = Snippetsearch(docizq,query);
            }else{
                result = Snippetsearch(docder,query);
            }
        }
        return result;
    }
    private bool Scoredoc(List<List<string>> doc,List<string> query){
        bool result;
        //true izquierda, false derecha
        //List<string> normalized = Normalizer.Normal(doc,"");
        TF_IDF alpha = new TF_IDF(doc,Program.allthewords);

        List<float> tfiz =  alpha.TF(Program.allthewords,doc.First());
        List<float> tfder = alpha.TF(Program.allthewords,doc.Last());
        List<float> tfq = alpha.TF(Program.allthewords,query);

        List<float> veciz = new List<float>();
        List<float> vecder = new List<float>();
        List<float> vecq = new List<float>();

        for (int i = 0; i < Program.allthewords.Count; i++)
        {
            veciz.Add(tfiz[i]*alpha.idf[i]);
            vecder.Add(tfder[i]*alpha.idf[i]);
            vecq.Add(tfq[i]*alpha.idf[i]);
        }

        List<List<float>> vecs = new List<List<float>>();
        vecs.Add(veciz);
        vecs.Add(vecder);
        Ranking newr = new Ranking(vecs,vecq);

        if(newr.scoreslist.First()>=newr.scoreslist.Last()){
            result = true;
        }else{
            result = false;
        }
        return result;
    }
}