namespace Combination
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();

        int roundNum = 0;
        int inputInRound = 0;
        int number;
        int correctNum = 0;
        bool gameOver = false;
        PictureBox picInput;
        List<List<PictureBox>> gamePlan;
        List<List<PictureBox>> checkPlan;
        string[] combination = new string[5];

        Dictionary<string, Bitmap> colors = new Dictionary<string, Bitmap>()
        {
            { "1", Properties.Resources.greenBall},
            { "2", Properties.Resources.orangeBall},
            { "3", Properties.Resources.redBall},
            { "4", Properties.Resources.violetBall},
            { "5", Properties.Resources.blueBall},
            { "6", Properties.Resources.galaxyBall},
            { "7", Properties.Resources.whiteBall},
            { "8", Properties.Resources.blackBall}
        };

        public Form1()
        {
            InitializeComponent();
            endGame.Visible = false;

            gamePlan = new List<List<PictureBox>>() {
                                                    new List<PictureBox> {this.box1_1, this.box1_2, this.box1_3, this.box1_4, this.box1_5 },
                                                    new List<PictureBox> {this.box2_1, this.box2_2, this.box2_3, this.box2_4, this.box2_5 },
                                                    new List<PictureBox> {this.box3_1, this.box3_2, this.box3_3, this.box3_4, this.box3_5 },
                                                    new List<PictureBox> {this.box4_1, this.box4_2, this.box4_3, this.box4_4, this.box4_5 },
                                                    new List<PictureBox> {this.box5_1, this.box5_2, this.box5_3, this.box5_4, this.box5_5 },
                                                    new List<PictureBox> {this.box6_1, this.box6_2, this.box6_3, this.box6_4, this.box6_5 },
                                                    new List<PictureBox> {this.box7_1, this.box7_2, this.box7_3, this.box7_4, this.box7_5 },
                                                    new List<PictureBox> {this.box8_1, this.box8_2, this.box8_3, this.box8_4, this.box8_5 },
                                                    new List<PictureBox> {this.box9_1, this.box9_2, this.box9_3, this.box9_4, this.box9_5 },
                                                    new List<PictureBox> {this.box10_1, this.box10_2, this.box10_3, this.box10_4, this.box10_5 },};

            checkPlan = new List<List<PictureBox>>() {
                                                    new List<PictureBox> {this.check1_1, this.check1_2, this.check1_3, this.check1_4, this.check1_5 },
                                                    new List<PictureBox> {this.check2_1, this.check2_2, this.check2_3, this.check2_4, this.check2_5 },
                                                    new List<PictureBox> {this.check3_1, this.check3_2, this.check3_3, this.check3_4, this.check3_5 },
                                                    new List<PictureBox> {this.check4_1, this.check4_2, this.check4_3, this.check4_4, this.check4_5 },
                                                    new List<PictureBox> {this.check5_1, this.check5_2, this.check5_3, this.check5_4, this.check5_5 },
                                                    new List<PictureBox> {this.check6_1, this.check6_2, this.check6_3, this.check6_4, this.check6_5 },
                                                    new List<PictureBox> {this.check7_1, this.check7_2, this.check7_3, this.check7_4, this.check7_5 },
                                                    new List<PictureBox> {this.check8_1, this.check8_2, this.check8_3, this.check8_4, this.check8_5 },
                                                    new List<PictureBox> {this.check9_1, this.check9_2, this.check9_3, this.check9_4, this.check9_5 },
                                                    new List<PictureBox> {this.check10_1, this.check10_2, this.check10_3, this.check10_4, this.check10_5 },};
            MakingCombination();
        }

        private void NewGame(object sender, EventArgs e)
        {
            endGame.Visible = false;
            gameOver = false;
            MakingCombination();
            roundNum = 0;
            inputInRound = 0;
            gamePlan.ForEach(x => x.ForEach(y => y.Image = null));   //deletes the input images from last game
            checkPlan.ForEach(x => x.ForEach(y => y.Image = null));
        }

        public void NewRound()  //new round, new row in game
        {
            roundNum++;
            inputInRound = 0;
            correctNum = 0;
        }

        public void MakingCombination()   // making combination to guess
        {
            int i = 0;
            do
            {
                number = rnd.Next(1, 9);
                if (!combination.Any(x => x == number.ToString())) //checks if the number is already in the combination
                {
                    combination[i] = number.ToString();
                    i++;
                }
            } while (combination.Any(x => string.IsNullOrEmpty(x))); // runs until the array is filled
        }

        private void UserInput(object sender, EventArgs e)  // input of the guess by user
        {
            picInput = sender as PictureBox;

            if (gameOver)
            {
                return;
            }

            if (!gamePlan[roundNum].Any(x => x.Image == colors[picInput.Tag.ToString()]))
            {
                gamePlan[roundNum][inputInRound].Image = colors[picInput.Tag.ToString()];
                gamePlan[roundNum][inputInRound].Tag = picInput.Tag;
                inputInRound++;
            }

            if (inputInRound == 5)
            {
                GuessCheck();
                if (correctNum == 5)
                {
                    GameOver();
                }
                NewRound();
            }

            if (roundNum == 10)
            {

                GameOver();
            }
        }

        private void GuessCheck()
        {
            for (int i = 0; i < inputInRound; i++)
            {
                if (combination[i] == gamePlan[roundNum][i].Tag)
                {
                    checkPlan[roundNum][i].Image = Properties.Resources.yes;
                    correctNum++;
                    continue;
                }

                if (combination.Any(x => x == gamePlan[roundNum][i].Tag))
                {
                    checkPlan[roundNum][i].Image = Properties.Resources.move;
                    continue;
                }
            }
        }

        private void GameOver()
        {
            endGame.Visible = true;
            gameOver = true;

        }

        private void ShowRules(object sender, EventArgs e)
        {
            new Rules().ShowDialog();   // shows rules
        }
    }
}