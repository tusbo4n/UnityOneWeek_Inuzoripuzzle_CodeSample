using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirSelectUI : MonoBehaviour
{
    [SerializeField] MasterData_DebugData debugData;

    [SerializeField] SpriteRenderer stop;
    [SerializeField] SpriteRenderer down;
    [SerializeField] SpriteRenderer left;
    [SerializeField] SpriteRenderer right;
    [SerializeField] SpriteRenderer up;

    Inu inu;
    Vector3 beginMouserPos;
    Vector3Int direction;

    Color selectedColor = new Color(1,1,1,1);
    Color unSelectedColor = new Color(1,1,1,0.5f);

    public void Set(Inu inu)
    {
        beginMouserPos = Input.mousePosition;
        this.inu = inu;
    }

    private void Update()
    {
        //初期化
        left.color = unSelectedColor;
        right.color = unSelectedColor;
        up.color = unSelectedColor;
        down.color = unSelectedColor;
        stop.color = unSelectedColor;

        direction = CalcDir();
        if (Input.GetMouseButtonUp(0))
        {
            inu.SetMoveVec(direction);
            GameSceneController.instance.Score++;
            SoundManager.PlaySound(CommonData.SoundId.DogRun);
            Destroy(gameObject);
        }
        else
        {
            //押下中
            //Debug.Log(direction);
            int hantei = direction.x * 2 + direction.y;
            switch (hantei)
            {
                case -2: //left
                    left.color = selectedColor;
                    break;
                case -1: //down
                    down.color = selectedColor;
                    break;
                case 0:  //stop
                    stop.color = selectedColor;
                    break;
                case 1:  //up
                    up.color = selectedColor;
                    break;
                case 2:  //right
                    right.color = selectedColor;
                    break;
            }

        }
    }

    Vector3Int CalcDir()
    {
        Vector3Int result = Vector3Int.zero;
        var currentMousePos = Input.mousePosition;
        float xDist = currentMousePos.x - beginMouserPos.x;
        if(Mathf.Abs(xDist) > debugData.threshold)
        {
            result.x = (int)Mathf.Sign(xDist);
        }
        float yDist = currentMousePos.y - beginMouserPos.y;
        if(Mathf.Abs(yDist) > debugData.threshold)
        {
            result.y = (int)Mathf.Sign(yDist);
        }

        if(result.x != 0 && result.y != 0)
        {
            if(Mathf.Abs(xDist) > Mathf.Abs(yDist))
            {
                result.y = 0;
            }
            else
            {
                result.x = 0;
            }
        }

        return result;
    }
}
