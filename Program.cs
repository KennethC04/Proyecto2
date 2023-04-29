namespace ProyectoJuego
{
    
namespace Juego
    {
        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Bienvenido a CONNECT FOUR");
                Console.WriteLine("Juega en parejas o contra la compu");
                Console.WriteLine("Ingrese '1' para jugar contra la compu o '2' para jugar con tu amigo");
                int numJugadores = Convert.ToInt32(Console.ReadLine());
                string jugador1, jugador2;
                if (numJugadores == 1)
                {
                    jugador1 = "Jugador 1";
                    jugador2 = "bot1";
                }
                else
                {
                    Console.WriteLine("Como te llamas jugador 1: ");
                    jugador1 = Console.ReadLine();
                    Console.WriteLine("Como te llamas vos jugador 2:");
                    jugador2 = Console.ReadLine();
                    while (jugador1 == "COMPUTADORA" || jugador2 == "COMPUTADORA" || jugador1 == jugador2)
                    {
                        Console.WriteLine("Si jugas contra la computadora, no podes nombrarte computadora tambien");
                        Console.WriteLine("Los nicknames no pueden ser identicos");
                        Console.WriteLine("Ingrese el nombre del primer jugador");
                        jugador1 = Console.ReadLine();
                        Console.WriteLine("Ingrese el nombre del segundo jugador");
                        jugador2 = Console.ReadLine();
                    }
                }
                int[,] tablero = new int[6, 7];
                bool turnoJugador1 = true;
                bool juegoActivo = true;
                while (juegoActivo)
                {
                    int columna;
                    if (turnoJugador1)
                    {
                        Console.WriteLine(jugador1 + ", es tu turno");
                        columna = Convert.ToInt32(Console.ReadLine()) - 1;
                        while (columna < 0 || columna > 6 || tablero[0, columna] != 0)
                        {
                            Console.WriteLine("Movimiento inválido. Intenta de nuevo");
                            columna = Convert.ToInt32(Console.ReadLine()) - 1;
                        }
                        int fila = 5;
                        while (tablero[fila, columna] != 0)
                        {
                            fila--;
                        }
                        tablero[fila, columna] = 1;
                    }
                    else
                    {
                        Console.WriteLine(jugador2 + ", es tu turno");
                        if (jugador2 == "bot")
                        {
                            Random rnd = new Random();
                            columna = rnd.Next(0, 7);
                            while (tablero[0, columna] != 0)
                            {
                                columna = rnd.Next(0, 7);
                            }
                            int fila = 5;
                            while (tablero[fila, columna] != 0)
                            {
                                fila--;
                            }
                            tablero[fila, columna] = 2;
                            Console.WriteLine("el bot jugó en la columna " + (columna + 1));
                        }
                        else
                        {
                            columna = Convert.ToInt32(Console.ReadLine()) - 1;
                            while (columna < 0 || columna > 6 || tablero[0, columna] != 0)
                            {
                                Console.WriteLine("Movimiento inválido. Intenta de nuevo");
                                columna = Convert.ToInt32(Console.ReadLine()) - 1;
                            }
                            int fila = 5;
                            while (tablero[fila, columna] != 0)
                            {
                                fila--;
                            }
                            tablero[fila, columna] = 2;
                        }
                    }
                    ImprimirTablero(tablero);

                    if (Ganador(tablero, 1))
                    {
                        Console.WriteLine(jugador1 + " ganó la partida!");
                        juegoActivo = false;
                        RegistrarGanador(jugador1);
                        ImprimirUltimosGanadores();
                    }
                    else if (Ganador(tablero, 2))
                    {
                        Console.WriteLine(jugador2 + " ganó la partida!");
                        juegoActivo = false;
                        RegistrarGanador(jugador2);
                        ImprimirUltimosGanadores();
                    }
                    else if (Empate(tablero))
                    {
                        Console.WriteLine("El juego terminó en empate, animense a otra");
                        juegoActivo = false;
                    }
                    else
                    {
                        turnoJugador1 = !turnoJugador1;
                    }
                }
                Console.WriteLine("¿Otra partida? Quitense el desempate");
                Console.WriteLine("Ingrese '1' para iniciar una nueva partida o '2' para salir");
                int opcion = Convert.ToInt32(Console.ReadLine());
                if (opcion == 1)
                {
                    Main(args);
                }
                else
                {
                    Console.WriteLine("Te esperamos a la proxima, espero se hayan divertido!");
                }
            }

            static bool Ganador(int[,] tablero, int jugador)
            {
                // Verificar filas
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (tablero[i, j] == jugador && tablero[i, j + 1] == jugador && tablero[i, j + 2] == jugador && tablero[i, j + 3] == jugador)
                        {
                            return true;
                        }
                    }
                }
                // Verificar columnas
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (tablero[i, j] == jugador && tablero[i + 1, j] == jugador && tablero[i + 2, j] == jugador && tablero[i + 3, j] == jugador)
                        {
                            return true;
                        }
                    }
                }
                // Verificar diagonales
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (tablero[i, j] == jugador && tablero[i + 1, j + 1] == jugador && tablero[i + 2, j + 2] == jugador && tablero[i + 3, j + 3] == jugador)
                        {
                            return true;
                        }
                    }
                }
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 3; j < 7; j++)
                    {
                        if (tablero[i, j] == jugador && tablero[i + 1, j - 1] == jugador && tablero[i + 2, j - 2] == jugador && tablero[i + 3, j - 3] == jugador)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            static bool Empate(int[,] tablero)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (tablero[i, j] == 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            static void ImprimirTablero(int[,] tablero)
            {
                Console.WriteLine(" 1 2 3 4 5 6 7");
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Console.Write("|");
                        if (tablero[i, j] == 0)
                        {
                            Console.Write(" ");
                        }
                        else if (tablero[i, j] == 1)
                        {
                            Console.Write("O");
                        }
                        else
                        {
                            Console.Write("X");
                        }
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine("---------------");
            }

            static void RegistrarGanador(string jugador)
            {
                List<string> ganadores = new List<string>();
                if (System.IO.File.Exists("ganadores.txt"))
                {
                    string[] lines = System.IO.File.ReadAllLines("ganadores.txt");
                    foreach (string line in lines)
                    {
                        ganadores.Add(line);
                    }
                }
                ganadores.Add(DateTime.Now.ToString() + " - " + jugador);
                if (ganadores.Count > 10)
                {
                    ganadores.RemoveAt(0);
                }
                System.IO.File.WriteAllLines("ganadores.txt", ganadores.ToArray());
            }

            static void ImprimirUltimosGanadores()
            {
                Console.WriteLine("Últimos 10 ganadores:");
                if (System.IO.File.Exists("ganadores.txt"))
                {
                    string[] lines = System.IO.File.ReadAllLines("ganadores.txt");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
        }
    }
}
