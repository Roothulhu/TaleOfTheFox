using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Colisionador2D : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    // Variable pública que puedes modificar desde el Inspector
    public bool activarTrigger = false;

    void Start()
    {
        // Obtenemos el componente BoxCollider2D en el mismo objeto
        boxCollider = GetComponent<BoxCollider2D>();

        // Asignamos el valor de activarTrigger al estado de isTrigger del BoxCollider
        boxCollider.isTrigger = activarTrigger;
    }

    // Este método se llama cuando otro colisionador entra en el área de colisión
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Puedes usar "other" para acceder al objeto que colisionó
        Debug.Log("Colisión detectada con: " + other.gameObject.name);

        // Aquí puedes agregar tu lógica personalizada para manejar la colisión
    }

    // Este método se llama cuando el colisionador permanece dentro del área de colisión
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("El objeto " + other.gameObject.name + " sigue dentro de la colisión");
    }

    // Este método se llama cuando otro colisionador sale del área de colisión
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("El objeto " + other.gameObject.name + " ha salido de la colisión");
    }
}
