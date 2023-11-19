using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace MemoryGame
{
    public partial class Form1 : Form
    {
        static public int image_selec = 0;
        public string[] ImagesKeys = new string[4] { "i1", "i2", "i3", "i4" };
        public int[,] SimilerImages = new int[3, 4];
        public List<int> Memory = new List<int>();
        public int image_Trouv = 0;
        public Boolean[,] Just_Cliked = new Boolean[3, 4];
        public List<int> ImagesWeFound = new List<int>();

        public Boolean FirstUseOfHelp = false;



        public void InitialiserGame()
        {
            for (int ligne = 0; ligne < 3; ligne++)
            {
                for (int colonne = 0; colonne < 4; colonne++)
                {
                    Just_Cliked[ligne, colonne] = false;
                }
            }

            Random r = new Random();
            for (int ligne = 0; ligne < 3; ligne++)
            {
                for (int colonne = 0; colonne < 4; colonne++)
                { SimilerImages[ligne, colonne] = 0; }
            }
            //----------------------------------

            for (int k = 1; k <= 4; k++)
            {
                for (int cmpt = 0; cmpt < 3; cmpt++)
                {
                    int l = r.Next(0, 3);
                    int c = r.Next(0, 4);
                    while (SimilerImages[l, c] != 0)
                    {
                        l = r.Next(0, 3);
                        c = r.Next(0, 4);
                    }
                    SimilerImages[l, c] = k;
                }
            }
            //-------------------------------
        }
        public void CheckImages(int NumberOfImage)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (SimilerImages[i, j] == NumberOfImage)
                    {
                        Button button = this.Controls.Find($"IM{i}{j}", true).FirstOrDefault() as Button;
                        button.BackgroundImage = Properties.Resources._checked;
                        button.Enabled = false;
                    }
                }
            }
        }

        public Image getImageOfCase(int ligne, int colonne)
        {
            int imageNumber = SimilerImages[ligne, colonne];
            string imageKey = "i" + imageNumber.ToString();
            Image image = Properties.Resources.ResourceManager.GetObject(imageKey) as Image;
            Memory.Add(imageNumber);
            return image;
        }
        public void UpdateMemory()
        {
            Memory[0] = Memory[1];
            Memory[1] = Memory[2];
            Memory.RemoveAt(2);
        }

        public Boolean wellMemorized()
        {
            if (Memory.Count == 3)
            {

                if (Memory[0] == Memory[1] && Memory[1] == Memory[2])
                {

                    image_Trouv++;
                    ImageTrouver.Text = "Images found: " + image_Trouv.ToString();
                    CheckImages(Memory[2]);
                    ImagesWeFound.Add(Memory[2]);
                    this.BackColor = Color.Lime;
                    UpdateMemory();
                    return true;
                }
                UpdateMemory();
                return false;
            }
            return false;
        }

        public int NumberRightSelect()
        {
            if (Memory.Count == 0) return 0;
            if (Memory.Count == 1) return 1;
            if (Memory.Count == 2)
            {
                if (Memory[0] == Memory[1]) return 2;
                else return 0;
            }
            if (Memory.Count == 3)
            {
                if (Memory[0] == Memory[1] && Memory[1] == Memory[2]) return 3;
                else if (Memory[1] == Memory[2]) return 2;
                else return 0;
            }
            return -1;
        }

        public void ButtonEffect(int ligne, int colonne)
        {
            attention.Text = "";

            if (Just_Cliked[ligne, colonne] == false)
            {
                Just_Cliked[ligne, colonne] = true;
                this.BackColor = Color.DarkSlateGray;
                PIC.Image = getImageOfCase(ligne, colonne);
                image_selec++;
                //image_selec = image_selec % 4;

                IMselec.Text = "Number of tried : " + image_selec.ToString();
                CorrSelec.Text = "Correct selections : " + NumberRightSelect().ToString();
                wellMemorized();
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i != ligne && j != colonne) Just_Cliked[i, j] = false;
                }
            }

        }
        public Form1()
        {
            InitializeComponent();
            InitialiserGame();
        }

        private void IM00_Click(object sender, EventArgs e)
        {
            ButtonEffect(0, 0);
        }

        private void IM01_Click(object sender, EventArgs e)
        {
            ButtonEffect(0, 1);
        }

        private void IM02_Click(object sender, EventArgs e)
        {
            ButtonEffect(0, 2);

        }

        private void IM03_Click(object sender, EventArgs e)
        {
            ButtonEffect(0, 3);
        }

        private void IM10_Click(object sender, EventArgs e)
        {
            ButtonEffect(1, 0);
        }

        private void IM11_Click(object sender, EventArgs e)
        {
            ButtonEffect(1, 1);
        }

        private void IM12_Click(object sender, EventArgs e)
        {
            ButtonEffect(1, 2);
        }

        private void IM13_Click(object sender, EventArgs e)
        {
            ButtonEffect(1, 3);
        }

        private void IM20_Click(object sender, EventArgs e)
        {
            ButtonEffect(2, 0);
        }

        private void IM21_Click(object sender, EventArgs e)
        {
            ButtonEffect(2, 1);
        }

        private void IM22_Click(object sender, EventArgs e)
        {
            ButtonEffect(2, 2);
        }

        private void IM23_Click(object sender, EventArgs e)
        {
            ButtonEffect(2, 3);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitialiserGame();
            this.BackColor = Color.DarkSlateGray;
            Memory.Clear();
            image_selec = 0;
            image_Trouv = 0;
            IMselec.Text = "Number of tried : " + image_selec.ToString();
            ImageTrouver.Text = "Images found: " + image_Trouv.ToString();
            attention.Text = "";
            FirstUseOfHelp = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    Button button = this.Controls.Find($"IM{i}{j}", true).FirstOrDefault() as Button;
                    button.BackgroundImage = null;
                    button.Enabled = true;


                }
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> t = new List<int>() { 1, 2, 3, 4 };
            if (ImagesWeFound.Count == 4) button2.Enabled = false;
            else
            {
                if (FirstUseOfHelp == false)
                {
                    FirstUseOfHelp = true;
                    for (int i = 0; i < ImagesWeFound.Count; i++)
                    {
                        if (ImagesWeFound[i] == 1) t.Remove(1);
                        if (ImagesWeFound[i] == 2) t.Remove(2);
                        if (ImagesWeFound[i] == 3) t.Remove(3);
                        if (ImagesWeFound[i] == 4) t.Remove(4);

                    }
                    image_Trouv++;
                    ImageTrouver.Text = "Images found: " + image_Trouv.ToString();
                    CheckImages(t[0]);
                }
                else
                {
                    attention.Text = "You can use Help one time in the game !";
                }
            }

        }

        private void PIC_Click(object sender, EventArgs e)
        {

        }
    }
}