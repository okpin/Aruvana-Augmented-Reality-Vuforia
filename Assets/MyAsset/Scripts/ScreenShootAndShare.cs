using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class ScreenShootAndShare : MonoBehaviour
{
	public Animator dropdown;
    public GameObject mainui;
	string path;
	private IEnumerator TakeScreenshotAndSave()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		ss.Apply();

		path = Path.Combine(Application.temporaryCachePath, "Image.png");
		File.WriteAllBytes(path, ss.EncodeToPNG());

		NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(ss, "Aruvana", "Image.png", (success, path) => Debug.Log("Media save result: " + success + " " + path));	

		Destroy(ss);
		mainui.SetActive(true);
		dropdown.SetBool("drop", true);
		
	}
	//share screnshot
	public void Share()
    {
		new NativeShare()
		.AddFile(path)
		.SetSubject("my screenshot")
		.SetText("share my AR screenshot with friends")
		.Share();

		dropdown.SetBool("drop", false);
	}
	//start takescreenshot IEnumerator
	public void TakeScreenShoot()
	{
		mainui.SetActive(false);
		StartCoroutine("TakeScreenshotAndSave");		
	}
	//close share panel ui
	public void CloseSharePanel()
    {
		dropdown.SetBool("drop", false);
	}
}
