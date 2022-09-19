using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Fence> fences_ = new List<Fence>();
    [SerializeField] List<Sunflora> sunfloras_ = new List<Sunflora>();
    [SerializeField] NavMeshSurface2d surf;
    [SerializeField] int _surfEvent;
    public int totalF;
    bool _once;
    bool _end;
    

    #region Encapsulamento
    public List<Fence> fences
    {
        get { return fences_; }
        set { fences_ = value; }
    }

    public List<Sunflora> sunfloras
    {
        get { return sunfloras_; }
        set { sunfloras_ = value; }
    }

    public bool once
    {
        get { return _once; }
        set { _once = value; }
    }

    public bool end
    {
        get { return _end; }
        set { _end = value; }
    }

    public int surfEvent
    {
        get { return _surfEvent; }
        set { _surfEvent = value; }
    }

    #endregion
    private void Awake()
    {
        SetFences();
        SetSunflora(ref sunfloras_);
    }

    private void Start()
    {
        totalF = fences_.Count;
        end = true;
        once = false;
    }

    void Update()
    {
        /*if(fences_.Count > (totalF - 4))
        {
            foreach (Fence fence in fences_)
            {
                if (fence == null)
                {
                    fences_.Remove(fence);
                }
            }
        }
        else
        {*/

        

        if(fences_.Count <= (totalF - surfEvent))
        {
            if (!once)
            {
                //ativando as fences como obstaculos
                for (int i = 0; i < fences.Count; i++)
                {
                    fences[i].Mod.overrideArea = true;
                }

                once = true;

                //rebuildando a NavMesh "bake"
                surf.BuildNavMesh();
                once = true;
            }
            if (sunfloras.Count <= 0)
            {
                end = false;
            }
        }
            /*foreach (Sunflora sunflora in sunfloras_)
            {
                if (sunflora == null)
                {
                    sunfloras_.Remove(sunflora);
                }
            }*/
        //}
    }

    

    void SetFences()
    {
        Fence[] fences = FindObjectsOfType<Fence>();
        for(int i = 0; i < fences.Length; i++)
        {
            fences_.Add(fences[i]);
        }
    }

    void SetSunflora(ref List<Sunflora> list)
    {
        Sunflora[] sunfloras = FindObjectsOfType<Sunflora>();
        for (int i = 0; i < sunfloras.Length; i++)
        {
            list.Add(sunfloras[i]);
        }
    }
}
