using UnityEngine;

[CreateAssetMenu(fileName = "MouseLookParameters", menuName = "Player/MouseLookParameters")]
public class MouseLookParameters : ScriptableObject
{
    public float VerticalSensivity => _verticalSensivity;
    public float HorizontalSenivity => _horizontalSensivity;
    public float UpperVerticalRotationBorder => _upperVerticalRotationBorder;
    public float LowerVerticalRotationBorder => _lowerVerticalRotationBorder;

    [SerializeField] private float _verticalSensivity = 400;
    [SerializeField] private float _horizontalSensivity = 400;
    [Space(10), SerializeField] public float _upperVerticalRotationBorder = 90;
    [SerializeField] public float _lowerVerticalRotationBorder = -90;
}
