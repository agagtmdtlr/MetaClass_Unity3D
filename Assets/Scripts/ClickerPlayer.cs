using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerPlayer : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (BossMonster.IsBossMonster(hit.collider))
                {
                    BossMonster.CrrentSceneBossMonster.ChangeHp(-1);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var capsuleWarrior = CapsuleWarrior.GetCapsuleWarrior(hit.collider);
                if (capsuleWarrior is not null)
                {
                    capsuleWarrior.ChangeHp(+1);
                }
            }
        }
    }
}
