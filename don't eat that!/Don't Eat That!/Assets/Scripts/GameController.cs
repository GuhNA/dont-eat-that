using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Fence> fences_ = new List<Fence>();
    [SerializeField] List<Sunflora> sunfloras_ = new List<Sunflora>();
    [SerializeField] NavMeshSurface2d surf;
    [SerializeField] int _surfEvent;
    public int totalF;
    public float timerP;
    public float timerZ;
    float timertemp;
    float timertempZ;
    int index;
    int indexZ;
    bool _once;
    bool _end;

    [Header("HUD")]
    public List<Sprite> plant;
    public List<Sprite> zum;
    public Image sun;
    public Image zombie;
    public Text numZ;
    public Text numSun;
    public Text timeS;
    public Text timeM;
    public float deadEnd;
    public Text scoreT;

    [Header("Score")]
    public int score;

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
        timertemp = timerP;
        timertempZ = timerZ;
        index = 0;
        indexZ = 0;
        score = 0;
    }

    void Update()
    {
        if(end)
        {
            deadEnd = Mathf.Clamp(deadEnd, 0, 600);
            deadEnd -= Time.deltaTime;

            if (fences_.Count <= (totalF - surfEvent))
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

            if (deadEnd <= 0)
            {
                score += sunfloras.Count * 300 + (fences.Count - (totalF - surfEvent)) * 200;

                end = !end;
            }
        }
        else
        {
            PlayerPrefs.SetInt("score", score);
            SceneManager.LoadScene("GameOver");
        }
        HUDSystem();

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

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

    void HUDSystem()
    {

        AnimHUD_P();
        AnimHUD_Z();
        DeadEndTime();

        scoreT.text = "Score: " + score; 

    }

    void DeadEndTime()
    {
        int min;
        int seg;

        min = (int)(deadEnd / 60);
        seg = (int)(deadEnd % 60);


        if(seg < 10)
        {
            timeS.text = "0"+seg.ToString();
        }
        else
        {
            timeS.text = seg.ToString();
        }
        if (min < 10)
        {
            timeM.text = "0" + min.ToString();
        }
        else
        {
            timeM.text = min.ToString();
        }
    }

    void AnimHUD_P()
    {
        //timerP precisa ser exatamente o valor da animação ( 1/samples ).
        timerP -= Time.deltaTime;
        //planta animada
        if (timerP <= 0)
        {
            sun.sprite = plant[index];
            index++;
            timerP = timertemp;
        }

        if (index > (plant.Count-1))
        {
            index = 0;
        }

        numSun.text = sunfloras.Count.ToString();
    }

    void AnimHUD_Z()
    {
        int numberZ = FindObjectsOfType<ZombieWM>().Length;
        timerZ -= Time.deltaTime;
        //Zumbi animada
        if (timerZ <= 0)
        {
            zombie.sprite = zum[indexZ];
            indexZ++;
            timerZ = timertempZ;
        }

        if (indexZ > (zum.Count-1))
        {
            indexZ = 0;
        }

       numZ.text = numberZ.ToString();
    }
}
