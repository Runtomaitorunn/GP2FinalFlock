using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject[] flockPrefabs;
    public Canvas ideaBubble;
    public GameObject brainFull;
    public List<IdeaFlocks> ideaFlocks = new List<IdeaFlocks>();

    private string textInput;

    [Header("Character Animaiton")]
    public float durationTime = 4f;

    void Start()
    {
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetInteger("Index", 100);

        AddPrompt(0, "Birds");
        AddPrompt(0, "fly");
        AddPrompt(9, "How you doing");
        AddPrompt(1, "crazy");
        AddPrompt(1, "heartbreak");
        AddPrompt(1, "love");
        AddPrompt(1, "him");
        AddPrompt(1, "boy");
        AddPrompt(2, "mad");
        AddPrompt(2, "just chill");
        AddPrompt(2, "try to relax");
        AddPrompt(3, "crazy");
        AddPrompt(4, "money");
        AddPrompt(4, "job");
        AddPrompt(5, "paperplane");
        AddPrompt(5, "plane");
        AddPrompt(5, "airplane");
        AddPrompt(6, "powerpuff");
        AddPrompt(6, "power");
        AddPrompt(6, "human");
        AddPrompt(6, "strength");
        AddPrompt(6, "wake");
        AddPrompt(7, "sad");
        AddPrompt(7, "you're under stress");
        AddPrompt(7, "get over it");
        AddPrompt(8, "Hello");
        AddPrompt(8, "hi");
        AddPrompt(9, "find some koala");
        AddPrompt(9, "peace");
        AddPrompt(9, "tree");
        AddPrompt(9, "I don't know");
        AddPrompt(9, "idk");
        AddPrompt(10, "wtf");
        AddPrompt(10, "fuck");
        AddPrompt(10, "sassy");
        AddPrompt(10, "care");
        AddPrompt(10, "I don't care");
        AddPrompt(10, "Take care");
        AddPrompt(10, "metal");
        AddPrompt(10, "rock");
        AddPrompt(11, "you need more food");
        AddPrompt(11, "just eat");


    }
    public void AddPrompt(int flockIndex, string prompts)
    {
        IdeaFlocks newIdea = new IdeaFlocks (new List<string> { prompts }, flockIndex); // Initialize a new IdeaFlocks object
        ideaFlocks.Add(newIdea);
    }
    public void StartIdeas(string inputText)
    {
        
        // Ask FlocksData if click is allowed
        if (FlocksData.flockList.Count < 5)
        {
            HandleIdeas(inputText);
        }
        else
            // Brain is full
            StartCoroutine(BrainFull());
    }

    public void HandleIdeas(string inputText)
    {
        // check if input string matches keyword
        // If not, then randomly generate
        int flockIndex;

        if (IdeaFlocksContainInput(inputText, out flockIndex))
        {
            InitiateFlocks(flockIndex);
        }
        else
        {
            flockIndex = IdeaRandom();
            InitiateFlocks(flockIndex);
        }

        // Pass the index to ideaBudbble
        ShowIdeas showIdeas = ideaBubble.GetComponent<ShowIdeas>();
        if (showIdeas != null)
        {
            showIdeas.StartCoroutine(showIdeas.TackleIdeas(ideaBubble, flockIndex, durationTime));
        }
        else
            Debug.Log("can't find the script ShowIdeas!");

        // Set the animator
        StartCoroutine(SetAnimation(flockIndex));

    }

    bool IdeaFlocksContainInput(string inputText, out int flockIndex)
    {
        foreach (var ideaFlock in ideaFlocks)
        {
            foreach (var prompt in ideaFlock.prompts)
            {
                Debug.Log("HERE I AM"+prompt);
                Debug.Log("HERE I WAS"+inputText);
                if (prompt.Contains(inputText, StringComparison.OrdinalIgnoreCase))
                {
                    flockIndex = ideaFlock.flockIndex;
                    Debug.Log("think same in" + flockIndex);
                    return true;
                }
            }
        }
        flockIndex = -1;
        Debug.Log("think different!");
        return false;
        
    }

    public int IdeaRandom()
    {
        int flockIndex = UnityEngine.Random.Range(0, flockPrefabs.Length);
        return flockIndex;
    }


    public void InitiateFlocks(int flockIndex)
    {

        GameObject prefab = flockPrefabs[flockIndex];
        GameObject flock = Instantiate(prefab, transform);
        flock.transform.position = new Vector3(0, 0, 0);

        // Count the flock
        FlocksData.flockList.Add(flock);
    }

    IEnumerator SetAnimation(int flockIndex)
    {
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null)
        {
            // Play the disired animation
            anim.SetInteger("Index", flockIndex);

            yield return new WaitForSeconds(durationTime);

            // Go back to idle
            anim.SetInteger("Index", 100);
            anim.SetTrigger("BackToIdle");
        }
        else
            Debug.Log("can't find animtor in child of character");
    }

    IEnumerator BrainFull()
    {
        brainFull.SetActive(true);

        yield return new WaitForSeconds(durationTime);

        brainFull.SetActive(false);
    }

    [System.Serializable]
    public class IdeaFlocks
    {
        public List<string> prompts;
        public int flockIndex;

        public IdeaFlocks(List<string> prompts, int flockIndex)
        {
            this.prompts = prompts;
            this.flockIndex = flockIndex;
        }

    }
}
