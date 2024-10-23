using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Discogs;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Fantassimo : MonoBehaviour {   
    private int x = 0;
    private int y = 0;
    public Image fixedImg;
    private readonly List<Image> imgs = new List<Image>();
    private readonly float[] coloure = {0f,255f,255f};
    public bool greenlit = false;

    public void AddImage(Image newIm) {
        imgs.Add(newIm);
    }
    public void imstillalive() {
        imgs.Clear();
    }

    void Update() {
        if (greenlit) {
            Time.fixedDeltaTime = 10f/9f/255f;
        } else {
            Time.fixedDeltaTime = 1000f;
            fixedImg.color = new Color(1,1,1);
            if (imgs.Count>0) {
                foreach (var img in imgs) {
                    img.color = new Color(1,1,1);
                }
            }
        }
    }

    void FixedUpdate() {
        if (greenlit) {
            if ((y%2)==0) {
                coloure[x%3]++;
                if (coloure[x%3]>=255) {
                    coloure[x%3] = 255f;
                    x++;
                    y++;
                }
            } else {
                coloure[x%3]--;
                if (coloure[x%3]<=0) {
                coloure[x%3] = 0f;
                    x++;
                    y++;
                }
            }
            fixedImg.color = new Color(coloure[0]/255,coloure[1]/255,coloure[2]/255);
            if (imgs.Count>0) {
                foreach (var img in imgs) {
                    img.color = new Color(coloure[0]/255,coloure[1]/255,coloure[2]/255);
                }
            }
        }
    }
}
