using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyController : MonoBehaviour
{
    LineRenderer lr;
    [SerializeField]LayerMask GrappableMask;
    [SerializeField] float LassoRadius;
    [SerializeField] Transform LassoPos;
    [SerializeField] Transform LassoTarget;
    [SerializeField] SpringJoint joint;
    [SerializeField] bool LassoStuck;
    Rigidbody body;
    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        body = GetComponent<Rigidbody>();
    }
    //https://www.youtube.com/watch?v=Xgh4v1w5DxU

    private void Update()
    {
        

        if(Input.GetMouseButtonUp(0) && !LassoStuck)
        {
            LassoStuck = true;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Collider[] hitColliders = Physics.OverlapSphere(worldPosition, LassoRadius, GrappableMask);
           
            if (hitColliders.Length > 0)
            {
                //perhaps more catches if powerup in future
                
                GameObject Target = hitColliders[0].gameObject;

                joint = Target.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedBody = body;

                float distanceFromGrapple = Vector3.Distance(transform.position, Target.transform.position);

                joint.maxDistance = distanceFromGrapple * .8f;
                joint.minDistance = distanceFromGrapple * .25f;

                joint.spring = 6f;
                joint.damper = 5;
                joint.massScale = 5;

                LassoTarget = Target.transform;
                lr.positionCount = 2;
            }

        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        if (!joint) return;

        lr.SetPosition(0,LassoPos.position);
        lr.SetPosition(1, LassoTarget.position);

    }

}
