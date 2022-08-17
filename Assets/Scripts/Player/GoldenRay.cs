using System.Collections.Generic;
using UnityEngine;

public class GoldenRay : MonoBehaviour
{
  [SerializeField] private LineRenderer line;
  [SerializeField] private EdgeCollider2D collider2D;
  [SerializeField]private List<Vector2> _linePoints = new List<Vector2>(2);
  
  
  private void Update()
  {
    _linePoints[0] = line.GetPosition(0);
    _linePoints[1] = line.GetPosition(1);

    collider2D.SetPoints(_linePoints);
  }
}
