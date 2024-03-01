using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cell : MonoBehaviour
{
    public GameObject GameOverWindow;
    public Board board;
    [SerializeField] private Sprite quanX;
    [SerializeField] private Sprite quanO;
    public Transform Canvas;
    private Image img;
    public int row;
    public int collumn;
    private Button btn;
    public AI bot;
    private void Awake()
    {
        img = GetComponent<Image>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    private void Start()
    {
        board = FindObjectOfType<Board>();
        Canvas = FindObjectOfType<Canvas>().transform;
        bot = FindObjectOfType<AI>();
    }
    public void changeChess(string chess)
    {
        if (chess == "x" && img.sprite != quanO)
        {
            img.sprite = quanX;
        }
        else if(chess=="x" && img.sprite==quanO)
        {
            Debug.Log("You must choose empty cell!!!");
            return;
        } else
            img.sprite = quanO;

    }
    public void OnClick()
    {
        if (board.inTurn != "x")
        {
            return; // Chỉ cho phép người chơi 'x' đánh khi đến lượt họ
        }

        changeChess(board.inTurn);

        // Thêm dòng này để cập nhật trạng thái hiển thị của ô trên bàn cờ
        board.matrix[row, collumn] = board.inTurn;
        if (board.Check(row, collumn))
        {
            GameObject window = Instantiate(GameOverWindow, Canvas);
            window.GetComponent<GameOverWindow>().SetName(board.inTurn);
        }
        if (board.inTurn == "x")
        {
            board.inTurn = "o";
            bot.MakeAIMove();
        }
    }

}
