namespace MoogleEngine;


public static class Moogle
{
    //Metodo Query:
    //Toma la consulta como parametro.
    //Crea una nueva instancia de la clase Query, de aqui toma la sugerencia,
    //los resultados y sus respectivos snippets, los guarda en un arreglo del 
    //tipo SearchItem.
    //Devuelve una nueva instncia del tipo Searchresult
    public static SearchResult Query(string query) {
        Query newq = new Query(query);
        string sug = "";
        if(newq.suggestion!=query){
            sug = newq.suggestion;
        }
        List<SearchItem> results = new List<SearchItem>();
     
        for (int i = Program.docs.Count-1; i >= 0; i--)
        {
            if(Program.docs[newq.indices[i]].score >= 0.000001){  
                results.Add(new SearchItem(Program.docs[newq.indices[i]].name,Program.docs[newq.indices[i]].snippet,Program.docs[newq.indices[i]].score));
            }
        }
        SearchItem[] items = new SearchItem[results.Count];
        for (int i = 0; i < results.Count; i++)
        {
            items[i] = results[i];
        }
        
        //Solo tomo los 10 primeros
        SearchItem[] newitems = new SearchItem[10];
        if(items.Length>10){
            for (int i = 0; i < 10; i++)
            {
                newitems[i] = items[i];
            }
            items = newitems;
        }
        if(items.Length==0){
            items = new SearchItem[1];
            items[0] = new SearchItem("No results found","",0);
        }

        return new SearchResult(items, sug);
    }
}
