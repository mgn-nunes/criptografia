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
        private static bool debug = false;
        private static bool CPFusado = false;
        public static int Main()
        {
            Console.WriteLine("\n\t ______     ______     __     ______   ______   ______" +
                          "\n\t/\\  ___\\   /\\  == \\   /\\ \\   /\\  == \\ /\\__  _\\ /\\  __ \\" +
                          "\n\t\\ \\ \\____  \\ \\  __<   \\ \\ \\  \\ \\  _-/ \\/_/\\ \\/ \\ \\ \\/\\ \\" +
                          "\n\t \\ \\_____\\  \\ \\_\\ \\_\\  \\ \\_\\  \\ \\_\\      \\ \\_\\  \\ \\_____\\" +
                          "\n\t  \\/_____/   \\/_/ /_/   \\/_/   \\/_/       \\/_/   \\/_____/" +
                          "\n\t\t\t\t\t\t     Versão 0.3.0");

            Console.CursorVisible = false;
            ConsoleKey tecla;
            short selecionado = 0;
            Console.Write("\n\n\t\t\tCriptografar\n\t\t\tDescriptografar",
                          "\n\t\t\t???\n\t\t\tSair");
            do
            {
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
                        exits();
                        break;
                    }
            }
            return 0;
        }
        static void criptografar()
        {
            Console.Clear();
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
                CPFcalc();
            }
            //fim - CPF validado ou inutilizado
            
            Console.Clear();
            Console.WriteLine("\n\tDigite uma senha para criptografia");
            Console.WriteLine("\tSua senha deve possuir entre 4 e 25 caracteres.");
            Console.Write("\tSenha: ");
            //stringbuilder para permitir a máscara da senha
            StringBuilder senha = new StringBuilder(25);
            ConsoleKeyInfo key;
            bool erroSenha = false;

            //C# já converte para a tabela ascii, a matriz não precisa dessa string pra ser montada
            //essa string serve apenas para a construção da senha
            //referência tabela ascii https://www.dotnetperls.com/ascii-table
            string teclavalida = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()ÁáÀàÉéÓóÚúÍíÂâÃãÕõÊêÇç-_=+`~;:'\"/?\\|.>,< ";
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
                    //remove um caractere do stringbuilder apenas se for maior que 0
                    senha.Remove(senha.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter && !erroSenha && senha.Length <= 25)
                {
                    //escreve um asterístico na posição atual do stringbuilder
                    senha.Append(key.KeyChar);
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (key.Key != ConsoleKey.Enter || senha.Length < 4);
            if (debug)
            {
                Console.WriteLine($"\nsenha:{senha}");
                Console.WriteLine("Pressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
            }

            //indica as dimensoes da matriz
            short dMatriz = (short)Math.Ceiling(Math.Sqrt(senha.Length));

            //essa parte insere caracteres aleatórios para concluir a matriz quadrada
            //caso o usuário não digite uma senha grande o suficiente
            short preencher = (short)Math.Pow(dMatriz, 2);
            Random rnd = new Random();
            for (int i = senha.Length; i < preencher; i++)
            {
                short crandom = (short)rnd.Next(teclavalida.Length);
                senha.Append(teclavalida[crandom]);
            }
            
            if(debug)
            {
                Console.WriteLine("ordem da matriz: "+dMatriz+"senha alterada: "+senha);
            }

            int[,] matriz = new int[dMatriz,dMatriz];

            //monta a matriz de acordo com as dimensoes
            for (int i = 0; i < dMatriz; i++)
            {
                for (int j = 0; j < dMatriz; j++)
                {
                    //converte para o valor do caractere na tabela ascii
                    matriz[i,j] = senha[(i*dMatriz)+j];
                    if (debug)
                    {
                        Console.Write($"[{i},{j}]={matriz[i,j]};");
                    }
                }
            }


            Console.ReadKey();
            exits();
        }
        //usar algum tipo de TRIM para remover os caracteres do texto original caso a senha não
        //tenha caracteres suficientes para uma matriz quadrada > devido ao random
        static void descriptografar()
        {
            Console.Clear();
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
        }
        static void texto()
        {
            //só é possível ler múltiplas linhas de texto a partir de arquivos
            string textoEntrada;
            Console.WriteLine("\n\n\tDigite ou copie e cole o texto a ser criptografado/descriptografado");
            Console.Write("\n\tTexto: ");
            textoEntrada = Console.ReadLine();
        }
        static void CPFcalc()
        {
            Console.Clear();
            Console.WriteLine("\n\tDigite seu CPF (sem pontos ou hifen)");
            Console.Write("\tCPF: ");
            string CPF = Console.ReadLine();
            short[] nCPF = new short[11];
            int[] vCPF = new int[2];
            bool erroCPF = false;

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
                }

                //início verificação
                for (int i = 0; i < 9; i++)
                {
                    vCPF[0] = vCPF[0] + (nCPF[i] * (10 - i));
                }
                for (int i = 1; i < 10; i++)
                {
                    vCPF[1] = vCPF[1] + (nCPF[i] * (11 - i));
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
                    vCPF[0] = 0;
                    vCPF[1] = 0;
                    erroCPF = true;
                }
                else
                {
                    Console.WriteLine("\n\tCPF válido.\n\tPressione qualquer tecla para continuar");
                    Console.ReadKey();
                    erroCPF = false;
                }
            } while (CPF.Length != 11 || erroCPF);
        }
        static void detCalc()
        {
            //fazer inversa por algoritmo de bareiss ou eliminação de gauss-jordan
            //equação geral da determinante

        }
        static void invCalc()
        {

        }
        static int exits()
        {
            Console.Clear();
            Console.WriteLine("\n\tTem certeza que deseja sair?");
            ConsoleKey tecla;
            short selecionado = 0;
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
            } while (tecla != ConsoleKey.Enter);
            if (selecionado == 0)
            {
                return 0;
            }
            else
            {
                Console.Clear();
                Main();
            }
            return 0;
        }
    }
}
