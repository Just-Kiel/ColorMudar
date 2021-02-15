using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SelectedPlayer : MonoBehaviour
{
    [SerializeField] private Transform[] PersoList;
    [SerializeField] private Transform currentPerso;
    // Start is called before the first frame update
    void Start()
    {
        currentPerso = PersoList.Single(d => d.name == MenuStart.currentPlayer);
    }
}
