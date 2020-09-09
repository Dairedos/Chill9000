using System.Collections;
using UnityEngine;
using System;


public class Collectable : MonoBehaviour
{
    [SerializeField]
    private AudioClip loopSound;

    [SerializeField]
    private AudioClip pickupSound;

    [SerializeField]
    private AudioClip fxAvailSound;

    private AudioSource audioSource;

    [SerializeField]
    private bool alreadyPicked;

    private bool grabbed;

    private PersistentSlotData.BigCollectable nameToBigCol;

    [SerializeField]
    private Material fxPickedMaterial;

    [SerializeField]
    private Material fxNotPickedMaterial;

    [SerializeField]
    private GameObject fxAvailVisualObject;

    private ParticleSystem fxAvailVisual;

    [SerializeField]
    protected bool makeAction;

    private Animator animator;


    enum collectableType
    {
        smallCollectable,
        bigCollectable,

    }


    [SerializeField]
    private collectableType type;

    [SerializeField]
    private int value;

    private AudioManager audioManager;

    void Awake()
    {
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
        fxAvailVisual = fxAvailVisualObject.GetComponent<ParticleSystem>();

    }

    void Start()
    {
        makeAction = false;
        grabbed = false;
        if (loopSound != null)
        {
            setLoopSound();
        }

        if (type.Equals(collectableType.bigCollectable))
        {

            nameToBigCol = (PersistentSlotData.BigCollectable)Enum.Parse(typeof(PersistentSlotData.BigCollectable), this.gameObject.name);
            alreadyPicked = PersistentSlotData.bigCollectableStatus[PersistentSlotData.currentScene][nameToBigCol];

            if (alreadyPicked)
                this.GetComponentInChildren<ParticleSystem>().gameObject.GetComponent<Renderer>().material = fxPickedMaterial;
            else
                this.GetComponentInChildren<ParticleSystem>().gameObject.GetComponent<Renderer>().material = fxNotPickedMaterial;

            this.gameObject.SetActive(false);
        }




    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbed)
        {

            makeAction = true;
        }

        if ((!grabbed) && makeAction)
        {
            grabbed = true;

            animator.SetBool("Collect", true);
            AddCollectable(type);

            if (pickupSound != null)
            {
                audioSource.Stop();
                audioSource.loop = false;
                audioSource.spatialize = false;

                audioSource.volume = 0.50f;
                audioSource.clip = pickupSound;
                audioSource.Play();
            }
        }

        if (grabbed)
            StartCoroutine(Destroy(1.0f));
    }

    private void AddCollectable(collectableType type)
    {
        switch (type)
        {

            case collectableType.smallCollectable:
            PersistentSlotData.smallCollectable += value;
            break;

            case collectableType.bigCollectable:
            if (!alreadyPicked)
            {
                alreadyPicked = true;
                /*Dictionary<PersistentData.BigCollectable, bool>  bicCols = PersistentData.bigCollectableStatus[PersistentData.currentScene];
                bicCols[nameToBigCol] = true;*/

                PersistentSlotData.bigCollectableStatus[PersistentSlotData.currentScene][nameToBigCol] = alreadyPicked;

                PersistentSlotData.bigCollectable += value;
            }
            break;
        }
    }

    IEnumerator Destroy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }

    public void setAlreadyPicked(bool alreadyPicked)
    {

        alreadyPicked = true;
    }

    public void AvailBigCol()
    {
        this.gameObject.SetActive(true);

        audioSource.Stop();
        audioSource.loop = false;

        audioSource.clip = fxAvailSound;
        audioSource.volume = 1.0f;
        audioSource.spatialize = false;
        audioSource.spatialBlend = 0.0f;
        audioSource.Play();
        fxAvailVisual.Stop();
        fxAvailVisual.Play();

        StartCoroutine(delayLoopSound(1.0f));
    }

    private void setLoopSound()
    {
        audioSource.clip = loopSound;
        audioSource.volume = 0.5f;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatialize = true;
        audioSource.loop = true;
        audioSource.Play();
    }

    private IEnumerator delayLoopSound(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        setLoopSound();

    }
}
