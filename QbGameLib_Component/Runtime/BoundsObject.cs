using System;
using UnityEngine;

namespace QbGameLib.Component
{
    public class BoundsObject
    {
        private static Func<Collider2D, bool> EMPTY_COLLIDER2D_BOUNDS = collider => false;
        private static Func<Collider, bool> EMPTY_COLLIDER_BOUNDS = collider => false;
        private static Func<Vector3, bool> EMPTY_POSITION_BOUNDS = position => false;
        private static Func<Ray, bool> EMPTY_RAY_BOUNDS = ray => false;

        private Func<Collider2D, bool> _collider2DBoundsFunc = EMPTY_COLLIDER2D_BOUNDS;
        private Func<Collider, bool> _colliderBoundsFunc = EMPTY_COLLIDER_BOUNDS;
        private Func<Vector3, bool> _positionBoundsFunc = EMPTY_POSITION_BOUNDS;
        private Func<Ray, bool> _rayBoundsFunc = EMPTY_RAY_BOUNDS;

        public BoundsObject(GameObject gameObject)
        {
            Collider2D _collider;
            Collider _collider3d;
            SpriteRenderer _spriteRenderer;
            MeshRenderer _meshRenderer;

            if (gameObject.TryGetComponent(out _collider))
            {
                _collider2DBoundsFunc = collider => _collider.bounds.Intersects(collider.bounds);
                _colliderBoundsFunc = collider => _collider.bounds.Intersects(collider.bounds);
                _positionBoundsFunc = position => _collider.bounds.Contains(position);
                _rayBoundsFunc = ray => _collider.bounds.IntersectRay(ray);
            }
            else if (gameObject.TryGetComponent(out _spriteRenderer))
            {
                _collider2DBoundsFunc = collider => _spriteRenderer.bounds.Intersects(collider.bounds);
                _colliderBoundsFunc = collider => _spriteRenderer.bounds.Intersects(collider.bounds);
                _positionBoundsFunc = position => _spriteRenderer.bounds.Contains(position);
                _rayBoundsFunc = ray => _spriteRenderer.bounds.IntersectRay(ray);
            }
            else if (gameObject.TryGetComponent(out _collider3d))
            {
                _collider2DBoundsFunc = collider => _collider3d.bounds.Intersects(collider.bounds);
                _colliderBoundsFunc = collider => _collider3d.bounds.Intersects(collider.bounds);
                _positionBoundsFunc = position => _collider3d.bounds.Contains(position);
                _rayBoundsFunc = ray => _collider3d.bounds.IntersectRay(ray);
            }
            else if (gameObject.TryGetComponent(out _meshRenderer))
            {
                _collider2DBoundsFunc = collider => _meshRenderer.bounds.Intersects(collider.bounds);
                _colliderBoundsFunc = collider => _meshRenderer.bounds.Intersects(collider.bounds);
                _positionBoundsFunc = position => _meshRenderer.bounds.Contains(position);
                _rayBoundsFunc = ray => _meshRenderer.bounds.IntersectRay(ray);
            }
        }

        public bool IsBounds(Vector3 position) => _positionBoundsFunc.Invoke(position);
        public bool IsBounds(Collider2D collider) => _collider2DBoundsFunc.Invoke(collider);
        public bool IsBounds(Collider collider) => _colliderBoundsFunc.Invoke(collider);
        public bool IsBounds(Ray ray) => _rayBoundsFunc.Invoke(ray);
    }
}