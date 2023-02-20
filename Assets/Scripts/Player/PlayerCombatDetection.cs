using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerCombatDetection : MonoBehaviour
{
    #region Inspector Methods
    [Header("Settings")]
    //[SerializeField] private bool isPlayer;
    [SerializeField] private string characterLayer;

    [Header("References")]
    [SerializeField] private PlayerCombat playerCombat;
    #endregion

    #region Detection Methods
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(characterLayer))
        {
            Debug.Log(other.gameObject.layer + "Se está recogiendo el enemigo");
            if (playerCombat.isPlayer)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    //Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Add(otherChar);
                    playerCombat.enemys.Add(other.gameObject);
                }
            }
            else
            {
                if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ally")
                {
                    Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Add(otherChar);

                    Debug.Log("Se está soltó el enemigo");
                }
            }
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(characterLayer))
        {
            if (playerCombat.isPlayer)
            {
                if (other.gameObject.tag == "Enemy")
                {
                    //Character otherChar = other.GetComponent<Character>();
                    //for (int i = 0; i < character.Combats.Length; i++) character.Combats[i].Targets.Remove(otherChar);
                    playerCombat.enemys.Remove(other.gameObject);
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
