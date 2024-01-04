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
    [SerializeField] private int[] _spellInventory;
    // 0 = force spike, 1 = deflector, 2 = lightning, 3 = anchor

    [Tooltip("The game objects for each spells")]
    [SerializeField]
    private GameObject[] _spells;

    [Tooltip("The offset of where the spell is cast")]
    [SerializeField] private float _castingOffset;


    [Header("Anchor Information")]
    [Tooltip("How long the anchor effect lasts")]
    [SerializeField] private float _anchorTime;
    [Tooltip("How much the speed is affected by the anchor")]
    [SerializeField] private float _speedMultiplier;

    [Tooltip("Where the anchor is in the spell index")]
    [SerializeField] private int _anchorIndex;

    [Header("ForceField Information")]
    [Tooltip("How long the forcefield effect lasts")]
    [SerializeField] private float _forcefieldTime;

    [Tooltip("Where the forcefield is in the spell index")]
    [SerializeField] private int _forcefieldIndex;
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
        _spells[_anchorIndex].SetActive(false);
        _spells[_forcefieldIndex].SetActive(false);
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
    void OnShoot(InputValue input)
    {
        //catch zeroes
        if (_spellInventory[_index] < 0)
        {
            return;
        }
        //lose  one spell
        DecrementSpell();
        if (_index == _anchorIndex)
        {
            StartCoroutine(CastAnchor());
        }
        else if(_index == _forcefieldIndex)
        {
            StartCoroutine(CastForceField());
        }
        else
        {
            //activate selected spell
            CastSpell(_spells[_index]);
        }

    }

    private void CastSpell(GameObject Spell)
    {
        Instantiate(Spell, transform.position + (transform.up * _castingOffset), transform.rotation); //, PlayerMovementController.Instance.GetRotation()
    }


    private void DecrementSpell()
    {
        if(_spellInventory[_index] <= 0)
        {
            return;
        }
        if (--_spellInventory[_index] < 0)
        {
            _spellInventory[_index] = 0;
        }
    }


    #endregion

    #region Passive Abilities

    private IEnumerator CastAnchor()
    {
        _spells[_anchorIndex].SetActive(true);
        PlayerMovementController.Instance.SetDescendSpeed(PlayerMovementController.Instance.GetDescendSpeed() * _speedMultiplier);
        yield return new WaitForSeconds(_anchorTime);
        PlayerMovementController.Instance.SetDescendSpeed(PlayerMovementController.Instance.GetDescendSpeed() / _speedMultiplier);
        _spells[_anchorIndex].SetActive(false);
        print("ANCHOR OVER");
    }

    private IEnumerator CastForceField()
    {
        _spells[_forcefieldIndex].SetActive(true);
        yield return new WaitForSeconds(_forcefieldTime);
        _spells[_forcefieldIndex].SetActive(false);
        print("FORCE FIELD OVER");
    }

    #endregion


    #region UI 

    #endregion
}
