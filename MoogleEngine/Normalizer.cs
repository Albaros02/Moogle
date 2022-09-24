namespace MoogleEngine;
class Normalizer{
    //Metodo Normal:
    //Toma como parametros un string y un grupo extra de simbolos para agregar al criterio
    //con el que se separa el string.
    //Devuelve una lista con el resultado de "normalizar" el string, osea una lista de strings
    //donde cada uno representa una palabra.
public static List<string> Normal(string document, string moresimbols){
        List<string> result = new List<string>();
        string [] s = document.Split((@"#%&'@#$%&()_+,-./:;?@<>[]_{}¡§«¶·»¿ \="+"\t"+"\r"+"\n"+@""""+moresimbols).ToCharArray(),StringSplitOptions.RemoveEmptyEntries);
        foreach (var item in s)
        {
            result.Add(item.ToLower());
        }
        return result;
    }
}
