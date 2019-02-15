using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController> {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        GameObject soundManager = new GameObject("SoundManager");
        soundManager.AddComponent<SoundManager>();
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
