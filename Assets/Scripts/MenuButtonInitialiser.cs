using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonInitialiser : MonoBehaviour
{

    [SerializeField] private Button play;
    [SerializeField] private Button quitToMenu;

    private void Awake()
    {
        play.onClick.AddListener(GameManager.Instance.NewGame);
        quitToMenu.onClick.AddListener(GameManager.Instance.ExitGame);
    }
}
