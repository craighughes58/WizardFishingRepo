using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpellController : MonoBehaviour
{


    #region
    //the index of _spellInventory
    private int _index = 0;
    #endregion

    #region Serialized Variables
    [Tooltip("The number of each spell the player has")]
    [SerializeField]
    private int[] _spellInventory;
    // 0 = force spike, 1 = deflector, 2 = lightning, 3 = anchor

    [Tooltip("The game objects for each spells")]
    [SerializeField]
    private GameObject[] _spells;

    /* [Tooltip("The game object of the force spike")]
     [SerializeField]
     private GameObject _forceSpike;
     [Tooltip("The game object of the lightning bolt")]
     [SerializeField]
     private GameObject _lightning;
     [Tooltip("The game object of the deflector")]
     [SerializeField]
     private GameObject _deflector;
     [Tooltip("The game object of the anchor")]
     [SerializeField]
     private GameObject _anchor;*/
    #endregion


    public static PlayerSpellController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }



    #region Switching Spells

    void OnSpellSelect(InputValue input)
    {

        if(input.Get() == null)
        {
            return;
        }
        if(input.Get().ToString().Equals("-1"))
        {
            LastSpell();
        }
        else if(input.Get().ToString().Equals("1"))
        {
            NextSpell();
        }
        print(_index);
        //use next spell and last spell here based on positives and negatives
    }

    private void NextSpell()
    {
        ++_index;
        if (_index >= _spellInventory.Length)
        {
            _index = 0;
        }
    }

    private void LastSpell()
    {
        --_index;
        if (_index < 0)
        {
            _index = _spellInventory.Length - 1;
        }
    }

    #endregion

    #region Using Spells
    void OnSoot(InputValue input)
    {
        //catch zeroes
        if (_spellInventory[_index] < 0)
        {
            return;
        }
        //lose  one spell
        DecrementSpell();
        //activate selected spell
        CastSpell(_spells[_index]);

    }

    private void CastSpell(GameObject Spell)
    {
        Instantiate(Spell, transform.position, transform.rotation); //, PlayerMovementController.Instance.GetRotation()
    }


    private void DecrementSpell()
    {
        if (--_spellInventory[_index] < 0)
        {
            _spellInventory[_index] = 0;
        }
    }


    #endregion

    #region UI 

    #endregion
}
