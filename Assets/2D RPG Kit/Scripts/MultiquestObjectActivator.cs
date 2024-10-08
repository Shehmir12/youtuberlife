using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiquestObjectActivator : MonoBehaviour
{
    [Tooltip("Drag and drop the game object that should be activated or deactivated")]
    public GameObject objectToActivate;
    [Tooltip("Choose the quest whose completion should be checked from the Quest Manager")]
    public List<string> questsToCheck;
    [Tooltip("Activate the game object when the chosen quest was completed. Leave unchecked if you want to deactivate the game object instead")]
    public bool activeIfComplete;
    [Tooltip("Activate a delay before the activation")]
    public bool waitBeforeActivate;
    [Tooltip("Enter the duration for the delay in seconds")]
    public float waitTime;

    private bool initialCheckDone;

    public UnityEvent onActivate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            initialCheckDone = true;

            CheckCompletion();
        }
    }

    public void CheckCompletion()
    {
        for (int i = 0; i < questsToCheck.Count; ++i)
        {
            if (QuestManager.instance.CheckIfComplete(questsToCheck[i]))
            {
                if (i == questsToCheck.Count - 1)
                {
                    if (waitBeforeActivate)
                    {
                        StartCoroutine(waitCo());
                    }
                    else
                    {
                        objectToActivate.SetActive(activeIfComplete);
                    }
                }
            }
            else
            {

            }
        }
    }

    IEnumerator waitCo()
    {
        yield return new WaitForSeconds(waitTime);
        objectToActivate.SetActive(activeIfComplete);
        onActivate?.Invoke();
    }
}
