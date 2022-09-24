namespace MoogleEngine;
public class Query{
    public int[] indices = new int[Program.docs.Count];  
    public string suggestion; 
    public Query(string query){

        //Hallo la sugerencia 
        Suggestion s = new Suggestion(query);
        suggestion = s.suggestion;
        List<float> normalized = Program.tfsidfs.TF(Program.allthewords,Normalizer.Normal(query,"!^*~"));

        //Normalizando la entrada
        List<float> pivote = new List<float>();
        for (int i = 0; i < Program.Vectorsize; i++)
        {
            pivote.Add(Program.documentsidf[i]*normalized[i]);
        }

        //Rankeo los documentos
        Ranking ranked = new Ranking(Program.Vectores,normalized);
        for (int i = 0; i < Program.docs.Count; i++)
        {
            Program.docs[i].score = ranked.scoreslist[i];
        }

        //Operadores 
        Operators op = new Operators(query);
        for (int i = 0; i < Program.docs.Count; i++)
        {
            Program.docs[i].score = op.smartsearch(i);
            Console.WriteLine(Program.docs[i].score);
        }

        //Ordenarlos 
        float[] scores = new float[Program.docs.Count];
        for (int i = 0; i < Program.docs.Count; i++)
        {
            scores[i]=Program.docs[i].score;
            indices[i]=i;
        }
        Array.Sort(scores,indices);
            
        //Snippet
        foreach (var item in Program.docs)
        {
            item.snippet="";
        }
        int cont = 0;
        for (int i = scores.Length-1 ; i >= 0; i--)
        {
            if(cont==10)break; 
            Snippet sp = new Snippet(Program.docs[indices[i]],Normalizer.Normal(query,"!~^*")); 
            foreach (var item in Normalizer.Normal(query,"!~^*"))
            {
                Console.WriteLine(item);
                sp.snip = sp.snip.Replace(" "+item,@"<mark style = \"+'"'+"background:"+@"#ffea02\"+'"'+"> "+item+"</mark>");
            } 
            Program.docs[indices[i]].snippet = sp.snip;
            cont++;
        }
    }
}