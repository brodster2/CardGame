using System;
using System.Resources;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace CardGame
{
    public partial class Form1 : Form
    {
        public int turns = 0;                 //this is incremented by Card.flip()
        public int cardsFlipped = 0;
        private int pairsFound = 0;           //this is incremented by Form.compare()
        public Card firstCard, secondCard;
        private static System.Timers.Timer flipTimer = new System.Timers.Timer(2000);
        Card[] deck = new Card[52];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            createCards();
            deal();
        }


        private void createCards()
        {

            deck[0] = new Card(Properties.Resources.ace_of_clubs , 1);
            deck[1] = new Card(Properties.Resources.ace_of_diamonds , 1);
            deck[2] = new Card(Properties.Resources.ace_of_hearts , 1);
            deck[3] = new Card(Properties.Resources.ace_of_spades2 , 1);
            deck[4] = new Card(Properties.Resources._2_of_clubs , 2);
            deck[5] = new Card(Properties.Resources._2_of_diamonds , 2);
            deck[6] = new Card(Properties.Resources._2_of_hearts , 2);
            deck[7] = new Card(Properties.Resources._2_of_spades , 2);
            deck[8] = new Card(Properties.Resources._3_of_clubs , 3);
            deck[9] = new Card(Properties.Resources._3_of_diamonds , 3);
            deck[10] = new Card(Properties.Resources._3_of_hearts , 3);
            deck[11] = new Card(Properties.Resources._3_of_spades , 3);
            deck[12] = new Card(Properties.Resources._4_of_clubs , 4);
            deck[13] = new Card(Properties.Resources._4_of_diamonds , 4);
            deck[14] = new Card(Properties.Resources._4_of_hearts , 4);
            deck[15] = new Card(Properties.Resources._4_of_spades , 4);
            deck[16] = new Card(Properties.Resources._5_of_clubs , 5);
            deck[17] = new Card(Properties.Resources._5_of_diamonds , 5);
            deck[18] = new Card(Properties.Resources._5_of_hearts , 5);
            deck[19] = new Card(Properties.Resources._5_of_spades , 5);
            deck[20] = new Card(Properties.Resources._6_of_clubs , 6);
            deck[21] = new Card(Properties.Resources._6_of_diamonds , 6);
            deck[22] = new Card(Properties.Resources._6_of_hearts , 6);
            deck[23] = new Card(Properties.Resources._6_of_spades , 6);
            deck[24] = new Card(Properties.Resources._7_of_clubs , 7);
            deck[25] = new Card(Properties.Resources._7_of_diamonds , 7);
            deck[26] = new Card(Properties.Resources._7_of_hearts , 7);
            deck[27] = new Card(Properties.Resources._7_of_spades , 7);
            deck[28] = new Card(Properties.Resources._8_of_clubs , 8);
            deck[29] = new Card(Properties.Resources._8_of_diamonds , 8);
            deck[30] = new Card(Properties.Resources._8_of_hearts , 8);
            deck[31] = new Card(Properties.Resources._8_of_spades , 8);
            deck[32] = new Card(Properties.Resources._9_of_clubs , 9);
            deck[33] = new Card(Properties.Resources._9_of_diamonds , 9);
            deck[34] = new Card(Properties.Resources._9_of_hearts , 9);
            deck[35] = new Card(Properties.Resources._9_of_spades , 9);
            deck[36] = new Card(Properties.Resources._10_of_clubs , 10);
            deck[37] = new Card(Properties.Resources._10_of_diamonds , 10);
            deck[38] = new Card(Properties.Resources._10_of_hearts , 10);
            deck[39] = new Card(Properties.Resources._10_of_spades , 10);
            deck[40] = new Card(Properties.Resources.jack_of_clubs2 , 11);
            deck[41] = new Card(Properties.Resources.jack_of_diamonds2 , 11);
            deck[42] = new Card(Properties.Resources.jack_of_hearts2 , 11);
            deck[43] = new Card(Properties.Resources.jack_of_spades2 , 11);
            deck[44] = new Card(Properties.Resources.queen_of_clubs2 , 12);
            deck[45] = new Card(Properties.Resources.queen_of_diamonds2 , 12);
            deck[46] = new Card(Properties.Resources.queen_of_hearts2 , 12);
            deck[47] = new Card(Properties.Resources.queen_of_spades2 , 12);
            deck[48] = new Card(Properties.Resources.king_of_clubs2 , 13);
            deck[49] = new Card(Properties.Resources.king_of_diamonds2 , 13);
            deck[50] = new Card(Properties.Resources.king_of_hearts2 , 13);
            deck[51] = new Card(Properties.Resources.king_of_spades2 , 13);
            shuffle();
        }

        private void shuffle()
        {
            Random dealer = new Random();
            Card placeholder;
            int shuffles = 0;

            while (shuffles < 100) {
                int r1 = dealer.Next(0, 52);
                int r2 = dealer.Next(0, 52);
                placeholder = deck[r1];
                deck[r1] = deck[r2];
                deck[r2] = placeholder;

                shuffles++;
            }
        }

        private void deal()
        { 

            int count = 0;
            for (int x = 0; x < 13; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    deck[count].Parent = this;
                    deck[count].Left = x * 100 + 50;
                    deck[count].Top = y * 125 + 100;
                    Controls.Add(deck[count]);
                    count++;
                }
            } 
        }

        public void compare(Card a, Card b)   //Called by Card.flip()
        {
            flipTimer.Elapsed += timeUp;
            flipTimer.Enabled = false;

            if (a.Value == b.Value)
            {
                cardsFlipped = 0;
                pairsFound++;
                checkWin();
            }
            else               
            {
                flipTimer.Enabled = true;
            }
        }

        public void checkWin()  //Called by Form.compare()
        {
            if (pairsFound == 26)
            {
                MessageBox.Show("Congratulations you won",
                    "Your score is " + turns.ToString(),
                    MessageBoxButtons.OK);
            }
        }


        private void timeUp(Object source, System.Timers.ElapsedEventArgs e)
        {
            firstCard.flipback();
            secondCard.flipback();
            flipTimer.Enabled = false;
        }
    }

    public class Card : PictureBox
    {
        public Form1 Parent; //Refers to PictureBox.Parent (deck[])
        public int Value;
        public Image Face;
        public bool flipped;


        public Card(Image face, int value)
        {
            this.Width = 75;
            this.Height = 100;
            this.Left = 0;
            this.Top = 0;
            this.Image = Properties.Resources.zblue_back;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Face = face;
            this.Value = value;
            this.flipped = false;
            this.Click += new System.EventHandler(flip);
        }

        public void flip(object sender, EventArgs e)
        {
            var p = Parent;
            if (!flipped)
            {

                if (p.cardsFlipped == 0)
                {
                    this.Image = this.Face;
                    p.firstCard = this;
                    flipped = true;
                    p.cardsFlipped++;
                }
                else //if (p.cardsFlipped < 2)
                {
                    this.Image = this.Face;
                    p.secondCard = this;
                    flipped = true;
                    p.turns++;
                    p.compare(p.firstCard, p.secondCard);
                }
            }
        }

        public void flipback()
        {

            this.Image = global::CardGame.Properties.Resources.zblue_back;
            flipped = false;
            Parent.cardsFlipped = 0;
        }
    }
}

