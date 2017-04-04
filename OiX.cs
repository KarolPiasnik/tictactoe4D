using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OiX
{
    class oix
    {
        private bool?[,,,] gameBoard; // true - x, false o, null - nic
        //wymiary: [x - numer wiersza, y - numer kolumny, z - głębia, t - numer planszy do gry]
        //zawsze stosowane w tej kolejności, t ostatnie między innymi z powodu planu zmiany jego roli z numeru planszy na pełnoprawny wymiar

        private int size;

        public oix(int size) // tworzenie planszy do gry
        {
            gameBoard = new bool?[size, size, size, size];
            for(int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                        for(int o = 0; o < size; o++) // wypełnienie planszy pustą przestrzenią
                            {
                                gameBoard[i, j, k, o] = null; 
                            }           
            this.size = size;
            
        }

        public bool insert(int x, int y, int z, int t, bool what) // typ bool ponieważ metoda zwraca informację o tym czy wstawienie było możliwe
        {
            if (gameBoard[x, y, z, t] == null)
            {
                gameBoard[x, y, z, t] = what;
                return true;
            }
            else
                return false;
        }
       
        public bool? check() //zwraca informację kto wygrał
        {

            /*
            Ogólny algorytm sprawdzania:
            * program porusza się po danej linii lub przekątnej układu porównując wartości kolejnych komórek do wartości jej pierwszej komórki
            * jeśli wartości komórek nie są równe następuje przerwanie wykonywanej oktualnie pętli i przejście do sprawdzania kolejnych możliwości
            * jeśli któraś komórka jest pusta również następuje przerwanie i przejście dalej 
            * jeśli pętla nie zostało przerwana oznacza to zwycięstwo, zwracana jest wartośc 1 komórki porównywanej w pętli
            * jeśli nie zostanie wykryte zwycięstwo funkcja zwraca informację o tym, że nikt nie wygrał (null)
            */

            bool win = true;
            
        

            // sprawdzanie zwycięstwa w linii ------------------------------------------------------------------------------------------------------------------------------
            for (int i = 0; i < size; i++)       //3 wymiar, głębia sześcianu  złożoność n^4
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                    {

                        for (int o = 0; o < size; o++)
                            if (gameBoard[j, k, o, i] == null || gameBoard[j, k, o, i] != gameBoard[j, k, 0, i]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                            {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)                                // tu następuje sprawdzenie czy pętla sprawdzająca została przerwana 
                            return gameBoard[j, k, 0, i];       // jeśli nie (win==true) to zwracana jest wartość pierwszej komórki sprawdzanej przez ostatnią pętlę
                        else
                            win = true;     // jeśli pętla została przerwana to następuje przwrócenie wartości zmiennej win i kontynuacja sprawdzania
                    }


            /*
             * UWAGA !
             * Dla wszystkich pozostałych pętli sprawdzających sprawdzanie wygląda identycznie, inne są tylko komórki sprawdzane
             * i stopień zagnieżdżenia pętli sprawdzającej
             */


            for (int i = 0; i < size; i++) //4 wymiar złożonośc n^4
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                    {

                        for (int o = 0; o < size; o++)
                            if (gameBoard[i, j, k, o] == null || gameBoard[i, j, k, o] != gameBoard[i, j, k, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                            {                             // komórki to przerywane sprawdzanie tego wiersza, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[i, j, k, 0];
                        else
                            win = true;
                    }

            for (int i = 0; i < size; i++)       //2 wymiar, szerokość sześcianu złożoność n^4
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                    {

                        for (int o = 0; o < size; o++)
                            if (gameBoard[j, o, i, k] == null || gameBoard[j, o, i, k] != gameBoard[j, 0, i, k]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                            {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[j, 0, i, k];
                        else
                            win = true;
                    }

            for (int i = 0; i < size; i++)       //1 wymiar, wysokość sześcianu złożoność n^4
                for (int j = 0; j < size; j++)
                    for (int k = 0; k < size; k++)
                    {

                        for (int o = 0; o < size; o++)
                            if (gameBoard[o, j, k, i] == null || gameBoard[o, j, k, i] != gameBoard[0, j, k, i]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                            {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[0, j, k, i];
                        else
                            win = true;
                    }
            // przekąye 4 wymiarowe
            for (int i = 0; i < size; i++)
                if (gameBoard[i, i, i, i] == null || gameBoard[i, i, i, i] != gameBoard[0, 0, 0, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, 0, 0, 0];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[i, i, i, size - i - 1] == null || gameBoard[i, i, i, size - i - 1] != gameBoard[0, 0, 0, size - 1]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, 0, 0, size - 1];
            else
                win = true;



            for (int i = 0; i < size; i++)
                if (gameBoard[i, i, size - i - 1, i] == null || gameBoard[i, i, size - i - 1, i] != gameBoard[0, 0, size - 1, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, 0, size - 1, 0];
            else
                win = true;



            for (int i = 0; i < size; i++)
                if (gameBoard[i, size - i - 1, i, i] == null || gameBoard[i, size - i - 1, i, i] != gameBoard[0, size - 1, 0, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, size -1, 0, 0];
            else
                win = true;

            for (int i = 0; i < size; i++)
                if (gameBoard[size - i - 1, i, i, i] == null || gameBoard[size - i - 1, i, i, i] != gameBoard[size -1, 0, 0, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[size -1, 0, 0, 0];
            else
                win = true;
            // dwójki----------------------------------------------------------------------------------------------------------------------
            for (int i = 0; i < size; i++)
                if (gameBoard[size - i - 1, size - i - 1, i, i] == null || gameBoard[size - i - 1, size - i - 1, i, i] != gameBoard[size - 1, size - 1, 0, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[size - 1, size - 1, 0, 0];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[size - i - 1, i, size - i - 1, i] == null || gameBoard[size - i - 1, i, size - i - 1, i] != gameBoard[size - 1, 0, size - 1, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[size - 1, 0, size - 1, 0];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[size - i - 1, i, i, size - i - 1] == null || gameBoard[size - i - 1, i, i, size - i - 1] != gameBoard[size - 1, 0, 0, size - 1]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[size - 1, 0, 0, size - 1];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[i, size - i - 1, size - i - 1, i] == null || gameBoard[i, size - i - 1, size - i - 1, i] != gameBoard[0, size - 1, size - 1, 0]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, size - 1, size - 1, 0];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[i, size - i - 1, i, size - i - 1] == null || gameBoard[i, size - i - 1, i, size - i - 1] != gameBoard[0, size - 1, 0, size - 1]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, size - 1, 0, size - 1];
            else
                win = true;


            for (int i = 0; i < size; i++)
                if (gameBoard[i, i, size - i - 1, size - i - 1] == null || gameBoard[i, i, size - i - 1, size - i - 1] != gameBoard[0, 0, size - 1, size - 1]) // jeśli komórka wiersza jest pusta lub różna od wartości pierwszej 
                {                             // komórki to przerywane sprawdzanie, bo nie ma to już sensu
                    win = false;
                    break;
                }

            if (win)
                return gameBoard[0, 0, size - 1, size - 1];
            else
                win = true;



            // przekątne w trójwymiarze -----------------------------------------------------------------------------------

            for (int i = 0; i < size; i++)// złożoność 16n^2
            {
                for (int j = 0; j < size; j++)
                    if (gameBoard[j, j, j, i] == null || gameBoard[j, j, j, i] != gameBoard[0, 0, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, 0, 0, i];
                else
                    win = true;



                for (int j = 0; j < size; j++)
                    if (gameBoard[size - j - 1, j, j, i] == null || gameBoard[size - j - 1, j, j, i] != gameBoard[size - 1, 0, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[size - 1, 0, 0, i];
                else
                    win = true;




                for (int j = 0; j < size; j++)
                    if (gameBoard[j, size - j - 1, j, i] == null || gameBoard[j, size - j - 1, j, i] != gameBoard[0, size - 1, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, size - 1, 0, i];
                else
                    win = true;




                for (int j = 0; j < size; j++)
                    if (gameBoard[j, j, size - j - 1, i] == null || gameBoard[j, j, size - j - 1, i] != gameBoard[0, 0, size -1, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, 0, size -1, i];
                else
                    win = true;


                //-------------------------------------------------------------------------------------------------------------


                for (int j = 0; j < size; j++)
                    if (gameBoard[i, j, j, j] == null || gameBoard[i, j, j, j] != gameBoard[i, 0, 0, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[i, 0, 0, 0];
                else
                    win = true;




                for (int j = 0; j < size; j++)
                    if (gameBoard[i, j, size - j - 1, j] == null || gameBoard[i, j, size - j - 1, j] != gameBoard[i, 0, size - 1, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[i, 0, size - 1, 0];
                else
                    win = true;




                for (int j = 0; j < size; j++)
                    if (gameBoard[i, size - j - 1, j, j] == null || gameBoard[i, size - j - 1, j, j] != gameBoard[i, size - 1, 0, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[i, size - 1, 0, 0];
                else
                    win = true;




                for (int j = 0; j < size; j++)
                    if (gameBoard[i, j, j, size - j - 1] == null || gameBoard[i, j, j, size - j - 1] != gameBoard[i, 0, 0, size - 1]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[i, 0, 0, size - 1];
                else
                    win = true;

                //--------------------------------------------------------------------------------------------------------------


                for (int j = 0; j < size; j++)
                    if (gameBoard[j, i, j, j] == null || gameBoard[j, i, j, j] != gameBoard[0, i, 0, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, i, 0, 0];
                else
                    win = true;



                for (int j = 0; j < size; j++)
                    if (gameBoard[size - j - 1, i, j, j] == null || gameBoard[size - j - 1, i, j, j] != gameBoard[size - 1, i, 0, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[size - 1, i, 0, 0];
                else
                    win = true;



                for (int j = 0; j < size; j++)
                    if (gameBoard[j, i, size - j - 1, j] == null || gameBoard[j, i, size - j - 1, j] != gameBoard[0, i, size - 1, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, i, size - 1, 0];
                else
                    win = true;



                for (int j = 0; j < size; j++)
                    if (gameBoard[j, i, j, size - j - 1] == null || gameBoard[j, i, j, size - j - 1] != gameBoard[0, i, 0, size - 1]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, i, 0, size -1];
                else
                    win = true;
                //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                for (int j = 0; j < size; j++)
                    if (gameBoard[j, j, i, j] == null || gameBoard[j, j, i, j] != gameBoard[0, 0, i, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, 0, i, 0];
                else
                    win = true;

                for (int j = 0; j < size; j++)
                    if (gameBoard[j, j, i, size - j -1] == null || gameBoard[j, j, i, size - j -1] != gameBoard[0, 0, i, size -1]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, 0, i, size -1];
                else
                    win = true;

                for (int j = 0; j < size; j++)
                    if (gameBoard[j, size - j - 1, i, j] == null || gameBoard[j, size - j - 1, i, j] != gameBoard[0, size -1, i, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[0, size -1, i, 0];
                else
                    win = true;

                for (int j = 0; j < size; j++)
                    if (gameBoard[size - j - 1, j, i, j] == null || gameBoard[size - j - 1, j, i, j] != gameBoard[size -1, 0, i, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                    {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                        win = false;
                        break;
                    }

                if (win)
                    return gameBoard[size -1, 0, i, 0];
                else
                    win = true;

            }


            // przekątne na płaszczyźnie -----------------------------------------------------------------------------------
            // złożoność 12n^3
                for (int i = 0; i < size; i++) // front 2 przekątne 
                {
                    for (int j = 0; j < size; j++)
                    {
                        for (int k = 0; k < size; k++)
                            if (gameBoard[k, k, j, i] == null || gameBoard[k, k, j, i] != gameBoard[0, 0, j, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[0, 0, j, i];
                        else
                            win = true;




                        for (int k = 0; k < size; k++)
                            if (gameBoard[size - k - 1, k, j, i] == null || gameBoard[size - k - 1, k, j, i] != gameBoard[size - 1, 0, j, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[size - 1, 0, j, i];
                        else
                            win = true;



                        //---------------------------------------------------------------------------------------------------------------------------------------------------
                        //bok 2 przekątne 

                        for (int k = 0; k < size; k++)
                            if (gameBoard[j, k, k, i] == null || gameBoard[j, k, k, i] != gameBoard[j, 0, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[j, 0, 0, i];
                        else
                            win = true;




                        for (int k = 0; k < size; k++)
                            if (gameBoard[j, k, size - k - 1, i] == null || gameBoard[j, k, size - k - 1, i] != gameBoard[j, 0, size - 1, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                            // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[j, 0, size - 1, i];
                        else
                            win = true;



                        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //pion 2 przekątne 

                        for (int k = 0; k < size; k++)
                            if (gameBoard[k, j, k, i] == null || gameBoard[k, j, k, i] != gameBoard[0, j, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[0, j, 0, i];
                        else
                            win = true;

                        for (int k = 0; k < size; k++)
                            if (gameBoard[size - k - 1, j, k, i] == null || gameBoard[size - k - 1, j, k, i] != gameBoard[size - 1, j, 0, i]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[size - 1, j, 0, i];
                        else
                            win = true;





                        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        for (int k = 0; k < size; k++) //
                            if (gameBoard[i, j, k, k] == null || gameBoard[i, j, k, k] != gameBoard[i, j, 0, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[i, j, 0, 0];
                        else
                            win = true;


                        for (int k = 0; k < size; k++)
                            if (gameBoard[i, j, size - k -1, k] == null || gameBoard[i, j, size - k -1, k] != gameBoard[i, j, size -1, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                            {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                                win = false;
                                break;
                            }

                        if (win)
                            return gameBoard[i, j, size -1, 0];
                        else
                            win = true;
                    
                    //-------------------------------------------------------------------------------------------------------
                    for (int k = 0; k < size; k++) //
                        if (gameBoard[k, j, i, k] == null || gameBoard[k, j, i, k] != gameBoard[0, j, i, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                        {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                            win = false;
                            break;
                        }

                    if (win)
                        return gameBoard[0, j, i, 0];
                    else
                        win = true;

                    
                    for (int k = 0; k < size; k++) //
                        if (gameBoard[size - k -1, j, i, k] == null || gameBoard[size - k - 1, j, i, k] != gameBoard[size -1, j, i, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                        {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                            win = false;
                            break;
                        }

                    if (win)
                        return gameBoard[size -1, j, i, 0];
                    else
                        win = true;

                    //----------------------------------------------------------------------------------------------------
                    for (int k = 0; k < size; k++) //
                        if (gameBoard[i, k, j, k] == null || gameBoard[i, k, j, k] != gameBoard[i, 0, j, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                        {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                            win = false;
                            break;
                        }

                    if (win)
                        return gameBoard[i, 0, j, 0];
                    else
                        win = true;

                    for (int k = 0; k < size; k++) //
                        if (gameBoard[i, size - k -1, j, k] == null || gameBoard[i, size - k -1, j, k] != gameBoard[i, size -1, j, 0]) // jeśli komórka przękątnej jest pusta lub różna od wartości jej pierwszej 
                        {                             // komórki to przerywane sprawdzanie tej przekątnej, bo nie ma to już sensu
                            win = false;
                            break;
                        }

                    if (win)
                        return gameBoard[i, size -1, j, 0];
                    else
                        win = true;
                    }
                }  
            return null;
        }
    }
}
