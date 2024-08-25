using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper;

public class Gameboard : IEnumerable<Field>
{
    private Field? _topLeft;
    private Field? _currentField;
    public int BombCount { get; set; }
    public int FlagCount { get; set; }
    public int GameboardSize { get; set; }
    public void CreateGameboard(int gameboardSize)
    {
        GameboardSize = gameboardSize;
        Field? topRow= null;
        for (int i = 0; i < gameboardSize; i++)
        {
            Field? left= null;
            Field? topField= topRow;
            for(int j = 0; j < gameboardSize; j++)
            {
                var field = new Field(Random.Shared.NextDouble() < 0.16, topField, left);
                if (field.IsBomb == true)
                {
                    BombCount++;
                }
                if (j == 0)
                {
                    topRow = field;
                }
                left = field;
                topField = field.Top?.Right;
            }
            if (i == 0)
            {
                _topLeft = topRow!;
            }
        }
    }

    public List<List<string>> GetFieldRepresentation()
    {

        List<List<string>> result = new List<List<string>>();
        var currentRow = _topLeft;

        while (currentRow != null)
        {
            List<string> row = new List<string>();
            var currentField = currentRow;

            while (currentField != null)
            {
                row.Add(currentField.GetValue());
                currentField = currentField.Right;
            }

            result.Add(row); 
            currentRow = currentRow.Bottom;
        }

        return result;
    }

    public void UpdateField(FieldInput fieldInput)
    {
        if (fieldInput.XCoordinate < GameboardSize && fieldInput.YCoordinate < GameboardSize)
        {
            _currentField = _topLeft;
            for (int i = 0; i < fieldInput.XCoordinate; i++)
            {
                _currentField = _currentField.Right;

            }

            for (int i = 0; i < fieldInput.YCoordinate; i++)
            {
                _currentField = _currentField.Bottom;
            }

            if (fieldInput.ActionType == FieldInput.Action.Reveal)
            {
                _currentField.Reveal();
            }

            if (fieldInput.ActionType == FieldInput.Action.Flag && _currentField.IsVisible != true && FlagCount <= BombCount -1)
            {
                _currentField.HasFlag = true;
                FlagCount++;
            }

            if (fieldInput.ActionType == FieldInput.Action.RemoveFlag && _currentField.HasFlag == true)
            {
                _currentField.HasFlag = false;
                FlagCount--;
            }
        }
    }

    public IEnumerator<Field> GetEnumerator()
    {
        var currentRow = _topLeft;
        while (currentRow != null)
        {
            var currentField = currentRow;
            while (currentField != null)
            {
                yield return currentField;
                currentField = currentField.Right;
            }
            currentRow = currentRow.Bottom;
        }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        var currentRow = _topLeft;
        while (currentRow != null)
        {
            var currentField = currentRow;
            while (currentField != null)
            {
                yield return currentField;
                currentField = currentField.Right;
            }   
            currentRow = currentRow.Bottom;
        }
    }
}

