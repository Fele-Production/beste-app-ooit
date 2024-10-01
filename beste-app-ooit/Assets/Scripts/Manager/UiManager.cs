using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public LibraryAccesser libraryAccesser;
    public TMP_InputField searchPrompt;
    public Image imgTest;

    public void Search() {
        libraryAccesser.Search(searchPrompt.text);
    }


}
