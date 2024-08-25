using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper;

public class Field
{
    public bool IsBomb { get; }
    public bool HasFlag { get; set; }
    public bool IsVisible { get; set; }
    public int Count { get; set; }
    public Field? Top { get; }
    public Field? Right { get; private set; }
    public Field? Bottom { get; private set; }
    public Field? Left { get; }
    public Field(bool isBomb, Field? top, Field? left)
    {
        IsBomb = isBomb;
        HasFlag = false;
        IsVisible = false;
        Top = top;
        Left = left;
        
        if (Top != null)
        {
            Top.Bottom = this;
        }

        if (Left != null)
        {
            Left.Right = this;
        }
    }

    public string GetValue ()
    {
        if(HasFlag)
        {
            return "P";
        }
        if(IsVisible)
        {
            if (IsBomb)
            {
                return "M";
            }
            if (!IsBomb)
            {
                return Count.ToString();
            }
        }
        return "X";
    }

    public int CalcBombsAroundMe()
    {
        var Count = 0;
        if (Top is not null && Top.IsBomb)
        {
            Count++;
        }

        if (Right is not null)
        {
            if (Right.IsBomb)
            {
                Count++;
            }
            if (Right.Top != null && Right.Top.IsBomb)
            {
                Count++;
            }
            if (Right.Bottom != null && Right.Bottom.IsBomb)
            {
                Count++;
            }
        }

        if (Bottom is not null && Bottom.IsBomb)
        {
            Count++;
        }

        if (Left is not null)
        {
            if (Left.IsBomb)
            {
                Count++;
            }
            if (Left.Top != null && Left.Top.IsBomb)
            {
                Count++;
            }
            if (Left.Bottom != null && Left.Bottom.IsBomb)
            {
                Count++;
            }
        }
        return Count;
    }

    public void Reveal()
    {
        if (IsVisible)
        {
            return;
        }

        IsVisible = true;
        Count = CalcBombsAroundMe();

        if (Count == 0 && IsBomb == false)
        {
            if (Top!= null)
            {
                Top.Reveal();
            }

            if (Right != null)
            {
                Right.Reveal();

                if (Right.Top != null)
                {
                    Right.Top.Reveal();
                }

                if (Right.Bottom != null)
                {
                    Right.Bottom.Reveal();
                }
            }

            if (Bottom != null)
            {
                Bottom.Reveal();
            }

            if (Left!= null)
            {
                Left.Reveal();

                if (Left.Bottom != null)
                {
                    Left.Bottom.Reveal();
                }

                if (Left.Top != null)
                {
                    Left.Top.Reveal();
                }
            }
        }
    }
}

