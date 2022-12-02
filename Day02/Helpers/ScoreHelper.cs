namespace Day02.Helpers
{
    internal static class ScoreHelper
    {
        // Since both the move and the character to decide the outcome either A,B or C, use these ugly constants.
        const char RockOrLoss = 'A';
        const char PaperOrDraw = 'B';
        const char ScissorsOrWin = 'C';

        const int win = 6;
        const int draw = 3;
        const int loss = 0;

        internal static int GetGameScore(char[] game, bool decideOutcome = false)
        {
            char enemyMove = game[0];
            char myMove = game[1];

            int score = 0;

            if (decideOutcome)
            {
                myMove = DecideMove(myMove, enemyMove);
            }

            score += GetMoveScore(myMove);

            if (myMove == enemyMove)
            {
                return score += draw;
            }

            score += RegularGame(myMove, enemyMove);

            return score;
        }

        private static int RegularGame(char myMove, char enemyMove)
        {
            switch (myMove)
            {
                case RockOrLoss:
                    return enemyMove == ScissorsOrWin ? win : loss;
                case PaperOrDraw:
                    return enemyMove == RockOrLoss ? win : loss;
                case ScissorsOrWin:
                    return enemyMove == PaperOrDraw ? win : loss;
                default:
                    return myMove;
            }
        }

        private static char DecideMove(char myMove, char enemyMove)
        {
            switch (myMove)
            {
                case RockOrLoss:
                    return ChangeMove(enemyMove, false);
                case PaperOrDraw:
                    return enemyMove;
                case ScissorsOrWin:
                    return ChangeMove(enemyMove, true);
                default:
                    return myMove;
            }
        }

        private static int GetMoveScore(char myMove)
        {
            switch (myMove)
            {
                case RockOrLoss:
                    return 1;
                case PaperOrDraw:
                    return 2;
                case ScissorsOrWin:
                    return 3;
                default:
                    return 0;
            }
        }

        private static char ChangeMove(char enemyMove, bool shouldWin)
        {
            switch (enemyMove)
            {
                case RockOrLoss:
                    return shouldWin ? PaperOrDraw : ScissorsOrWin;
                case PaperOrDraw:
                    return shouldWin ? ScissorsOrWin : RockOrLoss;
                case ScissorsOrWin:
                    return shouldWin ? RockOrLoss : PaperOrDraw;
                default:
                    return enemyMove;
            }
        }
    }
}
