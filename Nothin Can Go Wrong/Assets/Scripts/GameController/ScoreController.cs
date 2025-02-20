using UnityEngine;

public class ScoreController : MonoBehaviour
{

    public static int vidaInicial;
    public static int vidaAnterior;
    public static double ultimoScore;
    void Start()
    {
        vidaAnterior=vidaInicial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void atualizarVida(double score){
    }
}
