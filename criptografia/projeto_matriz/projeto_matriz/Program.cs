﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace projeto_matriz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool debug = false;
            bool encrypt = false;
            ConsoleKey selecao;
            Console.WriteLine("\t~Projeto criptografia versão 0~");
            Console.WriteLine("\n\tDeseja criptografar ou descriptografar?\n");
            Console.Write("\tCriptografar\tDescriptografar");
            do
            {
                //menu em setas, pressiona D para ativar o debug
                selecao = Console.ReadKey().Key;
                if (selecao == ConsoleKey.LeftArrow)
                {
                    encrypt = true;
                    Console.SetCursorPosition(5, 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\tCriptografar");
                    Console.ResetColor();
                    Console.SetCursorPosition(20, 4);
                    Console.Write("\tDescriptografar");
                }
                else if (selecao == ConsoleKey.RightArrow)
                {
                    encrypt = false;
                    Console.SetCursorPosition(17, 4);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\tDescriptografar");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4);
                    Console.Write("\tCriptografar");
                }
                else if (selecao == ConsoleKey.D)
                {
                    debug = true;
                    Console.SetCursorPosition(5, 6);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\tDEBUG ATIVO");
                    Console.ResetColor();
                }
            } while (selecao != ConsoleKey.Enter);
            if(encrypt)
            {
                bool CPFusado = false;
                Console.WriteLine("\tDeseja utilizar o CPF para a criptografia?");
                Console.WriteLine("\tO uso do CPF resulta numa criptografia mais complexa");
                Console.WriteLine("\tSelecione utilizando as setas do teclado\n");
                Console.Write("\tSim\tNão");
                do
                {
                    selecao = Console.ReadKey().Key;
                    if (selecao == ConsoleKey.LeftArrow)
                    {
                        CPFusado = true;
                        Console.SetCursorPosition(5, 8);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tSim");
                        Console.ResetColor();
                        Console.SetCursorPosition(13, 8);
                        Console.Write("\tNão");
                    }
                    else if (selecao == ConsoleKey.RightArrow)
                    {
                        CPFusado = false;
                        Console.SetCursorPosition(13, 8);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\tNão");
                        Console.ResetColor();
                        Console.SetCursorPosition(5, 8);
                        Console.Write("\tSim");
                    }
                } while (selecao != ConsoleKey.Enter);

                //solicita e valida o CPF
                if (CPFusado)
                {
                    Console.Clear();
                    Console.WriteLine("\tDigite seu CPF (sem pontos ou hifen)");
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
                            Console.WriteLine("\tCPF inválido, digite novamente.");
                            Console.Write("\tCPF: ");
                            CPF = Console.ReadLine();
                        }
                        for (int i = 0; i <= CPF.Length - 1; i++)
                        {
                            nCPF[i] = short.Parse(CPF[i].ToString());
                            if (debug)
                            {
                                Console.Write($"i[{i}]:{nCPF[i]},");
                            }
                        }

                        //se o DEBUG estiver ativado a verificação näo é feita
                        //qualquer CPF poderá ser utilizado
                        if (debug)
                        {
                            break;
                        }

                        //início verificação
                        for (int i = 0; i <= 8; i++)
                        {
                            vCPF[0] = vCPF[0] + (nCPF[i] * (10 - i));
                        }
                        for (int i = 1; i <= 9; i++)
                        {
                            vCPF[1] = vCPF[1] + (nCPF[i] * (10 - i));
                        }
                        for (int i = 0; i <= 1; i++)
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
                            Console.WriteLine("\tCPF inválido, digite novamente.");
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
                StringBuilder senha = new StringBuilder(25);
                Console.WriteLine("\n\tDigite uma senha para criptografia");
                Console.WriteLine("\tSua senha deve possuir entre 4 e 25 caracteres.");
                Console.Write("\tSenha: ");
                ConsoleKeyInfo tecla;
                bool erroSenha = false;

                do
                {
                    int x = Console.CursorLeft;
                    int y = Console.CursorTop;
                    tecla = Console.ReadKey(true);
                    if (tecla.Key == ConsoleKey.Backspace && senha.Length > 0)
                    {
                        senha.Remove(senha.Length - 1, 1);
                        Console.SetCursorPosition(x - 1, y);
                        Console.Write(" ");
                        Console.SetCursorPosition(x - 1, y);
                    }
                    else if (tecla.Key != ConsoleKey.Backspace && tecla.Key != ConsoleKey.Enter)
                    {
                        senha.Append(tecla.KeyChar);
                        Console.Write("*");
                    }
                } while (tecla.Key != ConsoleKey.Enter || senha.Length == 25 || erroSenha);
                if (debug)
                {
                    Console.WriteLine($"\nsenha: {senha}");
                }
                Console.ReadKey();
            }
        }
    }
}
