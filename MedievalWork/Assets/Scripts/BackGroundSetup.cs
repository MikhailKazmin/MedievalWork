using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundSetup : MonoBehaviour
{
    [SerializeField] private GameObject _prefabGroup;
    [SerializeField] private List<Sprite> _treesSprite;
    [SerializeField] private List<Sprite> _rocksSprite;
    //[SerializeField] private Transform _parentTree;
    //[SerializeField] private Transform _parentRock;
    private List<GameObject> currentPref = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            currentPref.Add(Instantiate(_prefabGroup, transform));
        }
        PrefabSetup();
    }

    private void PrefabSetup()
    {
        foreach (var prefab in currentPref)
        {
            var prefTree = prefab.transform.GetChild(0).GetChild(0);
            var prefRock = prefab.transform.GetChild(1).GetChild(0);
            prefTree.GetComponent<Image>().sprite = _treesSprite[Random.Range(0, _treesSprite.Count - 1)];
            prefRock.GetComponent<Image>().sprite = _rocksSprite[Random.Range(0, _rocksSprite.Count - 1)];
            prefTree.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
            prefRock.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
            prefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-Screen.width / 2, Screen.width / 2), Random.Range(-Screen.height / 2, Screen.height / 2));
        }


    }
}
