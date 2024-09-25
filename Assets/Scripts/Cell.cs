using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Cell UpCell;
    public Cell LeftCell;
    public Cell RightCell;
    public Cell DownCell;

    [Space(10)]
    public int Value = 1;
    public int Points;
    public int UpX = 1;

    [Space(10)]
    public bool IsEmpty = true;
    public bool IsCellToRandom;
    public bool Starting = false;

    [Space(20)]
    [SerializeField]
    public bool up = false;
    public bool left = false;
    public bool right = false;

    public bool wasMoved = false;


    [Space(10)]
    public List<Cell> List1;
    public bool isList1;
    public List<Cell> List2;
    public bool isList2;
    public List<Cell> List3;
    public bool isList3;
    public List<Cell> List4;
    public bool isList4;
    public List<Cell> List5;
    public bool isList5;

    [Space(10)]
    [SerializeField]
    private Image image;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    public GameMan GameMan;




    private List<Cell> List;
    public CellList CellList;

    void Start()
    {
        List = CellList.cells;
        Starting = true;
        RandomizeValue();
    }


    void Update()
    {
        UpdateCell();
    }

    public void UpdateCell()
    {
        Points = (int)Mathf.Pow(2, Value);
        if (!IsEmpty && Value != 0)
        {
            pointsText.text = Points.ToString();
            if (Value < 4)
            {
                pointsText.color = ColorMan.instance.Colors[0];
                image.color = ColorMan.instance.Colors[Value];
            }
            else
            {
                pointsText.color = Color.white;
                image.color = ColorMan.instance.Colors[Value];
            }
        }
        else
        {
            pointsText.text = "";
            image.color = new Color(0, 0, 0, 0.2f);
            IsEmpty = true;
        }
    }
    public void RandomizeValue()
    {
        if (IsCellToRandom)
        {
            Value = (Random.Range(1, 6) * UpX);
        }
    }

    public void CreateRandomCell(CellList CList)
    {
        Cell LCell;
        var LastList = new List<Cell>();
        List = CList.cells;
        GameMan.Steps++;

        for (int i = 0; i < List.Count; i++)
            if (List[i].IsEmpty && Starting == true)
            {
                LCell = List[i];
                CreateOnCell(LCell);
                UpdateCell();
                RandomizeValue();
                break;
            }
    }

    public void CreateOnCell(Cell LCell)
    {

        LCell.Value = Value;
        LCell.IsEmpty = false;
        LCell.UpdateCell();
        CheckNearCell(LCell);

        if (IsCellToRandom)
        {
            GameMan.SavePrev();
            Debug.Log("213");
        }
    }

    public void CheckNearCell(Cell LCell)
    {
        up = false;
        left = false;
        right = false;

        if (!IsEmpty)
        {
            if (LCell.UpCell.Value == LCell.Value && !LCell.UpCell.IsEmpty)
            {
                up = true;
            }
            if (LCell.LeftCell.Value == LCell.Value && !LCell.LeftCell.IsEmpty)
            {
                left = true;
            }
            if (LCell.RightCell.Value == LCell.Value && !LCell.RightCell.IsEmpty)
            {
                right = true;
            }


            if (up || left || right)
            {
                LCell.MergeWithCell(up, left, right, LCell);
            }
        }

    }

    public void MergeWithCell(bool Lup, bool Lleft, bool Lright, Cell LCell)
    {
        bool merged = false;

        up = Lup;
        left = Lleft;
        right = Lright;

        if (Lup == true && Lleft == false && Lright == false)
        {
            UpCell.Value = Value + 1;
            IsEmpty = true;
            Value = 0;
            merged = true;
        }
        else if (Lup == false && Lleft == true && Lright == false)
        {
            LeftCell.Value = 0;
            LeftCell.IsEmpty = true;
            Value++;
            merged = true;
        }
        else if (Lup == false && Lleft == false && Lright == true)
        {
            RightCell.Value = 0;
            RightCell.IsEmpty = true;
            Value++;
            merged = true;
        }
        else if (Lup == false && Lleft == true && Lright == true)
        {
            LeftCell.Value = 0;
            RightCell.Value = 0;
            LeftCell.IsEmpty = true;
            RightCell.IsEmpty = true;
            Value += 2;
            merged = true;
        }
        else if (Lup == true && Lleft == true && Lright == false)
        {
            UpCell.Value = Value += 2;
            LeftCell.Value = 0;
            Value = 0;
            LeftCell.IsEmpty = true;
            IsEmpty = true;
            merged = true;
        }
        else if (Lup == true && Lleft == false && Lright == true)
        {
            UpCell.Value = Value += 2;
            RightCell.Value = 0;
            Value = 0;
            LeftCell.IsEmpty = true;
            IsEmpty = true;
            merged = true;
        }
        else if (Lup == true && Lleft == true && Lright == true)
        {
            UpCell.Value = Value += 3;
            LeftCell.Value = 0;
            RightCell.Value = 0;
            Value = 0;
            LeftCell.IsEmpty = true;
            RightCell.IsEmpty = true;
            IsEmpty = true;
            merged = true;
        }

        if (merged)
        {
            MoveToEmpty(LCell);
        }
    }

    public void MoveToEmpty(Cell LCell)
    {
        if(up)
        {
            UpCell.CheckNearCell(UpCell);
        }

        if (left)
        {
            LeftCell.Value = LeftCell.DownCell.Value;
            LeftCell.IsEmpty = false;
            if (!LeftCell.IsEmpty)
                LeftCell.CheckNearCell(LeftCell);

            LeftCell.DownCell.Value = LeftCell.DownCell.DownCell.Value;
            LeftCell.DownCell.IsEmpty = false;
            if (!LeftCell.IsEmpty)
                LeftCell.DownCell.CheckNearCell(LeftCell);

            LeftCell.DownCell.DownCell.Value = LeftCell.DownCell.DownCell.DownCell.Value;
            LeftCell.DownCell.DownCell.IsEmpty = false;
            if (!LeftCell.IsEmpty)
                LeftCell.DownCell.DownCell.CheckNearCell(LeftCell);

            LeftCell.DownCell.DownCell.DownCell.Value = LeftCell.DownCell.DownCell.DownCell.DownCell.Value;
            LeftCell.DownCell.DownCell.DownCell.IsEmpty = false;
            if (!LeftCell.IsEmpty)
                LeftCell.DownCell.DownCell.DownCell.CheckNearCell(LeftCell);

            LeftCell.DownCell.DownCell.DownCell.DownCell.Value = LeftCell.DownCell.DownCell.DownCell.DownCell.DownCell.Value;
            LeftCell.DownCell.DownCell.DownCell.DownCell.IsEmpty = false;
            if (!LeftCell.IsEmpty)
                LeftCell.DownCell.DownCell.DownCell.DownCell.CheckNearCell(LeftCell);
        }

        if (right)
        {
            RightCell.Value = RightCell.DownCell.Value;
            RightCell.IsEmpty = false;
            if (!RightCell.IsEmpty)
                RightCell.CheckNearCell(RightCell);

            RightCell.DownCell.Value = RightCell.DownCell.DownCell.Value;
            RightCell.DownCell.IsEmpty = false;
            if (!RightCell.IsEmpty)
                RightCell.DownCell.CheckNearCell(RightCell);

            RightCell.DownCell.DownCell.Value = RightCell.DownCell.DownCell.DownCell.Value;
            RightCell.DownCell.DownCell.IsEmpty = false;
            if (!RightCell.IsEmpty)
                RightCell.DownCell.DownCell.CheckNearCell(RightCell);

            RightCell.DownCell.DownCell.DownCell.Value = RightCell.DownCell.DownCell.DownCell.DownCell.Value;
            RightCell.DownCell.DownCell.DownCell.IsEmpty = false;
            if (!RightCell.IsEmpty)
                RightCell.DownCell.DownCell.DownCell.CheckNearCell(RightCell);

            RightCell.DownCell.DownCell.DownCell.DownCell.Value = RightCell.DownCell.DownCell.DownCell.DownCell.DownCell.Value;
            RightCell.DownCell.DownCell.DownCell.DownCell.IsEmpty = false;
            if (!RightCell.IsEmpty)
                RightCell.DownCell.DownCell.DownCell.DownCell.CheckNearCell(RightCell);
        }

        CheckNearCell(LCell);
    }
}
