using UnityEngine;

public class ModulerRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Hangi eksende donmesini sagladigim yer 0,1 deðerleri ile")]
    [SerializeField] private Vector3 rotationAxis = new Vector3(0, 1, 0); 

    [Tooltip("Donme hizi")]
    [SerializeField] private float rotationSpeed = 50f;

    [Tooltip("Self kendi ekseni | World dunya ekseni")]
    [SerializeField] private Space relativeTo = Space.Self;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, relativeTo);
    }
}