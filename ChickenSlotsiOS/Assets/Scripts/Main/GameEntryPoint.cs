using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEntryPoint
{
    private static GameEntryPoint instance;
    private UIRootView rootView;
    private Coroutines coroutines;
    public GameEntryPoint()
    {
        coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
        Object.DontDestroyOnLoad(coroutines.gameObject);

        var prefabUIRoot = Resources.Load<UIRootView>("UIRootView");
        rootView = Object.Instantiate(prefabUIRoot);
        Object.DontDestroyOnLoad(rootView.gameObject);

    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Autorun()
    {
        GlobalGameSettings();

        instance = new GameEntryPoint();
        instance.Run();

    }

    private static void GlobalGameSettings()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Run()
    {
#if UNITY_EDITOR

        //var sceneName = SceneManager.GetActiveScene().name;

        //if (sceneName == Scenes.MAIN_MENU)
        //{
        //    coroutines.StartCoroutine(LoadAndStartMainMenu());
        //    return;
        //}

        //if (sceneName == Scenes.MINI_GAME)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneMiniGame());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_1)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots1());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_2)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots2());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_3)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots3());
        //    return;
        //}

        //if (sceneName == Scenes.SLOT_4)
        //{
        //    coroutines.StartCoroutine(LoadAndStartSceneSlots4());
        //    return;
        //}

        //if (sceneName == Scenes.BOOT)
        //{
        //    return;
        //}

#endif

        coroutines.StartCoroutine(LoadAndStartMainMenu());
    }

    //private IEnumerator LoadAndStartCountryChecker()
    //{
    //    rootView.SetLoadScreen(0);

    //    yield return rootView.ShowLoadingScreen();

    //    yield return new WaitForSeconds(0.3f);

    //    yield return LoadScene(Scenes.SLOT_1);

    //    yield return new WaitForSeconds(0.2f);

    //    var sceneEntryPoint = Object.FindObjectOfType<CountryCheckerSceneEntryPoint>();
    //    sceneEntryPoint.Run(rootView);

    //    sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
    //    sceneEntryPoint.GoToOther += () => coroutines.StartCoroutine(LoadAndStartOther());

    //    yield return rootView.HideLoadingScreen();
    //}

    //private IEnumerator LoadAndStartOther()
    //{
    //    rootView.SetLoadScreen(0);

    //    yield return rootView.ShowLoadingScreen();

    //    yield return new WaitForSeconds(0.3f);

    //    yield return LoadScene(Scenes.SLOT_1);

    //    yield return new WaitForSeconds(0.2f);

    //    var sceneEntryPoint = Object.FindObjectOfType<OtherSceneEntryPoint>();
    //    sceneEntryPoint.Run(rootView);

    //    sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

    //    yield return rootView.HideLoadingScreen();
    //}

    private IEnumerator LoadAndStartMainMenu()
    {
        rootView.SetLoadScreen(0);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.2f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MAIN_MENU);

        yield return new WaitForSeconds(0.4f);

        var sceneEntryPoint = Object.FindObjectOfType<MainMenuEntryPoint>();
        sceneEntryPoint.Run(rootView);
        sceneEntryPoint.GoToMiniGame1_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1());
        sceneEntryPoint.GoToMiniGame2_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2());
        sceneEntryPoint.GoToMiniGame3_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3());

        sceneEntryPoint.GoToMiniGame1_Compaign_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1_Compaign());
        sceneEntryPoint.GoToMiniGame2_Compaign_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2_Compaign());
        sceneEntryPoint.GoToMiniGame3_Compaign_Action += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3_Compaign());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartSceneMiniGame1()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_1);

        yield return new WaitForSeconds(0.5f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame1SceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartSceneMiniGame2()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_2);

        yield return new WaitForSeconds(0.5f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame2SceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartSceneMiniGame3()
    {
        rootView.SetLoadScreen(3);

        yield return rootView.ShowLoadingScreen();

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_3);

        yield return new WaitForSeconds(0.5f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame3SceneEntryPoint>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());

        yield return rootView.HideLoadingScreen();
    }



    private IEnumerator LoadAndStartSceneMiniGame1_Compaign()
    {
        rootView.SetLoadScreen(1);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.2f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_1_COMPAIGN);

        yield return new WaitForSeconds(0.3f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame1SceneEntryPoint_Compaign>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToTryAgain += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1_Compaign());

        sceneEntryPoint.GoToGame1 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1_Compaign());
        sceneEntryPoint.GoToGame2 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2_Compaign());
        sceneEntryPoint.GoToGame3 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3_Compaign());

        Debug.Log("ClosePanel_Transit");
        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartSceneMiniGame2_Compaign()
    {
        rootView.SetLoadScreen(2);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.2f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_2_COMPAIGN);

        yield return new WaitForSeconds(0.3f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame2SceneEntryPoint_Compaign>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToTryAgain += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2_Compaign());

        sceneEntryPoint.GoToGame1 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1_Compaign());
        sceneEntryPoint.GoToGame2 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2_Compaign());
        sceneEntryPoint.GoToGame3 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3_Compaign());

        Debug.Log("ClosePanel_Transit");
        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadAndStartSceneMiniGame3_Compaign()
    {
        rootView.SetLoadScreen(3);

        yield return rootView.ShowLoadingScreen();

        yield return new WaitForSeconds(0.2f);

        yield return LoadScene(Scenes.BOOT);
        yield return LoadScene(Scenes.MINI_GAME_3_COMPAIGN);

        yield return new WaitForSeconds(0.3f);

        var sceneEntryPoint = Object.FindObjectOfType<MiniGame3SceneEntryPoint_Compaign>();
        sceneEntryPoint.Run(rootView);

        sceneEntryPoint.GoToMainMenu += () => coroutines.StartCoroutine(LoadAndStartMainMenu());
        sceneEntryPoint.GoToTryAgain += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3_Compaign());

        sceneEntryPoint.GoToGame1 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame1_Compaign());
        sceneEntryPoint.GoToGame2 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame2_Compaign());
        sceneEntryPoint.GoToGame3 += () => coroutines.StartCoroutine(LoadAndStartSceneMiniGame3_Compaign());

        Debug.Log("ClosePanel_Transit");
        yield return rootView.HideLoadingScreen();
    }

    private IEnumerator LoadScene(string scene)
    {
        Debug.Log("�������� ����� - " + scene);
        yield return SceneManager.LoadSceneAsync(scene);
    }
}
