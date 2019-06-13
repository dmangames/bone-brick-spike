using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite buttonDown;
    public Sprite buttonUp;

    public Transform spawnItemLocation;
    public Transform spawnImageLocation;
    public GameObject itemImage;
    public int itemChildIndex;
    public GameObject[] possibleItemImages;
    public GameObject[] actualItems;
    // Start is called before the first frame update
    void Start()
    {
        CreateItemImage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateItemImage()
    {
        itemChildIndex = Random.Range(0, possibleItemImages.Length);
        GameObject item = possibleItemImages[itemChildIndex];
        itemImage = Instantiate(item, spawnImageLocation.position, Quaternion.identity);
    }

    private void OnMouseDown()
    {
        //we need to change the sprite
        sr.sprite = buttonDown;
        //instantiate actual object
        Instantiate(actualItems[itemChildIndex], spawnItemLocation.transform.position, Quaternion.identity);
        //get rid of item image
        Destroy(itemImage);

    }

    private void OnMouseUp()
    {
        //change to mouse up sprite
        sr.sprite = buttonUp;
        //create new item image
        CreateItemImage();
    }
}
