﻿using System;

namespace Day08
{
    public static class BooleanExtension
    {
        public static bool Operator(this string logic, int x, int y)
        {
            switch (logic)
            {
                case ">":  return x > y;
                case ">=": return x >= y;
                case "<":  return x < y;
                case "<=": return x <= y;
                case "==": return x == y;
                case "!=": return x != y;
                default: throw new Exception("invalid logic");
            }
        }
    }
}