using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Crop : MonoBehaviour
{
    private int STEP_EMPTY = 0;
    private int STEP_GROWS = 1;
    private int STEP_READY = 2;
    private int STEP_PLOW = 3;
    private int STEP_WITHERED = 4;

    private SpriteRenderer seedSpriteRenderer;
    private SpriteRenderer productSpriteRenderer;
    private SpriteRenderer cropSpriteRenderer;

    private string timeGrowStarted = "";
    private Item cropItem;
    private int step = 0;
    private float weedSpawnChance = 0.3f; 
    private float weedSpawnTime = 3f;
    private float witheredTime = 25f; 

    private GameObject player;
    private bool readyForAction;
   
    void Start()
    {
        cropSpriteRenderer = GetComponent<SpriteRenderer>();
        seedSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[2];
        productSpriteRenderer = GetComponentsInChildren<SpriteRenderer>()[1];

        player = GameObject.FindWithTag("Player");
    }


    void OnMouseDown()
    {
        Item item = Player.getHandItem();

        if (readyForAction)
        {
            if(step == STEP_EMPTY)
            {
                if(item.type == Item.TYPEPFOOD)
                {  
                    Player.removeItem();
                    cropItem = item;
                    cropItem.count = 2;
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");

                    if (Random.Range(0f, 1f) < weedSpawnChance)
                    {   
                        step = STEP_WITHERED;
                        StartCoroutine(SpawnTimeWeed());
                        StartCoroutine(OnStepWithered());
                    }
                    else
                    {
                        step = STEP_GROWS;
                        timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                        StartCoroutine(grow());
                    }

                }
            }
            else if (step == STEP_WITHERED)
            {   
                if (item.type == Item.TYPEPLOW)
                {
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");
                    timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                    Player.DeletPlow(item);
                    StartCoroutine(grow());
                    step = STEP_GROWS;
                }
            }

            else if (step == STEP_READY)
            {
                productSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
                seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/extraDirt");
                Player.checkIfItemExists(cropItem);

                step = STEP_PLOW;
            }
            else if(step == STEP_PLOW)
            {
                if (item.type == Item.TYPEPLOW)
                {
                    seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
                    Player.DeletPlow(item);
                    step = STEP_EMPTY;
                }
            }
        }
    }

    public Item Save()
    {
        if(step == STEP_GROWS)
        {
            var currentTime = System.DateTime.Now;
            var lastSavedTimeConverted = System.Convert.ToInt64(timeGrowStarted);

            System.DateTime oldTime = System.DateTime.FromBinary(lastSavedTimeConverted);
            System.TimeSpan differenc = currentTime.Subtract(oldTime);

            cropItem.timeToGrow = (long)differenc.TotalSeconds;
            cropItem.type = STEP_GROWS;

            return cropItem;
        }
        else if (step == STEP_READY)
        {
            cropItem.type = STEP_READY;
            return cropItem;
        }
        else if (step == STEP_PLOW)
        {
            var saveItem = Player.getEmptyItem();
            saveItem.type = STEP_PLOW;
            return saveItem;
        }
        else return Player.getEmptyItem();
    }

    public void LaunchInitProcess(long offlineTime, Item item)
    {
        StartCoroutine(UpdateWithSaveData(offlineTime, item));
    }

    private IEnumerator UpdateWithSaveData(long offlineTime, Item item)
    {
        yield return new WaitForSeconds(1f);

        if (item.type == STEP_GROWS)
        {
            
            item.type = Item.TYPEPFOOD;
            cropItem = item;

            if (offlineTime > item.timeToGrow)
            {   
                OnStepReady();  
            }
            else
            {
                cropItem.timeToGrow = item.timeToGrow - offlineTime;
                timeGrowStarted = System.DateTime.Now.ToBinary().ToString();
                seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/seeds");
                StartCoroutine(grow());
            }
        }
    
        else if(item.type == STEP_READY)
        {
            item.type = Item.TYPEPFOOD;
            cropItem = item;
            OnStepReady();
        }
        else if (item.type == STEP_PLOW)
        {
            productSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
            seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/extraDirt");

            step = STEP_PLOW;
        }
    
    }

    private IEnumerator SpawnTimeWeed()
    {
        yield return new WaitForSeconds(weedSpawnTime);
        seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/weed");
    }

    private IEnumerator OnStepWithered()
    {
        yield return new WaitForSeconds(witheredTime);
        if (step == STEP_WITHERED)
        {
            seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/withered");
            step = STEP_PLOW;
        }
    }

    private void OnStepReady()
    {
       seedSpriteRenderer.sprite = Resources.Load<Sprite>("Food/empty");
       productSpriteRenderer.sprite = Resources.Load<Sprite>(cropItem.imgUrl);

       step = STEP_READY;
    }

    private IEnumerator grow()
    {
        yield return new WaitForSeconds(cropItem.timeToGrow);
        OnStepReady();
    }

    private void FixedUpdate()
    {
        if (step != STEP_GROWS)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) < 0.06f)
            {
                readyForAction = true;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/cropSelected");
            }
            else
            {
                readyForAction = false;
                cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/crop");
            }
        }
        else
        {
            cropSpriteRenderer.sprite = Resources.Load<Sprite>("Food/crop");
        }
    }

}
