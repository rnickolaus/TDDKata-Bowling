using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Bowling.Test
{
    [TestFixture]
    public partial class GameTest
    {
        #region Helpers

        //  Provide a list of classes to test that implement the IGame interface
        private static IEnumerable GetAllTestCases()
        {
            yield return new TestCaseData(new Dev01.Game()).SetName("Dev01");
            yield return new TestCaseData(new Dev02.Game()).SetName("Dev02");
            yield return new TestCaseData(new Dev03.Game()).SetName("Dev03");
            yield return new TestCaseData(new Dev04.Game()).SetName("Dev04");
            yield return new TestCaseData(new Dev05.Game()).SetName("Dev05");
            yield return new TestCaseData(new Dev06.Game()).SetName("Dev06");
            yield return new TestCaseData(new Dev07.Game()).SetName("Dev07");
            yield return new TestCaseData(new Dev08.Game()).SetName("Dev08");
            yield return new TestCaseData(new Dev09.Game()).SetName("Dev09");
            yield return new TestCaseData(new Dev10.Game()).SetName("Dev10");
		}

        /// <summary>
        /// Given a list of pPinsPerThrow to knock down per roll and a game container, roll the game and calculate the score.
        /// </summary>
        /// <param name="pGame">game container (IGame)</param>
        /// <param name="pPinsPerThrow"># pins knocked down per ball (collection)</param>
        /// <returns>Overall game score</returns>
        private static int BowlAGame(IGame pGame, IEnumerable<int> pPinsPerThrow)
        {
            foreach (var pinThrow in pPinsPerThrow)
                pGame.ThrowABall(pinThrow);
            return pGame.TallyScore();
        }

        #endregion Helpers

        #region No Bonus Tests

        [Test]
        [Description("Exercise 2")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0
        // Score  0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0    0   0
        public void NoBonus_Roll20Gutterballs_ZeroScore(IGame game)
        {
            var pinsPerThrow = new List<int>() { 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0, 0,0 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(0, score);
        }

        [Test]
        [Description("Exercise 3")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1
        // Score  1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        public void NoBonus_Roll20Singles_20Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(20, score);
        }

        #endregion No Bonus Tests

        #region Bonus Tests

        #region Spare Tests

        [Test]
        [Description("Exercise 4")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   5   5    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1
        // Score  5  11   12  13   14  15   16  17   18  19   20  21   22  23   24  25   26  27   28  29
        public void Spare_RollASpareFirstFrameThenSingles_29Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 5,5, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(29, score);
        }

        [Test]
        [Description("Exercise 5")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    5   5
        // Score  1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   23  29
        public void Spare_RollSinglesThenASpareTenthFrame_29Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 5,5 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(29, score);
        }

        [Test]
        [Description("Exercise 6")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   5   5    5   5    5   5    5   5    5   5    5   5    5   5    5   5    5   5    5   5
        // Score  5  15   20  30   35  45   50  60   65  75   80  90   95 105  110 120  125 135  140 150
        public void Spare_Roll5PinsEachBall_150Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5, 5,5 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(150, score);
        }

        #endregion Spare Tests

        #region Strike Tests

        [Test]
        [Description("Exercise 7")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1        2   3    4   5    6   7    8   9   10  11   12  13   14  15   16  17   18  19
        // Pins  10   -    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1
        // Score 12   -   13  14   15  16   17  18   19  20   21  22   23  24   25  26   27  28   29  30
        public void Strike_RollAStrikeFirstFrameThenSingles_30Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 10, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(30, score);
        }

        [Test]
        [Description("Exercise 8")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19
        // Pins   1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1   10
        // Score  1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   30  
        public void Strike_RollSinglesThenAStrikeTenthFrame_30Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(30, score);
        }

        [Test]
        [Description("Exercise 9")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9        10
        // Ball   1        2        3        4        5        6        7        8        9        10
        // Pins  10   -   10   -   10   -   10   -   10   -   10   -   10   -   10   -   10   -    10
        // Score 30       60       90      120      150      180      210      240      270       300
        public void Strike_Roll10Strikes_300Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(300, score);
        }

        #endregion Strike Tests

        #endregion Bonus Tests

        #region Misc Bowl Tests

        [Test]
        [Description("Exercise 10")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   2   2    4   4    6   4    8   2   10   -   10   -    8   2    6   4    4   4    2   2
        // Score  2   4    8  12   18  30   38  50   78       98      106 114  120 128  132 136  138 140
        public void Misc_RollStepUpDownPattern_140Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 2,2, 4,4, 6,4, 8,2, 10, 10, 8,2, 6,4, 4,4, 2,2 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(140, score);
        }

        [Test]
        [Description("Exercise 11")]
        [TestCaseSource("GetAllTestCases")]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins  10        8   2    6   4    4   4    2   2    2   2    4   4    6   4    8   2   10
        // Score 20       28  36   42  50   54  58   60  62   64  66   70  74   80  92  100  112 140
        public void Misc_RollStepDownUpPattern_Score(IGame game)
        {
            var pinsPerThrow = new List<int>() { 10, 8,2, 6,4, 4,4, 2,2, 2,2, 4,4, 6,4, 8,2, 10 };
            var score = BowlAGame(game, pinsPerThrow);
            Assert.AreEqual(140, score);
        }

        #endregion Misc Bowl Tests

        #region Exception Tests

        [Test]
        [Description("Exercise 12")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectBallsException))]
        // Frame  1        2        3        4        5        6        7        8        9        10
        // Ball   1        2        3        4        5        6        7        8        9        10
        // Pins  11
        // Score Err
        public void Error_TryToThrow11PinsIn1Ball_ExpectBallsException(IGame game)
        {
            var pinsPerThrow = new List<int>() { 11 };
            BowlAGame(game, pinsPerThrow);
        }

        [Test]
        [Description("Exercise 13")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectBallsException))]
        // Frame  1        2        3        4        5        6        7        8        9        10
        // Ball   1        2        3        4        5        6        7        8        9        10
        // Pins   5   6
        // Score  5 Err
        public void Error_TryToThrow11PinsIn2Balls_ExpectBallsException(IGame game)
        {
            var pinsPerThrow = new List<int>() { 5, 6 };
            BowlAGame(game, pinsPerThrow);
        }

        [Test]
        [Description("Exercise 14")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectBallsException))]
        // Frame  1        2        3        4        5        6        7        8        9        10
        // Ball   1        2        3        4        5        6        7        8        9        10
        // Pins  -1
        // Score Err
        public void Error_TryToThrowNegativePins_ExpectBallsException(IGame game)
        {
            var pinsPerThrow = new List<int>() { -1 };
            BowlAGame(game, pinsPerThrow);
        }

        [Test]
        [Description("Exercise 15")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectGameException))]
        // Frame  1        2        3        4        5        6        7        8        9       10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20   21
        // Pins   1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1
        // Score  1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20  Err
        public void Error_TryToThrow21Singles_ExpectGameException(IGame game)
        {
            var pinsPerThrow = new List<int>() { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1 };
            BowlAGame(game, pinsPerThrow);
        }

        [Test]
        [Description("Exercise 16")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectGameException))]
        // Frame  1        2        3        4        5        6        7        8        9        10
        // Ball   1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  20
        // Pins   1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   1    1   ?
        // Score  1   2    3   4    5   6    7   8    9  10   11  12   13  14   15  16   17  18   19  ???
        public void Error_TryToThrowTooFewBallsAndTallyScore_ExpectGameException(IGame game)
        {
            var pinsPerThrow = new List<int>() { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1 };
            BowlAGame(game, pinsPerThrow);
        }

        [Test]
        [Description("Exercise 17")]
        [TestCaseSource("GetAllTestCases")]
        [ExpectedException(typeof(IncorrectGameException))]
        // Frame  1        2        3        4        5        6        7        8        9        10         ?
        // Ball   1        2        3        4        5        6        7        8        9        10        11
        // Pins  10   -   10   -   10   -   10   -   10   -   10   -   10   -   10   -   10   -    10        10
        // Score 30       60       90      120      150      180      210      240      270       300       Err
        public void Error_TryToThrow11Strikes_ExpectGameException(IGame game)
        {
            var pinsPerThrow = new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
            BowlAGame(game, pinsPerThrow);
        }

        #endregion Exception Tests

    }
}
