using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform playerTransform;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    /*lateUpdate happens after update. In update, we make sure the character does 
     not go out of bounds. So, instead of update, we will move camera in lateUpdate so there
     is no wrong calculations/glitches
    */

    void LateUpdate()
    {
        if(GameplayController.instance.gameGoal != GameGoal.DEFEND_FENCE ||
            GameplayController.instance.gameGoal != GameGoal.GAME_OVER)
        {
            if (playerTransform)
            {

                Vector3 temp = transform.position;
                temp.x = playerTransform.position.x;
                transform.position = temp;

            }
        }
    

    }
} //class



























