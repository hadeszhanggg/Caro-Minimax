using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Board : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform board;
    public GridLayoutGroup gridLayout;
    public int boardSize;
    public string inTurn = "x";
    public string[,] matrix;
    public Transform Canvas;
    public void Awake()
    {
        
    }

    public void Start()
    {
        matrix = new string[boardSize + 1, boardSize + 1];
        gridLayout.constraintCount = boardSize;
        createBoard();
    }

    private void createBoard()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                GameObject cellTransform = Instantiate(cellPrefab, board);
                Cell cell = cellTransform.GetComponent<Cell>();
                cell.row = i;
                cell.collumn = j;
                matrix[i, j] = "";
            }
        }
    }

    public bool Check(int row, int collumn)
    {
        matrix[row, collumn] = inTurn;
        bool result = false;
        // Kiểm tra chiều dọc
        int count = 0;
        for (int i = row - 1; i >= 1; i--)
        {
            if (matrix[i, collumn] == inTurn)
                count++;
            else break;
        }
        for (int i = row + 1; i <= boardSize; i++)
        {
            if (matrix[i, collumn] == inTurn)
                count++;
            else break;
        }
        if (count + 1 >= 5)
            result = true;

        // Kiểm tra chiều ngang
        count = 0;
        for (int i = collumn - 1; i >= 1; i--)
        {
            if (matrix[row, i] == inTurn)
                count++;
            else break;
        }
        for (int i = collumn + 1; i <= boardSize; i++)
        {
            if (matrix[row, i] == inTurn)
                count++;
            else break;
        }
        if (count + 1 >= 5)
            result = true;

        // Kiểm tra đường chéo 1 (từ trái sang phải hướng lên)
        count = 0;
        for (int i = collumn + 1, j = row - 1; i <= boardSize && j >= 1; i++, j--)
        {
            if (matrix[j, i] == inTurn)
                count++;
            else break;
        }
        for (int i = collumn - 1, j = row + 1; i >= 1 && j <= boardSize; i--, j++)
        {
            if (matrix[j, i] == inTurn)
                count++;
            else break;
        }
        if (count + 1 >= 5)
            result = true;

        // Kiểm tra đường chéo 2 (từ trái sang phải hướng xuống)
        count = 0;
        for (int i = collumn + 1, j = row + 1; i <= boardSize && j <= boardSize; i++, j++)
        {
            if (matrix[j, i] == inTurn)
                count++;
            else break;
        }
        for (int i = collumn - 1, j = row - 1; i >= 1 && j >= 1; i--, j--)
        {
            if (matrix[j, i] == inTurn)
                count++;
            else break;
        }
        if (count + 1 >= 5)
            result = true;

        return result;
    }
    public void UpdateBoardUI()
    {
        for (int i = 1; i <= boardSize; i++)
        {
            for (int j = 1; j <= boardSize; j++)
            {
                string cellValue = matrix[i, j]; // Lấy giá trị của ô từ mảng matrix
                if (cellValue == "x" || cellValue == "o")
                {
                    // Cập nhật trạng thái của ô trực tiếp trong lớp Cell
                    Cell cell = GetCell(i, j);
                    cell.changeChess(cellValue);
                }
            }
        }
    }

    public Cell GetCell(int row, int col)
    {
        // Thay thế bằng cách tìm kiếm trong các ô trên bàn cờ và trả về ô tại hàng và cột cụ thể
        foreach (Transform child in board.transform)
        {
            Cell cell = child.GetComponent<Cell>();
            if (cell.row == row && cell.collumn == col)
            {
                return cell;
            }
        }
        return null;
    }

}
