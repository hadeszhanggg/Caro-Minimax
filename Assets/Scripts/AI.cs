using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AI : MonoBehaviour
{
    [SerializeField] private Board board;
    private string opponent; // Người chơi đối thủ (x hoặc o)
    public void InitAI(string opponent)
    {
        this.opponent = opponent;
    }

    public void MakeAIMove()
    {
        if (board.inTurn == "o")
        {
            Debug.Log("AI is making a move...");
            // Duyệt qua tất cả các ô trống trên bàn cờ
            int bestScore = -1;
            Vector2 bestMove = Vector2.zero;

            for (int i = 1; i <= board.boardSize; i++)
            {
                for (int j = 1; j <= board.boardSize; j++)
                {
                    if (board.matrix[i, j] == "")
                    {
                        board.matrix[i, j] = "o";
                        int score = EvaluateMove(i, j);

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = new Vector2(i, j);
                        }

                        board.matrix[i, j] = "";
                    }
                }
            }

            board.matrix[(int)bestMove.x, (int)bestMove.y] = "o";
            Cell aiCell = board.GetCell((int)bestMove.x, (int)bestMove.y);
            aiCell.changeChess("o");
            board.UpdateBoardUI();

            // Kiểm tra chiến thắng sau khi AI đánh
            if (board.Check((int)bestMove.x, (int)bestMove.y))
            {
                GameObject window = Instantiate(aiCell.GameOverWindow, aiCell.Canvas);
                window.GetComponent<GameOverWindow>().SetName(board.inTurn);
            }

            // Thay đổi lượt sau khi AI đã đánh
            board.inTurn = "x";
        }
        else
            return;
    }


    private int EvaluateMove(int row, int column)
    {
        int score = 0;
        // Thực hiện đánh giá mức độ thuận lợi dựa trên số 'x' liên tiếp trong các hàng, cột, đường chéo
        // Điểm số tăng lên khi có nhiều 'x' liên tiếp

        // Đánh giá hàng dọc
        int count = 0;
        for (int i = row - 1; i >= 1; i--)
        {
            if (board.matrix[i, column] == "x")
                count++;
            else
                break;
        }
        for (int i = row + 1; i <= board.boardSize; i++)
        {
            if (board.matrix[i, column] == "x")
                count++;
            else
                break;
        }
        if (count >= 3)
            score += 100;

        // Đánh giá hàng ngang
        count = 0;
        for (int i = column - 1; i >= 1; i--)
        {
            if (board.matrix[row, i] == "x")
                count++;
            else
                break;
        }
        for (int i = column + 1; i <= board.boardSize; i++)
        {
            if (board.matrix[row, i] == "x")
                count++;
            else
                break;
        }
        if (count >= 3)
            score += 100;

        // Đánh giá đường chéo 1
        count = 0;
        for (int i = column + 1, j = row - 1; i <= board.boardSize && j >= 1; i++, j--)
        {
            if (board.matrix[j, i] == "x")
                count++;
            else
                break;
        }
        for (int i = column - 1, j = row + 1; i >= 1 && j <= board.boardSize; i--, j++)
        {
            if (board.matrix[j, i] == "x")
                count++;
            else
                break;
        }
        if (count >= 3)
            score += 100;

        // Đánh giá đường chéo 2
        count = 0;
        for (int i = column + 1, j = row + 1; i <= board.boardSize && j <= board.boardSize; i++, j++)
        {
            if (board.matrix[j, i] == "x")
                count++;
            else
                break;
        }
        for (int i = column - 1, j = row - 1; i >= 1 && j >= 1; i--, j--)
        {
            if (board.matrix[j, i] == "x")
                count++;
            else
                break;
        }
        if (count >= 3)
            score += 100;

        // Các thang điểm khác có thể được thêm vào

        return score;
    }

}