using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Math
{
    public class Formula
    {
        public string Equation { get; private set; }
        
        private Text _currentAnswerObject;
        
        private int _answer;
        private string _input = "";

        public Formula(int points, Text currentAnswerObject)
        {
            _currentAnswerObject = currentAnswerObject;
            _currentAnswerObject.text = "";
            CreateEquation(points);
        }

        public bool IsEnoughDigitsAnswered()
        {
            return _input.Length >= _answer.ToString().Length;
        }

        private void CreateEquation(int points)
        {
            if (points < 20)
            {
                CreateEquationLvl1();
            } else if (points < 40)
            {
                CreateEquationLvl2();
            } else if (points < 60)
            {
                CreateEquationLvl3();
            } else if (points < 80)
            {
                CreateEquationLvl4();
            } else if (points < 100)
            {
                CreateEquationLvl5();
            } else if (points < 120)
            {
                CreateEquationLvl6();
            } else
            {
                CreateEquationLvl7();
            }
        }

        private void CreateEquationLvl1()
        {
            CreatePlusMinusEquation(15);
        }
        
        private void CreateEquationLvl2()
        {
            CreatePlusMinusEquation(20);
        }
        
        private void CreateEquationLvl3()
        {
            if (Random.Range(0, 2) == 1)
            {
                CreatePlusMinusEquation(20);
            }
            else
            {
                CreateTimesEquation(2, 5);
            }
        }
        
        private void CreateEquationLvl4()
        {
            if (Random.Range(0, 2) == 1)
            {
                CreatePlusMinusEquation(20);
            }
            else
            {
                CreateTimesEquation(3, 5);
            }
        }
        
        private void CreateEquationLvl5()
        {
            if (Random.Range(0, 2) == 1)
            {
                CreatePlusMinusEquation(20);
            }
            else
            {
                CreateTimesEquation(4, 5);
            }
        }
        
        private void CreateEquationLvl6()
        {
            if (Random.Range(0, 2) == 1)
            {
                CreatePlusMinusEquation(20);
            }
            else
            {
                CreateTimesEquation(4, 5);
            }
        }
        
        private void CreateEquationLvl7()
        {
            if (Random.Range(0, 2) == 1)
            {
                CreatePlusMinusEquation(30);
            }
            else
            {
                CreateTimesEquation(5, 5);
            }
        }

        private void CreatePlusMinusEquation(int max)
        {
            if (Random.Range(0, 2) == 1)
            {
                var a = Random.Range(0, max + 1);
                var b = Random.Range(0, (max + 1) - a);
                Equation = $"{a} + {b}";
                _answer = a + b;
            }
            else
            {
                var a = Random.Range(0, max + 1);
                var b = Random.Range(0, a + 1);;
                Equation = $"{a} - {b}";
                _answer = a - b;
            }
        }
        
        private void CreatePlusMinusEquation(int firstMin, int firstMax, int secondMin, int secondMax)
        {
            if (Random.Range(0, 2) == 1)
            {
                var a = Random.Range(firstMin, firstMax + 1);
                var b = Random.Range(secondMin, secondMax + 1);
                Equation = $"{a} + {b}";
                _answer = a + b;
            }
            else
            {
                var a = Random.Range(firstMin, firstMax + 1);
                var b = Random.Range(secondMin, secondMax + 1);
                if (a >= b)
                {
                    Equation = $"{a} - {b}";
                    _answer = a - b;                    
                }
                else
                {
                    Equation = $"{b} - {a}";
                    _answer = b - a;     
                }
            }
        }
        
        private void CreateTimesEquation(int table, int max)
        {
            var a = Random.Range(0, max + 1);
            var b = Random.Range(1, (table + 1));
            Equation = $"{a} x {b}";
            _answer = a * b;
        }

        public void AddInput(int input)
        {
            if (input == -1)
            {
                _input = _input.Remove(_input.Length - 1);
            }
            else
            {
                _input += input;
            }

            _currentAnswerObject.text = _input;
        }

        public bool CorrectAnswerGiven()
        {
            return _input == "" + _answer;
        }
    }
}