using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCombatDetection : MonoBehaviour
{
    #region Inspector Methods
    [Header("Settings")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private string characterLayer;

    [Header("References")]
    [SerializeField] private PlayerMovement character;
    #endregion

    #region Detection Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(characterLayer))
        {
            if (isPlayer)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Add(otherChar);
                }
            }
            else
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ally")
                {
                    Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Add(otherChar);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(characterLayer))
        {
            if (isPlayer)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Remove(otherChar);
                }
            }
            else
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ally")
                {
                    Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Remove(otherChar);
                }
            }
        }
    }
    #endregion
}
