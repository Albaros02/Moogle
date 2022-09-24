namespace MoogleEngine;
public class Ranking {
    public  List<float> scoreslist;
    //Constructor de la clase
    //Toma los vectores que represntan los documentos y el vector que 
    //represnta la consulta,
    //Calcula los scores de cada documento en funcion de la consulta 
    //y los guarda.
    public Ranking(List<List<float>>vectores,List<float> pivote){
        scoreslist = new List<float>();
        for (int i = 0; i < vectores.Count; i++)
        {
            scoreslist.Add(Cosin(vectores[i],pivote)); 
        }
    }
    //Metodo Cosin:
    //Toma dos vectores y devuelve el valor de "distancia" entre ellos.
    public float Cosin(List<float> v1 , List<float> v2){
        float result=0;
        int largo = v1.Count;
        float linealmultip = 0;
        for (int i = 0; i < largo; i++)
        {
            linealmultip+= v1[i]*v2[i];
        }
        float sumv1 = 0;
        float sumv2 = 0;
        for (int i = 0; i < largo; i++)
        {
            sumv1+=v1[i]*v1[i];
            sumv2+=v2[i]*v2[i];
        }
        float normav1 = ((float)Math.Sqrt(sumv1));
        float normav2 = ((float)Math.Sqrt(sumv2));
        if(normav1*normav2!=0)result = linealmultip/(normav1*normav2);
        return result;
    }
}