using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Token
    {
        public TokenTypes TokenType { get; set; }
        public string Value { get; set; }
        public int CodeLineNumber { get; set; }
        public string Group { get; set; }
        public int Length { get; set; }
        public int CodeLineIndex { get; set; }

        public enum TokenTypes
        {
            CLOSING_SQUARE_BRACKET,
            COLON,
            UNKNOWN,
            OPENING_CURLY_BRACKET,
            MULTIPLICATION,
            COMMENT,
            STRING_CONST,
            OPENING_ROUND_BRACKET,
            FLOAT_NUM,
            MINUS,
            CLOSING_CURLY_BRACKET,
            INT_NUM,
            ID,
            DIVISION,
            DOT,
            COMMA,
            ASSIGN,
            OPENING_SQUARE_BRACKET,
            OR,
            MODULE,
            CLOSING_ROUND_BRACKET,
            PLUS,
            GREATER,
            AND,
            EQUAL,
            NOT,
            GREATER_OR_EQUAL,
            FUNCTION_DEFINITION,
            ELIF,
            BUILT_IN_FUNCTION,
            RAISE,
            IF,
            IMPORT,
            ELSE,
            LOWER,
            LOWER_OR_EQUAL,
            WHILE,
            NOT_EQUAL,
            FOR,
            IN
        }

        public bool IsOperation
        {
            get => SimpleOperators.ContainsValue(TokenType) | BlockOpeningOperators.ContainsValue(TokenType);
        }

        public bool IsReservedIdToken
        {
            get => ReservedIDs.ContainsValue(TokenType);
        }

        public bool IsIf
        {
            get => TokenType == TokenTypes.IF;
        }

        public bool IsBlockOpeningOperation
        {
            get => BlockOpeningOperators.ContainsValue(TokenType);
        }

        public bool IsElse
        {
            get => TokenType == TokenTypes.ELSE;
        }

        public bool IsElif
        {
            get => TokenType == TokenTypes.ELIF;
        }

        public bool IsClosingBracket
        {
            get => this.TokenType == TokenTypes.CLOSING_ROUND_BRACKET;
        }

        public bool IsOpeningBracket
        {
            get => this.TokenType == TokenTypes.OPENING_ROUND_BRACKET;
        }

        public string DescriptionString
        {
            get
            {
                if (this.IsReservedIdToken && !this.IsOperation)
                    return $"Reserved keyword {this.TokenType}";

                if (this.IsOperation)
                    return $"Operation {this.TokenType}";

                if (this.IsConstant)
                    return $"{this.TokenType} constant";

                if (this.TokenType == TokenTypes.COMMENT)
                    return $"is # comment";

                return $"is {this.TokenType}";
            }
        }

        public bool IsConstant
        {
            get => this.TokenType == TokenTypes.INT_NUM
                    || this.TokenType == TokenTypes.STRING_CONST
                    || this.TokenType == TokenTypes.FLOAT_NUM;
        }

        public static Dictionary<string, TokenTypes> BlockOpeningOperators = new Dictionary<string, TokenTypes>()
        {
            ["while"] = TokenTypes.WHILE,
            ["elif"] = TokenTypes.ELIF,
            ["import"] = TokenTypes.IMPORT,
            ["def"] = TokenTypes.FUNCTION_DEFINITION,
            ["else"] = TokenTypes.ELSE,
            ["and"] = TokenTypes.AND,
            ["if"] = TokenTypes.IF,
            ["or"] = TokenTypes.OR,
            ["raise"] = TokenTypes.RAISE,
            ["not"] = TokenTypes.NOT,
            ["for"] = TokenTypes.FOR,
            ["in"] = TokenTypes.IN
        };

        public static Dictionary<string, TokenTypes> SimpleOperators = new Dictionary<string, TokenTypes>()
        {
            ["/"] = TokenTypes.DIVISION,
            ["="] = TokenTypes.ASSIGN,
            [":"] = TokenTypes.COLON,
            ["=="] = TokenTypes.EQUAL,
            ["{"] = TokenTypes.OPENING_CURLY_BRACKET,
            ["%"] = TokenTypes.MODULE,
            [">"] = TokenTypes.GREATER,
            ["+"] = TokenTypes.PLUS,
            ["["] = TokenTypes.OPENING_SQUARE_BRACKET,
            [")"] = TokenTypes.CLOSING_ROUND_BRACKET,
            ["-"] = TokenTypes.MINUS,
            ["!="] = TokenTypes.NOT_EQUAL,
            ["]"] = TokenTypes.CLOSING_SQUARE_BRACKET,
            ["("] = TokenTypes.OPENING_ROUND_BRACKET,
            ["<="] = TokenTypes.LOWER_OR_EQUAL,
            ["*"] = TokenTypes.MULTIPLICATION,
            ["."] = TokenTypes.DOT,
            [","] = TokenTypes.COMMA,
            [">="] = TokenTypes.GREATER_OR_EQUAL,
            ["}"] = TokenTypes.CLOSING_CURLY_BRACKET,
            ["<"] = TokenTypes.LOWER
        };

        public static Dictionary<string, TokenTypes> ReservedIDs = new Dictionary<string, TokenTypes>()
        {
            ["type"] = TokenTypes.BUILT_IN_FUNCTION,
            ["print"] = TokenTypes.BUILT_IN_FUNCTION,
            ["min"] = TokenTypes.BUILT_IN_FUNCTION,
            ["in"] = TokenTypes.IN,
            ["input"] = TokenTypes.BUILT_IN_FUNCTION,
            ["or"] = TokenTypes.OR,
            ["range"] = TokenTypes.BUILT_IN_FUNCTION,
            ["if"] = TokenTypes.IF,
            ["else"] = TokenTypes.ELSE,
            ["import"] = TokenTypes.IMPORT,
            ["abs"] = TokenTypes.BUILT_IN_FUNCTION,
            ["elif"] = TokenTypes.ELIF,
            ["max"] = TokenTypes.BUILT_IN_FUNCTION,
            ["not"] = TokenTypes.NOT,
            ["def"] = TokenTypes.FUNCTION_DEFINITION,
            ["int"] = TokenTypes.BUILT_IN_FUNCTION,
            ["raise"] = TokenTypes.RAISE,
            ["while"] = TokenTypes.WHILE,
            ["float"] = TokenTypes.BUILT_IN_FUNCTION,
            ["and"] = TokenTypes.AND,
            ["for"] = TokenTypes.FOR
        };

        public override string ToString()
        {
            return $"{TokenType}: {Value}";
        }
    }
}
