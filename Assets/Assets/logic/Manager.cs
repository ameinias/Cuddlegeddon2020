using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager{

    public static int player1Score = 0;
    public static int player2Score = 0;



    public static void Score1(string player) {
        if (player == "P1")
            player1Score++;
        else
            player2Score++;
    }





    public static void Reset() {
        player1Score = 0;
        player2Score = 0;
    }


}
