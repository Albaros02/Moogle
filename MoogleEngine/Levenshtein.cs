namespace MoogleEngine;
public class Levenshtein{
    //Metodo Levenshteindis:
    //Recibe como parametros dos strings y devuelve la distancia entre ellos.
    public static long Levenshteindis(string s ,string t){
        long respuesta  = -1;
        int lens = s.Length;
        int lent = t.Length;
        int [,] m = new int[lens+1,lent+1];
        for (int i = 1; i < lens+1; i++)
        {
            m[i,0] = i;
        }
        for (int i = 1; i < lent+1; i++)
        {
            m[0,i] = i;

        }
        for (int i = 1; i < lens+1; i++)
        {
            for (int j = 1; j < lent+1; j++)
            {
                if(s[i-1]==t[j-1]){
                    m[i,j] = m[i-1,j-1];
                }
                else{
                    m[i,j] = Math.Min(Math.Min(m[i-1,j]+1,m[i,j-1]+1),m[i-1,j-1]+1);
                }
            }
        }
        respuesta = m[lens,lent];
        return respuesta;
    }
    //Metodo MinLevenshteindis:
    //Toma como parametros un grupo de palabras y una palabra que usara de pivote
    //Devuelve del grupo de palabras la que tenga menor distancia con el pivote
    //usando el metodo Levenshteindis.
    public static string MinLevenshteindis(List<string> words, string s){
        string t = "";
        long mindistance = long.MaxValue;
        foreach (var item in words)
        {
            long aux = Levenshteindis(item,s);
            if(mindistance > aux){
                mindistance = aux;
                t = item;
            }    
        }
        return t;
    }
}