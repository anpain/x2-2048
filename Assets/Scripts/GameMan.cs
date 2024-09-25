using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public List<Cell> AllCells;

    public List<int> PrevCells;
    public List<int> StorageCells;
    public Cell CellToRand;
    public int Steps;
    public int cooldown;

    [Space(10)]
    private bool wasReturned = true;


    public void Restart()
    {
        Steps = 0;
        for (int i = 0; i < AllCells.Count; i++)
        {
            AllCells[i].Value = 0;
            AllCells[i].IsEmpty = true;
            PrevCells[i] = 0;
            StorageCells[i] = 0;
        }
    }

    public void ReturnCells()
    {
        if (!wasReturned && cooldown >= 2)
        {
            Steps -= 1;
            for (int i = 0; i < AllCells.Count; i++)
            {
                AllCells[i].Value = PrevCells[i];
                wasReturned = true;
            }
            cooldown = 0;
        }
    }

    public void SavePrev()
    {
        cooldown++;
        wasReturned = false;
        for (int i = 0; i < 25; i++)
        {
            PrevCells[i] = StorageCells[i];
            StorageCells[i] = AllCells[i].Value;
        }
    }

}
