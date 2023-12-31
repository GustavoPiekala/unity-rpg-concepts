﻿using System;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;

        // Update is called once per frame
        void Update()
        {
            if (health.IsDead()) return;

            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private void Start() 
        {
            health = GetComponent<Health>();
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                GameObject targetGameObject = target.gameObject;
                if(!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            bool hasHit = Physics.Raycast(GetMouseRay(), out RaycastHit hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
