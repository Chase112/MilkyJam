using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StaticCacheLists
{
    public static readonly List<float> floatCache = new List<float>();
    public static readonly Collider2D[] colliderCache2D = new Collider2D[50];
    public static readonly RaycastHit2D[] raycastHitCache2D = new RaycastHit2D[50];
    
    public static readonly Collider[] colliderCache = new Collider[50];
    public static readonly RaycastHit[] raycastHitCache = new RaycastHit[50];

}
