using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace projeto_matriz
{
    internal class Program
    {
        //bloco principal (menu)
        private static bool debug = false; //bool para o debug
        public static int Main(string[] args)
        {
            //texto em ascii, apenas para estética
            Console.WriteLine("\n\t ______     ______     __     ______   ______   ______" +
                          "\n\t/\\  ___\\   /\\  == \\   /\\ \\   /\\  == \\ /\\__  _\\ /\\  __ \\" +
                          "\n\t\\ \\ \\____  \\ \\  __<   \\ \\ \\  \\ \\  _-/ \\/_/\\ \\/ \\ \\ \\/\\ \\" +
                          "\n\t \\ \\_____\\  \\ \\_\\ \\_\\  \\ \\_\\  \\ \\_\\      \\ \\_\\  \\ \\_____\\" +
                          "\n\t  \\/_____/   \\/_/ /_/   \\/_/   \\/_/       \\/_/   \\/_____/" +
                          "\n\t\t\t\t\t\t     Versão 0.2.1");

            Console.CursorVisible = false;
            ConsoleKey tecla;
            short selecionado = 0;
            Console.Write("\n\n\t\t\tCriptografar\n\t\t\tDescriptografar",
                          "\n\t\t\t???\n\t\t\tSair");
            do
            {
                //menu em setas, pressiona D para ativar o debug
                string[] opcao = { "Criptografar\t", "Descriptografar\t", "???\t", "Sair\t" };
                for (int i = 0; i < opcao.Length; i++)
                {
                    Console.SetCursorPosition(24, 9 + i);
                    if(i == selecionado)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{opcao[i]}");
                    Console.ResetColor();
                }
                tecla = Console.ReadKey(true).Key;

                //altera o valor da seleção
                switch (tecla)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (selecionado < opcao.Length)
                            {
                                selecionado++;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (selecionado > 0)
                            {
                                selecionado--;
                            }
                            break;
                        }
                    case ConsoleKey.D:
                        {
                            debug = true;
                            Console.SetCursorPosition(5, 6);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\tDEBUG ATIVO");
                            Console.ResetColor();
                            break;
                        }
                }
            } while (tecla != ConsoleKey.Enter);

            //executa a parte do código selecionada

            switch(selecionado)
            {
                case 0:
                    {
                        criptografar();
                        break;
                    }
                case 1:
                    {
                        descriptografar();
                        break;
                    }
                case 3:
                    {
                        return 0;
                    }
            }
            return 0;
        }
        static int criptografar()
        {
            Console.Clear();
            bool CPFusado = false;
            ConsoleKey tecla;
            short selecionado = 0;
            Console.WriteLine("\n\tDeseja utilizar o CPF para a criptografia?");
            Console.WriteLine("\tO uso do CPF resulta numa criptografia mais complexa");
            Console.Write("\n\n\t\tSim\n\t\tNão");
            do
            {
                string[] opcao = { "Sim\t", "Não\t" };
                for (int i = 0; i < opcao.Length; i++)
                {
                    Console.SetCursorPosition(16, 5 + i);
                    if (i == selecionado)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{opcao[i]}");
                    Console.ResetColor();
                }
                tecla = Console.ReadKey(true).Key;
                switch (tecla)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (selecionado < opcao.Length)
                            {
                                selecionado++;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (selecionado > 0)
                            {
                                selecionado--;
                            }
                            break;
                        }
                }
                if (selecionado == 0)
                {
                    CPFusado = true;
                }
            } while (tecla != ConsoleKey.Enter);

            //solicita e valida o CPF
            if (CPFusado)
            {
                Console.Clear();
                Console.WriteLine("\n\tDigite seu CPF (sem pontos ou hifen)");
                Console.Write("\tCPF: ");
                string CPF = Console.ReadLine();
                short[] nCPF = new short[11];
                int[] vCPF = new int[2];

                do
                {
                    Console.Clear();
                    while (CPF.Length != 11)
                    {
                        Console.Clear();
                        Console.WriteLine("\n\tCPF inválido, digite novamente.");
                        Console.Write("\tCPF: ");
                        CPF = Console.ReadLine();
                    }
                    for (int i = 0; i <= CPF.Length - 1; i++)
                    {
                        nCPF[i] = short.Parse(CPF[i].ToString());
                        if (debug)
                        {
                            Console.Write($"i[{i}]:{nCPF[i]}, ");
                        }
                    }

                    //se o DEBUG estiver ativado a verificação näo é feita
                    if (debug)
                    {
                        Console.WriteLine("\nPressione qualquer tecla para continuar");
                        Console.ReadKey();
                        break;
                    }

                    //início verificação
                    for (int i = 0; i < 9; i++)
                    {
                        vCPF[0] += (nCPF[i] * (10 - i));
                    }
                    for (int i = 1; i < 10; i++)
                    {
                        vCPF[1] +=  (nCPF[i] * (10 - i));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        vCPF[i] = vCPF[i] % 11;
                        if (vCPF[i] > 1)
                        {
                            vCPF[i] = 11 - vCPF[i];
                        }
                        else
                        {
                            vCPF[i] = 0;
                        }
                    }

                    if (vCPF[0] != nCPF[9] || vCPF[1] != nCPF[10])
                    {
                        Console.Clear();
                        Console.WriteLine("\n\tCPF inválido, digite novamente.");
                        Console.Write("\tCPF: ");
                        CPF = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("CPF válido.");
                    }
                    //fim verificação

                } while (CPF.Length != 11 || vCPF[0] != nCPF[9] || vCPF[1] != nCPF[10]);
            }
            //fim - CPF validado ou inutilizado
            
            Console.Clear();
            Console.WriteLine("\n\tDigite uma senha para criptografia");
            Console.WriteLine("\tSua senha deve possuir entre 4 e 25 caracteres.");
            Console.Write("\tSenha: ");
            StringBuilder senha = new StringBuilder(25);
            ConsoleKeyInfo key;
            bool erroSenha = false;

            string teclavalida = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_=+`~;:'\"/?\\|.>,< "; //91
            //entrada senha
            do
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                key = Console.ReadKey(true);
                for (int i = 0 ; i < teclavalida.Length; i++)
                {
                    if (key.KeyChar != teclavalida[i] && key.Key != ConsoleKey.Enter)
                    {
                        erroSenha = true;
                    }
                    else
                    {
                        erroSenha = false;
                        break;
                    }
                }
                if (key.Key == ConsoleKey.Backspace && senha.Length > 0)
                {
                    senha.Remove(senha.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter && !erroSenha)
                {
                    senha.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (key.Key != ConsoleKey.Enter || senha.Length <= 25);
            if (debug)
            {
                Console.WriteLine($"\nsenha:{senha}");
                Console.WriteLine("Pressione qualquer tecla para continuar");
                Console.ReadKey();
            }

            //começar parte das matrizes aqui
            //vvvvvv

            return 0;
        }
        static int descriptografar()
        {
            Console.Clear();
            bool CPFusado = false;
            ConsoleKey tecla;
            short selecionado = 0;
            Console.WriteLine("\n\tVocê utilizou seu CPF para a criptografia?");
            Console.Write("\n\n\t\tSim\n\t\tNão");
            do
            {
                string[] opcao = { "Sim\t", "Não\t" };
                for (int i = 0; i < opcao.Length; i++)
                {
                    Console.SetCursorPosition(16, 4 + i);
                    if (i == selecionado)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write($"{opcao[i]}");
                    Console.ResetColor();
                }
                tecla = Console.ReadKey(true).Key;
                switch (tecla)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (selecionado < opcao.Length)
                            {
                                selecionado++;
                            }
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (selecionado > 0)
                            {
                                selecionado--;
                            }
                            break;
                        }
                }
                if (selecionado == 0)
                {
                    CPFusado = true;
                }
            } while (tecla != ConsoleKey.Enter);

            return 0;
        }
    }
}
