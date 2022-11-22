using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Function : MonoBehaviour
{
    public AudioSource sound;
    public GameObject spawn_object;
    bool object_spawn = false;
    bool pinch = true;
    private Touch touch;
    Vector2 first_touch;
    Vector2 secound_touch;
    float distance_current;
    float distance_prev;

    public Vector3 min_scale = new Vector3(0.03f, 0.03f, 0.03f);
    public Vector3 max_scale = new Vector3(0.3f, 0.3f, 0.3f);

    //Image Target Terdeteksi
    public void OnTracking()
    {
        sound.Play();
        object_spawn = true;
    }

    //Image Target Tidak Terdeteksi
    public void LostTracking()
    {
        sound.Pause();
        object_spawn = false;
    }

    void Update()
    {
        //close app
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }            
        //jika ada sentuhan pada layar dan objek telah terdeteksi
        if (Input.touchCount > 0 && object_spawn)
        {
            if(Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                //jika sentuhan adalah swipe maka rotate objek
                if(touch.phase == TouchPhase.Moved)
                {
                    spawn_object.transform.Rotate(0f, 0f, -touch.deltaPosition.x * 2f);
                }
            }
            //jika jumblah sentukan lebih dari 1 (pinch to zoom/scale objek)
            if (Input.touchCount >= 2)
            {
                first_touch = Input.GetTouch(0).position;
                secound_touch = Input.GetTouch(1).position;

                distance_current = secound_touch.magnitude - first_touch.magnitude;
                if (pinch)
                {
                    distance_prev = distance_current;
                    pinch = false;
                }
                if (distance_current != distance_prev)
                {
                    Vector3 newScale = new Vector3();
                    newScale.x = Mathf.Clamp(spawn_object.transform.localScale.x * (distance_current / distance_prev), min_scale.x, max_scale.x);
                    newScale.y = Mathf.Clamp(spawn_object.transform.localScale.y * (distance_current / distance_prev), min_scale.y, max_scale.y);
                    newScale.z = Mathf.Clamp(spawn_object.transform.localScale.z * (distance_current / distance_prev), min_scale.z, max_scale.z);
                    spawn_object.transform.localScale = newScale;
                    distance_prev = distance_current;
                }
            }
            else{
                pinch = true;
            }
        }
    }



}
