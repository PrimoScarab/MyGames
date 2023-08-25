using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SimpleCaster : MonoBehaviour
{
    public enum CastType
    {
        Ray,
        Sphere
    };

    public enum CastTrigger
    {
        OnStart,
        OnEnable,
        OnDisable,
        Manual,

        ButtonHeld,    
        ButtonPressed,
        ButtonReleased,

        Interval
       
    };

    public enum ActionMode
    {
        Once,
        EveryTime//,
        //OncePerHit
    };
    
    [SerializeField]
    public Color color = Color.white;
    
    [SerializeField]
    public Transform target;

    [SerializeField]
    public CastType castType;

    [SerializeField]
    public float sphereRadius = 1.0f;

    //[SerializeField]
    protected float maxDistance = 1000f;
     
    

    [SerializeField]
    public LayerMask layerMask = ~0;

    [SerializeField]
    public CastTrigger trigger;

    [SerializeField]
    public float castIntervalInSeconds = 1.0f;

    [SerializeField]
    public string triggerButttonName;

    [SerializeField]
    public Transform origin;

    [SerializeField]
    public bool originFromMouse = false;

    [SerializeField]
    public new Camera camera;

    //[SerializeField]
    protected float delay;

    [SerializeField]
    ActionMode actionMode = ActionMode.Once;

    protected float nextCastTime;

    protected bool targetHasBeenHit = false;
    protected bool previousTargetHasBeenHit = false;

    


    [SerializeField]
    UnityEvent actions;

    bool actionHasBeenInvoked = false;

     // Start is called before the first frame update
    void Start()
    {
        nextCastTime = Time.realtimeSinceStartup;

        TryTrigger(CastTrigger.OnStart);
    }

    void LateUpdate()
    {
        
        targetHasBeenHit = false;

        CheckInput();

        if(trigger == CastTrigger.Interval && (Time.realtimeSinceStartup >= nextCastTime))
        {
            nextCastTime = Time.realtimeSinceStartup + castIntervalInSeconds;
            TryTrigger(CastTrigger.Interval);
        }

        previousTargetHasBeenHit = targetHasBeenHit; 

    }

    void OnDisable()
    {
        TryTrigger(CastTrigger.OnDisable);
    }

    void OnEnable()
    { 
        TryTrigger(CastTrigger.OnEnable);
    }

    public void ManualTrigger()
    {
        TryTrigger(CastTrigger.Manual);
    }


    public bool TryTrigger(CastTrigger trigger)
    {
        //TODO: Fix the rest from when using arrays
        if(this.trigger == trigger)
            Cast();
        else
            return false;

        return true;
    }

    protected void Cast()
    {
        if(originFromMouse)
            CastFromCamera();
        else
            CastFromOrigin();
    }

    public void CheckInput()
    {
        switch (trigger)
        {
            case CastTrigger.ButtonHeld:
                if(Input.GetButton(triggerButttonName))
                    Cast();
            break;

            case CastTrigger.ButtonPressed:
                if(Input.GetButtonDown(triggerButttonName))
                    Cast();
            break;
            
            case CastTrigger.ButtonReleased:
                if(Input.GetButtonUp(triggerButttonName))
                    Cast();
            break;

            default:
                return;
        }
    }

#if UNITY_EDITOR
    float hitScale = 0.3f;
    Color hitColor = Color.red;
    Color rayColor = Color.cyan;

    float hitDrawTime = 1.0f;
    float rayDrawTime = 0.5f;
#endif

    protected void CastFromCamera()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        float distance = camera.farClipPlane;
        maxDistance = distance;

        switch (castType)
        {
            case CastType.Ray:
            {
                #if UNITY_EDITOR
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, rayColor, rayDrawTime);
                #endif
                if(Physics.Raycast(ray, out hit, distance, layerMask))
                {
                    Debug.Log("Camera raycast hit" + hit.transform.name);

                    #if UNITY_EDITOR
                    
                    Debug.DrawLine(hit.point - Vector3.forward*hitScale, hit.point + Vector3.forward*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.right*hitScale, hit.point + Vector3.right*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.up*hitScale, hit.point + Vector3.up*hitScale, hitColor, hitDrawTime);
                    #endif
                    if(hit.transform == target)
                    {
                        targetHasBeenHit = true;
                        Debug.Log("Camera raycast hit the target " + target.name);
                        DoActions();
                    }
                }
            }
            break;

            case CastType.Sphere:
            {
                #if UNITY_EDITOR
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, rayColor, rayDrawTime);
                #endif
                float radius = sphereRadius;// * 0.5f;
                if(Physics.SphereCast(ray, radius, out hit, distance, layerMask))
                {
                    Debug.Log("Camera speherecast hit" + hit.transform.name);
                    
                    #if UNITY_EDITOR
                    
                    Debug.DrawLine(hit.point - Vector3.forward*hitScale, hit.point + Vector3.forward*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.right*hitScale, hit.point + Vector3.right*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.up*hitScale, hit.point + Vector3.up*hitScale, hitColor, hitDrawTime);
                    #endif
                    if(hit.transform == target)
                    {
                        targetHasBeenHit = true;
                        Debug.Log("Camera spherecast hit the target " + target.name);
                        DoActions();
                    }
                }
            }
            break;
        }
    }

    protected void CastFromOrigin()
    {
        RaycastHit hit;
        Vector3 toTarget = target.transform.position - origin.position;
        maxDistance = 100.0f;//toTarget.magnitude;
        float radius = sphereRadius;// * 0.5f;

        switch (castType)
        {
            case CastType.Ray:
            {
                #if UNITY_EDITOR
                Debug.DrawLine(origin.position, origin.position + toTarget.normalized * maxDistance, rayColor, rayDrawTime);
                #endif
                if(Physics.Raycast(origin.position, toTarget.normalized, out hit, maxDistance, layerMask))
                {
                    Debug.Log("Raycast hit" + hit.transform.name);

                    #if UNITY_EDITOR
                    
                    Debug.DrawLine(hit.point - Vector3.forward*hitScale, hit.point + Vector3.forward*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.right*hitScale, hit.point + Vector3.right*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.up*hitScale, hit.point + Vector3.up*hitScale, hitColor, hitDrawTime);
                    #endif

                    if(hit.transform == target)
                    {
                        Debug.Log("Raycast hit the target " + target.name);
                        targetHasBeenHit = true;
                        DoActions();
                    }
                }
            }
            break;

            case CastType.Sphere:
            {
                #if UNITY_EDITOR
                 Debug.DrawLine(origin.position, origin.position + toTarget.normalized * maxDistance, rayColor, rayDrawTime);
                #endif
                if(Physics.SphereCast(origin.position, radius, toTarget.normalized, out hit, maxDistance, layerMask))
                {
                    Debug.Log("Spherecast hit" + hit.transform.name);
                    #if UNITY_EDITOR
                    
                    Debug.DrawLine(hit.point - Vector3.forward*hitScale, hit.point + Vector3.forward*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.right*hitScale, hit.point + Vector3.right*hitScale, hitColor, hitDrawTime);
                    Debug.DrawLine(hit.point - Vector3.up*hitScale, hit.point + Vector3.up*hitScale, hitColor, hitDrawTime);
                    #endif

                    if(hit.transform == target)
                    {
                        Debug.Log("Spherecast hit the target " + target.name);
                        targetHasBeenHit = true;
                        DoActions();
                    }
                    
                }
            }
            break;
        }
    }

    protected void DoActions()
    {
        if(actionMode == ActionMode.Once && actionHasBeenInvoked)
            return;
        /*else if(actionMode == ActionMode.OncePerHit)
        {
            if(previousTargetHasBeenHit == true && targetHasBeenHit == true)
                return;
            else if(previousTargetHasBeenHit == true && targetHasBeenHit == false)
                return;
        }*/
        
        Debug.Log("Invoking actions");
        actions.Invoke();
        actionHasBeenInvoked = true;
    }

    
    void OnDrawGizmosSelected()
    {
        DrawGizmosSelected();
        DrawConnectedTransforms();
    }

    void OnDrawGizmos()
    {
        //DrawGizmos();
    }


    public void DrawConnectedTransforms()
    {
        #if UNITY_EDITOR
        //Vector3 toTarget = target.transform.position - transform.position;
        Handles.color = Color.green;
        Handles.DrawDottedLine(transform.position, target.position, 3.0f);

        if(!originFromMouse)
            Handles.DrawDottedLine(transform.position, origin.position, 3.0f);
        else
            Handles.DrawDottedLine(transform.position, camera.transform.position, 3.0f);
        //Gizmos.DrawLine(originPosition, originPosition + direction * distance);
        #endif
    }

    public void DrawGizmosSelected()
    {
        #if UNITY_EDITOR
        if(!origin || !target)
            return;
            
        Vector3 toTarget = target.transform.position - origin.position;
        Gizmos.color = color;
        Vector3 originPosition;
        if(!originFromMouse)
            originPosition = origin.position;
        else
            originPosition = camera.transform.position;

        Vector3 direction = toTarget.normalized;
        float distance = maxDistance;

        if(originFromMouse)
        {
            //originPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            originPosition = ray.origin;
            direction = ray.direction;
            //distance = 50f;//Vector3.Distance(originPosition, target.position);
        }


        switch (castType)
        {
            case CastType.Ray:
            {
                //distance = toTarget.magnitude;
                Gizmos.DrawLine(originPosition, originPosition + direction * distance);
            }
            break;

            case CastType.Sphere:
            {
                //distance = toTarget.magnitude;
                float radius = sphereRadius;//*0.5f;
                float steps = distance/radius;
                float d = 0.0f;
               
                Vector3 normal = target.transform.position - originPosition;
                Vector3 curPos = originPosition;
                for(int i = 0; i< (int)(steps + 0.5f); i++)
                {
                    Handles.DrawWireDisc(curPos, normal.normalized, radius);
                    d += radius;
                    curPos = originPosition + (direction * d);
                }
            }
            break;
            
        }
        #endif
    }
  
    
}
