using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance;
    public GameScene gameScene;
    public PlayerContaint playerContaint;

    


    public void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameScene.Init();
        playerContaint.Init();
    }

     
}
