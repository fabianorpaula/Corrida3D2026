using UnityEngine;

public class Carro : MonoBehaviour
{
    private Rigidbody corpo;
    public float forcaAceleracao = 30;
    public float limiteVelocidade = 100;
    public float velocidadeCurva = 120;
    public float efeitoSolo = 25;

    void Awake()
    {
        corpo = GetComponent<Rigidbody>();

        corpo.collisionDetectionMode = CollisionDetectionMode.Continuous;
        corpo.interpolation = RigidbodyInterpolation.Interpolate;

        corpo.constraints = RigidbodyConstraints.FreezeRotationX 
            | RigidbodyConstraints.FreezeRotationY |
            RigidbodyConstraints.FreezeRotationZ;

    }
    private void FixedUpdate()
    {
        float velocidade = Input.GetAxisRaw("Vertical"); //W ou S /\ ou \/
        float girar = Input.GetAxisRaw("Horizontal"); //A ou D ou < ou >

        if (corpo.angularVelocity.magnitude < limiteVelocidade)
        {
            //Acelera
            corpo.AddForce(transform.forward * velocidade * forcaAceleracao,
                ForceMode.Acceleration);
        }
        corpo.AddForce(Vector3.down * efeitoSolo * corpo.angularVelocity.magnitude,
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
