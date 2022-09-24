# Moogle!
> Proyecto de Programación I. Facultad de Matemática y Computación. Universidad de La Habana. Curso 2022.
>
>Albaro Suarez Valdes 

En general la busqueda de Moogle esta basada en primero tomar todos los docuementos, se "normalizan",
luego se les calcula sus TFIDF, despues se trabaja con los vectores TFIDF de los documentos y el obtenido
a partir de la query para encontrar los vectores mas cercanos al de la query que indican mayor coincidencia 
con lo deseado, despues se calcula el snippet de cada documento resultante de la busqueda y se mustran los
resultados.

### Acerca del tiempo.

En una base de datos de 45.4 mb con 203 documentos la primera busqueda (en el momento que carga los documentos) tarda alrededor 
de 10 segundos luego las consultas demoran alrededor de 3 segundos. Estos tiempos fueron calculados ejcutando el proyecto en un
ordenador con las siguientes propiedades: 
>-OS: Microsoft Windows 10 Pro                                                                 
>-CPU: Intel(R) Core(TM) i5-8250U CPU @ 1.60GHz   1.80 GHz                                     
>-RAM: 16.0 GB                                                                                 

### El proyecto cuenta con 11 clases adicionales.
Clases:


- Docinfo: Destinada a guardar informacion necesaria de cada documento con el obejtivo de acceder facilemente
    a ella en cualquier momento de la ejecucion. 
- FileManager: Lee los documentos en el directortio dado. 
- Levenshtein: Facilita la "distancia" que hay entre dos cadenas de caracteres empleando el algoritmo de Levenshtein.
- Normalizer: Toma una cadenas de texto y las separa en palabras.
- Operators: Aplica los operdores que se encuentre en la consulta a los resultados devueltos.
- Program: Clase principal, encargada de cargar los documentos en la primera busqueda que se realice.
- Query: Ordena el flujo de ejecucion de la consulta que se haga.
- Ranking: Calcula los scores de cada documento con respecto a la consulta actual.
- Snippet: Toma una seccion del documento en cuestion que cumpla cierta semejanza con la query.
- Suggestion: A partir de la consulta, brinda una sugerencia de cambio de palabra o palabras que no devuelvan resultados significativos para la busqueda.Usa el algoritmo de Levenshtein
- TF-IDF: Crea vectores TF o IDF a los documentos.

Luego de iniciado el servidor, con la primera busqueda se carga el programa. Este llama el metodo Main() de la clase Program.

## Clases de mayor interes.
### Program
>Clase Program.
Clase principal del motor de busqueda.
Tiene 8 propiedades:
>- public static bool precomputed. Propiedad que toma valor true luego de cargado el programa
y evita cargarlo nuevamente.
>- public static List<Docinfo> docs. Guarda la inforamcion de cada documento.
>- public static Dictionary<string,int> dic. Tiene la relacion de palabra-indice guardada, para acceder con facilidad
al indice de una palabra especifica en los vectores.
>- public static List<string> allthewords. Lista que contiene todas las palabras normalizadas de todos los documentos 
esta es usada como molde para los vectores de TFIDF.
>- public static int Vectorsize. Contiene el size de los vectores.
>- public static TF_IDF tfsidfs. Instancia de los TFIDF de los documentos.
>- public static List<List<float>> Vectores. Vectores TFIDF.
>- public static List<float> documentsidf. IDF de las palabras de >los documentos.
Tiene 1 metodo:

- Main(). Es invocado en la primera busqueda del Moogle, es encaragdo de calcular los TFSIDFS de todos los documentos.
Primero invoca el metodo Readall() de la clase FileManager, encargado de leer todos los documentos en la direccion dada,
despues documento por documento guarda en variables tipo Docinfo, clase encargada de tener informacion especifica de cada
documento, los TF, nombre, indice, y palabras de cada documento.

Ya cargados los documentos crea una nueva instancia en MoogleEngine.Moogle de la clase Query.
### Query
>Clase Query.
Tiene 2 propiedades.
>- public int[] indices. Se guardan los indices de los documentos los cuales van a ser ordenados en cuestion del score del docmuento
del indice espacificado.
>- public string suggestion. Se guarda la posible sugerencia de busqueda en caso de existir palabras de la query que no existan en los 
documentos.

- Constructor de la clase.
Toma como parametro la query, la normaliza, analiza el caso de una posible sugerencia, crea una instancia de la clase Ranking, encargada
de calcular los scores de los documentos, despues le aplica los operadores a cada documento crando una nueva instancia de la clase 
Operators encargada de gestionar cada operador que se intoduzca a la busqueda, le sigue ordenar los scores, y por ultimo calcula los
snippets de cada uno de los 10 primeros resultados de la busqueda.
### Snippet
>Clase Snippet
Toma un documento, lo divide a la mitad, luego a los dos nuevos documentos resultantes calcula sus scores con respecto a la query, el 
documento de mayor score se le vuelve a repetir este proceso recursivamente, hasta llegar a un documento con no mas de 805 caracteres,
ese sera el valor de la variable privada snip que determina el snippet del documento.






























