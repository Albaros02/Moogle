namespace MoogleEngine;
public class Suggestion{
    public string suggestion;
    public Suggestion(string query){
        suggestion = query;
        List<string> normalized  = new List<string>();
        bool sameq = true;
        foreach (var item in Normalizer.Normal(query,"~^*!"))
        {
            normalized.Add(item);
            if(!Program.dic.ContainsKey(item)){
                sameq = false;
            }
        }
        if(!sameq){
            List<string> sug = new List<string>();
            foreach (var item in normalized)
            {
                sug.Add(Levenshtein.MinLevenshteindis(Program.allthewords,item));
            }
            for (int i = 0; i < sug.Count; i++)
            {
                suggestion = suggestion.ToLower().Replace(normalized[i],sug[i]);
            }
        }    
        Console.WriteLine(suggestion);
    }
}