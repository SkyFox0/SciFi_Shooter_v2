using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ExplodeTarget : MonoBehaviour
{

#if UNITY_EDITOR || (!UNITY_EDITOR && !(UNITY_IPHONE || UNITY_ANDROID))
    //void OnMouseDown()
    //{
    //    _StartExplosion();
    //}
#endif

    //void Update()
    //{
      //  var thisCollider = GetComponent<Collider>();

    //    foreach (var i in Input.touches)
     //   {
    //        if (i.phase != TouchPhase.Began)
    //        {
    //            continue;
    //        }

            // It's kinda wasteful to do this raycast repeatedly for every ClickToExplode in the
            // scene, but since this component is just for testing I don't think it's worth the
            // bother to figure out some shared static solution.
     //       RaycastHit hit;
      //      if (!Physics.Raycast(Camera.main.ScreenPointToRay(i.position), out hit))
     //       {
     //           continue;
     //       }
     //       if (hit.collider != thisCollider)
     //       {
     //           continue;
      //      }

      //      _StartExplosion();
      //      return;
     //   }
   // }

    public void StartExplosion()
    {
        BroadcastMessage("Explode");
        GameObject.Destroy(gameObject);
    }

}
