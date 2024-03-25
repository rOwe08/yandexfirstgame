using System.Collections.Generic;
using UnityEngine;

public class HuggiWaggi : MonoBehaviour
{

    public List<GameObject> parts;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        ResetBody();
    }

    public void ResetBody()
    {
        index = 0;
        foreach (var part in parts)
        {
            part.SetActive(false);
        }
    }

    public void ActivateNextPart()
    {
        parts[index].SetActive(true);
        index++;
    }
}
