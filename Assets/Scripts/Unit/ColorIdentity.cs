using UnityEngine;

public class ColorIdentity : MonoBehaviour
{
    [SerializeField] private ColorType _color;
    
    public ColorType Color => _color;
}
