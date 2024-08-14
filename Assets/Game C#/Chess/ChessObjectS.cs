using UnityEngine;

public class ChessObjectS : MonoBehaviour
{
    public GameObject B_bishop, B_horse, B_king, B_Pawn, B_queen, B_rock, W_bishop, W_horse, W_king, W_Pawn, W_queen, W_rock;


    public void SetFieldObjectModel(string color, string name)
    {
        string n = (name.Substring(0, 4));
        if (color == "Black")
        {
            if (n == "Pawn")
            {
                B_Pawn.gameObject.SetActive(true);
            }
            else if (n == "Knig")
            {
                B_horse.gameObject.SetActive(true);
            }
            else if (n == "Bish")
            {
                B_bishop.gameObject.SetActive(true);
            }
            else if (n == "Rook")
            {
                B_rock.gameObject.SetActive(true);
            }
            else if (n == "Quee")
            {
                B_queen.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("오류..");
            }
        }
        else if (color == "White")
        {
            if (n == "Pawn")
            {
                W_Pawn.gameObject.SetActive(true);
            }
            else if (n == "Knig")
            {
                W_horse.gameObject.SetActive(true);
            }
            else if (n == "Bish")
            {
                W_bishop.gameObject.SetActive(true);
            }
            else if (n == "Rook")
            {
                W_rock.gameObject.SetActive(true);
            }
            else if (n == "Quee")
            {
                W_queen.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("오류..");
            }
        }
    }
}
