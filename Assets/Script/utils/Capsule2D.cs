
using UnityEngine;
using System;


[Serializable]
public struct Capsule2D
{
    [SerializeField]
    Vector2 _offset;
    [SerializeField]
    Vector2 _size;
    [SerializeField]
    CapsuleDirection2D _direction;

    public Vector2 offset => _offset;
    public Vector2 size => _size;
    public CapsuleDirection2D direction => _direction;

    public Capsule2D(Vector2 point = default, Vector2 size = default, CapsuleDirection2D direction = default)
    {
        _offset = point;
        _size = size;
        _direction = direction;
    }

    public static implicit operator (Vector2 point, Vector2 size, CapsuleDirection2D direction)(Capsule2D capsule)
    {
        return (capsule._offset, capsule._size, capsule._direction);
    }
}