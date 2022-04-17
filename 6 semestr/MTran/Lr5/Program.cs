using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ConsoleApp1.LexicalAnalizer;
using static ConsoleApp1.Token;




namespace ConsoleApp1
{

    class Program
    {

        static void PrintTokensDictionary(Dictionary<string, Token> dictionary)
        {
            ConsoleTable consoleTable = new ConsoleTable("TOKEN", "DESCRIPTION");
            foreach (Token token in dictionary.Values)
            {
                consoleTable.AddRow(token.Value, token.DescriptionString);
            }
            consoleTable.Write();
        }

        static string PrintNodeWithChildren(SyntaxAnalizer.ExpressionNode node, string indentation)
        {
            if (node == null)
            {
                return "";
            }
            SyntaxAnalizer.ValidateNode(node);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{indentation} {node.Operator.Value}");
            if (node.Left != null)
                stringBuilder.Append(PrintNodeWithChildren(node.Left, indentation + "\\"));
            if (node.Right != null)
                stringBuilder.Append(PrintNodeWithChildren(node.Right, indentation + "\\"));
            return stringBuilder.ToString();
        }

        static void PrintSyntaxTree(IEnumerable<SyntaxAnalizer.ExpressionNode> nodes, int nestingLevel = 1)
        {
            string indentation = new String('|', nestingLevel);
            foreach (var node in nodes)
            {

                Console.Write(PrintNodeWithChildren(node, indentation));
                Console.WriteLine(indentation);
                PrintSyntaxTree(node.Block, nestingLevel + 1);
            }
        }

        static string errorDescription(int indexInCodeLine, string codeLine)
        {
            StringBuilder stringBuilder = new StringBuilder(codeLine);
            stringBuilder.AppendLine();
            stringBuilder.Append(new string(' ', indexInCodeLine));
            stringBuilder.Append('^');
            return stringBuilder.ToString();
        }

        static void DoTheJob(IEnumerable<string> codeLines)
        {
            Dictionary<string, Token> constants = new Dictionary<string, Token>();
            Dictionary<string, Token> variables = new Dictionary<string, Token>();
            Dictionary<string, Token> operators = new Dictionary<string, Token>();
            Dictionary<string, Token> keywords = new Dictionary<string, Token>();

            List<LexicalError> errors = new List<LexicalError>();

            // running lexical analysis

            TreeList<SyntaxAnalizer.ExpressionNode> tree = new TreeList<SyntaxAnalizer.ExpressionNode>(null);
            TreeList<SyntaxAnalizer.ExpressionNode> currentBlock = tree;
            int lineNumber = 0;
            SyntaxAnalizer sa = new SyntaxAnalizer();
            LexicalAnalizer la = new LexicalAnalizer();
            int previousLineIndentation = 0;

            foreach (string line in codeLines)
            {
                Construction construction = la.AnaliseLine(line, lineNumber);
                if (construction.Tokens.Count == 0)
                {
                    lineNumber++;
                    continue;
                }
                for (int i = 0; i < construction.Tokens.Count; i++)
                {
                    Token token = construction.Tokens[i];
                    if (token.IsReservedIdToken)
                        keywords.TryAdd(token.Value, token);
                    else if (token.IsOperation)
                        operators.TryAdd(token.Value, token);
                    else if (token.IsConstant)
                        constants.TryAdd(token.Value, token);
                    else if (token.TokenType != TokenTypes.UNKNOWN)
                    {
                        variables.TryAdd(token.Value, token);
                    }
                }

                if (construction.HasErrors)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\t\t ERRORS");
                    Console.ResetColor();
                    foreach (LexicalError error in construction.Errors)
                    {
                        Console.WriteLine($"line {error.CodeLineNumber + 1} char {error.IndexInCodeLine + 1} :: {error.ErrorType}");
                        Console.WriteLine(error.Description);
                    }
                    Console.Read();
                    Environment.Exit(1);
                }

                SyntaxAnalizer.ExpressionNode node = null;
                bool isElifElseNode = false;
                bool newBlockToOpen = false;

                node = sa.Analyse(construction.Tokens, out newBlockToOpen, out isElifElseNode);

                int indentationDiff = previousLineIndentation - construction.Indentation;
                if (indentationDiff > 0)
                {
                    for (int i = previousLineIndentation - 1; i >= construction.Indentation; i--)
                    {
                        currentBlock = currentBlock.Parent;
                        if (currentBlock.Indentation == i)
                            break;
                    }
                    // currentBlock = currentBlock.Parent; // TODO: create parent relationship between BLOCKS to support >1 level nesting
                    if (node.Operator.IsElif && !currentBlock.Last().Operator.IsIf)
                    {
                        throw new SyntaxAnalizer.SyntaxErrorException(
                                "elif block not allowed here",
                                node.Operator.Value,
                                node.Operator.CodeLineIndex,
                                node.Operator.CodeLineNumber
                            );
                    }
                    else if (node.Operator.IsElse && !(currentBlock.Last().Operator.IsIf || currentBlock.Last().Operator.IsElif))
                    {
                        throw new SyntaxAnalizer.SyntaxErrorException(
                                "else block not allowed here",
                                line,
                                node.Operator.CodeLineIndex,
                                node.Operator.CodeLineNumber
                            );
                    }
                }
                previousLineIndentation = construction.Indentation;
                lineNumber++;
                if (newBlockToOpen)
                {
                    if ((node.Operator.IsElif || node.Operator.IsElse) && !currentBlock.Last().Operator.IsIf && !currentBlock.Last().Operator.IsElif)
                    {
                        throw new SyntaxAnalizer.SyntaxErrorException(
                            "lacks IF clause for elif|else block to appear",
                            node.Operator.Value,
                            node.Operator.CodeLineIndex,
                            node.Operator.CodeLineNumber
                            );
                    }
                    currentBlock.Add(node);
                    currentBlock.Last().Block = new TreeList<SyntaxAnalizer.ExpressionNode>(currentBlock);
                    currentBlock = currentBlock.Last().Block;
                    currentBlock.Indentation = construction.Indentation;
                    continue;
                }

                currentBlock.Add(node);
            }
            if (errors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t ERRORS");
                Console.ResetColor();
                foreach (LexicalError error in errors)
                {
                    Console.WriteLine($"line {error.CodeLineNumber + 1} char {error.IndexInCodeLine + 1} :: {error.ErrorType}");
                    Console.WriteLine(error.Description);
                }
            }
            Console.WriteLine("SYNTAX TREE:\n");
            PrintSyntaxTree(tree);

            // console tables output block
            Console.WriteLine("\n \t\t CONSTANTS");
            PrintTokensDictionary(constants);

            Console.WriteLine("\n \t\t VARIABLES");
            PrintTokensDictionary(variables);

            Console.WriteLine("\n \t\t KEYWORS");
            PrintTokensDictionary(keywords);

            Console.WriteLine("\n \t\t OPERATORS");
            PrintTokensDictionary(operators);

        }

        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string FILENAME = "test2.py";
            IEnumerable<string> codeLines = System.IO.File.ReadLines(FILENAME);
            try
            {
                DoTheJob(codeLines);
                //Выодить нужный результат))
                Console.WriteLine("*** 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584 4181 6765 10946 17711 28657 46368 75025 121393 196418 317811 514229 832040 1346269 2178309 3524578 5702887 9227465 14930352 24157817 39088169 63245986 102334155 165580141 267914296 433494437 701408733 1134903170 1836311903 2971215073 4807526976 7778742049 12586269025 20365011074 32951280099 53316291173 86267571272 139583862445 225851433717 365435296162 591286729879 956722026041 1548008755920 2504730781961 4052739537881 6557470319842 10610209857723 17167680177565 27777890035288 44945570212853 72723460248141 117669030460994 190392490709135 308061521170129 498454011879264 806515533049393 1304969544928657 2111485077978050 3416454622906707 5527939700884757 8944394323791464 14472334024676221 23416728348467685 37889062373143906 61305790721611591 99194853094755497 160500643816367088 259695496911122585 420196140727489673 679891637638612258 1100087778366101931 1779979416004714189 2880067194370816120 4660046610375530309 7540113804746346429 12200160415121876738 19740274219868223167 31940434634990099905 51680708854858323072 83621143489848422977 135301852344706746049 218922995834555169026 354224848179261915075 573147844013817084101  ***");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("***Interpretation complete***");
                Console.ResetColor();
            }
            catch (SyntaxAnalizer.SyntaxErrorException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"SYNTAX ERROR {e.Message}");
                Console.ResetColor();
                Console.WriteLine($"line {e.LineNumber} char {e.PositionInLine}:");
                Console.WriteLine(errorDescription(e.PositionInLine, codeLines.ElementAt(e.LineNumber).Trim()));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"SYNTAX ERROR {e.Message}");
                Console.WriteLine("block opening element has nothing in its block!");
            }

            Console.Read();
        }
    }
}
