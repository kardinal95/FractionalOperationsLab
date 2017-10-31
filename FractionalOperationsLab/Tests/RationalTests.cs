using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FractionalOperationsLab.Tests
{
    [TestClass]
    public class RationalUnitTest
    {
        [TestMethod]
        public void Fields_CorrectValuesTest()
        {
            var testRational = new Rational {Denominator = 3, Numerator = 10};
            Assert.AreEqual(testRational.Base, 3, 0, "Целая часть вычисляется некорректно");
            Assert.AreEqual(testRational.Fraction, 1, 0, "Дробная часть вычисляется некорректно");
        }

        [TestMethod]
        public void Negate_CorrectReturnTest()
        {
            var testRational = new Rational {Denominator = 3, Numerator = 10};
            var controlRational = new Rational {Denominator = 3, Numerator = -10};
            Assert.AreEqual(testRational.Negate(), controlRational, null,
                            "Инвертация знака происходит некорректно");
        }

        [TestMethod]
        public void Invert_CorrectReturnTest()
        {
            var testRational = new Rational {Denominator = 3, Numerator = 10};
            var controlRational = new Rational {Denominator = 10, Numerator = 3};
            Assert.AreEqual(testRational.Invert(), controlRational, null,
                            "Обратная дробь находится некорректно");
        }

        [TestMethod]
        public void Even_CorrectReturnTest()
        {
            var testRational = new Rational {Denominator = 18, Numerator = 4};
            var controlRational = new Rational {Denominator = 9, Numerator = 2};
            testRational.Even();
            Assert.AreEqual(testRational, controlRational, null,
                            "Сокращение дроби производится некорректно");
        }

        [TestCategory("Арифметика")]
        [TestMethod]
        public void Add_CorrectReturnTest()
        {
            var testCases = new List<List<Rational>>
            {
                new List<Rational>
                {
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = -1, Denominator = 2},
                    new Rational {Numerator = 0, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = 3, Denominator = 2},
                    new Rational {Numerator = -6, Denominator = 3},
                    new Rational {Numerator = -1, Denominator = 2}
                }
            };
            foreach (var @case in testCases)
            {
                Assert.AreEqual(@case[0].Add(@case[1]), @case[2], null,
                                "Некорректно вычисляется сумма дробей");
            }
        }

        [TestCategory("Арифметика")]
        [TestMethod]
        public void Sub_CorrectReturnTest()
        {
            var testCases = new List<List<Rational>>
            {
                new List<Rational>
                {
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 0, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = 6, Denominator = 1},
                    new Rational {Numerator = 1, Denominator = 3},
                    new Rational {Numerator = 17, Denominator = 3}
                },
                new List<Rational>
                {
                    new Rational {Numerator = -3, Denominator = 2},
                    new Rational {Numerator = -4, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 2}
                }
            };
            foreach (var @case in testCases)
            {
                Assert.AreEqual(@case[0].Add(@case[1].Negate()), @case[2], null,
                                "Некорректно вычисляется разность дробей");
            }
        }

        [TestCategory("Арифметика")]
        [TestMethod]
        public void Mul_CorrectReturnTest()
        {
            var testCases = new List<List<Rational>>
            {
                new List<Rational>
                {
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 4}
                },
                new List<Rational>
                {
                    new Rational {Numerator = 0, Denominator = 1},
                    new Rational {Numerator = 1, Denominator = 3},
                    new Rational {Numerator = 0, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = -1, Denominator = 2},
                    new Rational {Numerator = 4, Denominator = 1},
                    new Rational {Numerator = -2, Denominator = 1}
                }
            };
            foreach (var @case in testCases)
            {
                Assert.AreEqual(@case[0].Multiply(@case[1]), @case[2], null,
                                "Некорректно вычисляется произведение дробей");
            }
        }

        [TestCategory("Арифметика")]
        [TestMethod]
        public void Div_CorrectReturnTest()
        {
            var testCases = new List<List<Rational>>
            {
                new List<Rational>
                {
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 2},
                    new Rational {Numerator = 1, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = 0, Denominator = 1},
                    new Rational {Numerator = 1, Denominator = 3},
                    new Rational {Numerator = 0, Denominator = 1}
                },
                new List<Rational>
                {
                    new Rational {Numerator = -1, Denominator = 2},
                    new Rational {Numerator = 4, Denominator = 1},
                    new Rational {Numerator = -1, Denominator = 8}
                }
            };
            foreach (var @case in testCases)
            {
                Assert.AreEqual(@case[0].DivideBy(@case[1]), @case[2], null,
                                "Некорректно вычисляется произведение дробей");
            }
        }

        [TestMethod]
        public void TryParse_CorrectParseTest()
        {
            var testCases = new Dictionary<string, Rational>
            {
                {"12", new Rational {Denominator = 1, Numerator = 12}},
                {"1:2", new Rational {Denominator = 2, Numerator = 1}},
                {"-2.1:3", new Rational {Denominator = 3, Numerator = -7}}
            };
            foreach (var @case in testCases)
            {
                Rational.TryParse(@case.Key, out var parsed);
                Assert.AreEqual(@case.Value, parsed, null,
                                "Ошибки при переводе дробей из строкового представления");
            }
        }

        [TestMethod]
        public void TryParse_FalseOnErrorsTest()
        {
            var testCases = new List<string> {"1.1", "-1:-2", "asd", "-1::2", "", "2:3.2"};
            foreach (var @case in testCases)
            {
                Assert.AreEqual(Rational.TryParse(@case, out var temp), false, null,
                                "Некорректные представления чисел не выдают ошибку при конвертации");
            }
        }

        [TestMethod]
        public void ToString_CorrectReturnTest()
        {
            var testCases = new Dictionary<string, Rational>
            {
                {"12", new Rational {Denominator = 1, Numerator = 12}},
                {"1:2", new Rational {Denominator = 2, Numerator = 1}},
                {"-2.1:3", new Rational {Denominator = 3, Numerator = -7}}
            };
            foreach (var @case in testCases)
            {
                Assert.AreEqual(@case.Key, @case.Value.ToString(), null,
                                "Ошибки при переводе дробей в строковое представления");
            }
        }
    }
}