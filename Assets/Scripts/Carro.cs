using UnityEngine;

public class Carro : MonoBehaviour
{
    private Rigidbody corpo;
    public float forcaAceleracao = 30;
    public float limiteVelocidade = 100;
    public float velocidadeCurva = 120;

    void Awake()
    {
        corpo = GetComponent<Rigidbody>();

        corpo.collisionDetectionMode = CollisionDetectionMode.Continuous;
        corpo.interpolation = RigidbodyInterpolation.Interpolate;

        corpo.constraints = RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY;
        
    }
    private void FixedUpdate()
    {
        float velocidade = Input.GetAxisRaw("Vertical"); //W ou S /\ ou \/
        float girar = Input.GetAxisRaw("Horizontal"); //A ou D ou < ou >

        //Acelera
        corpo.AddForce(transform.forward * velocidade*forcaAceleracao,
            ForceMode.Acceleration);

        // Curvar
        float fatorVelocidade = Mathf.Clamp01
            (corpo.linearVelocity.magnitude / limiteVelocidade);
        float girando = girar * velocidadeCurva 
            * fatorVelocidade * Time.fixedDeltaTime;

        if(corpo.linearVelocity.magnitude > 0.2)
        {
            corpo.MoveRotation(corpo.rotation * 
                Quaternion.Euler(0, girando, 0));
        }
    }
}
