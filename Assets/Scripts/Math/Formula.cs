
using System;

namespace Math
{
    public class Formula
    {
        public string Equation { get; private set; }
        
        private readonly Random _rand = new Random();
        
        private int _answer;
        private string _input = "";

        public Formula()
        {
            CreateEquationLvl1();
        }

        private void CreateEquationLvl1()
        {
            var a = _rand.Next(15);
            var b = _rand.Next(15 - a);
            Equation = $"{a} + {b}";
            _answer = a + b;
        }

        public void AddInput(int input)
        {
            _input += input;
        }

        public bool CorrectAnswerGiven()
        {
            return _input.EndsWith("" + _answer);
        }
    }
}