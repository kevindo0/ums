using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class AddressableTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Show();
        }
    }

    public async void Show()
    {
        await ShowAsync();
    }

    public async Task ShowAsync()
    {
        TextAsset s = await AddressableMgr.LoadAsset<TextAsset>("player.json");
        Debug.Log("text:" + s.text);
    }

    public void onDraw()
    {
        Debug.Log("draw");
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
    }
}
