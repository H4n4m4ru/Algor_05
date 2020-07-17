using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;



namespace Algor_05
{
    public partial class Form1 : Form
    {
        const int width = 17;//繪圖的固定寬度
        bool CanChangeDirection;//方向鍵鎖
        bool PauseLock;//空白鍵鎖
        bool[][] exist = null;//檢查是否存在用
        int speed;//當前的速度
        string direction;//方向
        int Eating;//正在消化的份量
        int Level = 0;//關卡等級
        int value;//竜的長度
        int life = 3;//生命值
        int score;//分數
        bool Opening;//press pause to start
        Bitmap[] levelPics = { Algor_05.Properties.Resources.level_1, Algor_05.Properties.Resources.level_2, Algor_05.Properties.Resources.level_3, Algor_05.Properties.Resources.level_4, Algor_05.Properties.Resources.level_5, Algor_05.Properties.Resources.level_6, Algor_05.Properties.Resources.level_7, Algor_05.Properties.Resources.level_8, Algor_05.Properties.Resources.level_9, Algor_05.Properties.Resources.level_10, Algor_05.Properties.Resources.level_11, Algor_05.Properties.Resources.level_12, Algor_05.Properties.Resources.level_13 };
        int[] LengthToNextLevel = { 5, 10, 10, 15, 15, 10, 10, 15, 15, 10, 10, 15, 15 };
        
        string[] Maps ={ "map1.txt", "map2.txt", "map3.txt", "map4.txt", "map5.txt", "map6.txt", "map7.txt", "map8.txt", "map9.txt", "map10.txt", "map11.txt", "map12.txt", "map13.txt" };
        //地圖檔名稱陣列
        PictureBox[] Lifes=new PictureBox[3];//生命值圖示
        Queue<Point> Ryuu = new Queue<Point>();//佇列,紀錄竜的位置
        Widow widow = new Widow();//食物物件

        Graphics hanamaru = null;//form1的繪圖物件
        SolidBrush bush = new SolidBrush(Color.FromArgb(255,43,124,177));//竜的顏色
        SolidBrush eraser = new SolidBrush(Color.FromArgb(255,63,52,59));//背景色
        SolidBrush Hid = new SolidBrush(Color.FromArgb(255,255,184,1));//竜頭色
        SolidBrush Wall = new SolidBrush(Color.FromArgb(255, 168, 104, 108));//牆壁色
        Form3 winWindow = new Form3();
        Help HelpWindow = new Help();
        Random RandomNumberProvidor = new Random();//亂數產生器
        SoundPlayer BGM ;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            HelpWindow.ShowDialog();
            Lifes[0] = lifePoint_01;
            Lifes[1] = lifePoint_02;
            Lifes[2] = lifePoint_03;
            BGM = new SoundPlayer("BraveNewWorld.wav");
            BGM.PlayLooping();
            hanamaru = this.CreateGraphics();//建構畫布

            timer1.Enabled = false;
            for (int i = 0; i < 3; i++) Lifes[i].Visible = false;
            ShowLevel.Visible = false;
            ShowScore.Visible = false;//關閉右側遊戲資訊的顯示
            Opening = true;
            PauseLock = false;
            CanChangeDirection = false;

            this.Controls.Add(widow);//加入食物至表單
        
        } //End
        public void ReLoadLevel()
        {   //t重新載入目前等級之關卡

            timer1.Enabled = false;
            timer2.Enabled = false;//先暫停

            pictureBox1.BackgroundImage = levelPics[Level];
            pictureBox1.Visible = true;//顯示該關卡的圖片

            for (int i = 0; i < 3; i++) Lifes[i].Visible = false;
            ShowLevel.Visible = false;
            ShowScore.Visible = false;//關閉右側遊戲資訊的顯示

            CanChangeDirection = false;//在過場畫面結束前無法操作方向
            PauseLock = false;//在過場畫面結束前無法讓竜動作

            exist = null;//清除檢查用陣列
            speed = 41;//初始化速度(這邊可能要隨level做初始化)
            timer1.Interval = speed;
            direction = "Right";//初始化方向
            Eating = 0;//初始化消化量
            value = 1;//重置竜的長度

            //初始化食物數據,Ex:Score,value,speedDelta...

            Ryuu = null;
            Ryuu = new Queue<Point>();//初始化竜
            Ryuu.Enqueue(new Point(476, 255));//推入竜頭
            
            exist = new bool[36][];
            for (int i = 0; i < 36; i++) exist[i] = new bool[59];//重構檢查用陣列
            exist[15][28] = true;//紀錄

            ReadMap(Level);//讀取地圖

            WidowsAssRnD();//隨機改變食物位置

            ShowLevel.Text = "Level : " + (Level+1);
            ShowScore.Text = "Score : " + score;

            timer2.Enabled = true;
            timer2.Start();//過場開始

        }//End
        public void ReadMap(int index)
        {
            StreamReader Map = null;
            Map = new StreamReader(Maps[index]); //index=要讀的地圖編號
            string temp = null;//暫存用

            for (int i = 0; i < 36; i++)
            {
                temp = Map.ReadLine();//取得第i行地圖數據

                for (int j = 0; j < 59; j++)
                {
                    if (temp[j] == '1')
                    {
                        //是牆壁
                        exist[i][j] = true;//登錄牆壁,登錄完才能執行WidowAssRnd             
                        hanamaru.FillRectangle(Wall, j * 17, i * 17, width, width);//繪製牆壁
                    }
                }
            }
        }//End
        private void Form1_KeyDown(object sender, KeyEventArgs e){
            //改變竜頭的方向用
            if ((e.KeyCode == Keys.Space) && Opening){ 
                //press pause to start
                Level = 0;
                score = 0;
                life = 3;
                ReLoadLevel();
                Opening = false;
                return;
            }
            if ((e.KeyCode == Keys.Space)&&PauseLock){
                if (timer1.Enabled == true){
                    timer1.Enabled = false;
                }
                else{
                    pictureBox1.Visible = false;
                    timer1.Enabled = true;
                }
            }

            if (CanChangeDirection){
                if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && (direction != "Down")) direction = "Up";
                else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && (direction != "Up")) direction = "Down";
                else if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.A) && (direction != "Right")) direction = "Left";
                else if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.D) && (direction != "Left")) direction = "Right";
                CanChangeDirection = false;//這次改變完方向,至少要等待一次移動才能再次改變方向
            }
        } //End
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (direction) {
                case "Left": Ryuu.Enqueue(new Point(Ryuu.ElementAt(Ryuu.Count - 1).X - 17, Ryuu.ElementAt(Ryuu.Count - 1).Y)); break;
                case "Right": Ryuu.Enqueue(new Point(Ryuu.ElementAt(Ryuu.Count - 1).X + 17, Ryuu.ElementAt(Ryuu.Count - 1).Y)); break;
                case "Up": Ryuu.Enqueue(new Point(Ryuu.ElementAt(Ryuu.Count - 1).X, Ryuu.ElementAt(Ryuu.Count - 1).Y - 17)); break;
                case "Down": Ryuu.Enqueue(new Point(Ryuu.ElementAt(Ryuu.Count - 1).X, Ryuu.ElementAt(Ryuu.Count - 1).Y + 17)); break;
            }
            //根據方向推入新的竜頭
            //  1/3:Queue紀錄
            
            try {
                if (exist[Ryuu.ElementAt(Ryuu.Count - 1).Y / 17][Ryuu.ElementAt(Ryuu.Count - 1).X / 17]){
                    //碰到自己或牆壁
                    life--;
                    Lifes[life].Visible = false;//減少生命值
                    if (life == 0){
                        //遊戲結束,初始化遊戲設定
                        timer1.Enabled = false;
                        BGM.Stop();
                        pictureBox1.BackgroundImage = Algor_05.Properties.Resources.presss;
                        pictureBox1.Visible = true;
                        winWindow.Scott = score;
                        PauseLock = false;
                        CanChangeDirection = false;
                        winWindow.ShowDialog();//登錄過關玩家姓名
                        for (int i = 0; i < 3; i++) Lifes[i].Visible = false;
                        ShowLevel.Visible = false;
                        ShowScore.Visible = false;//關閉右側遊戲資訊的顯示
                        Opening = true;
                        BGM = null;
                        BGM = new SoundPlayer("BraveNewWorld.wav");
                        BGM.PlayLooping();
                        return;
                    }
                    ReLoadLevel();
                    return;
                }
            }
            catch {
                //越界,處理同撞牆與自己,只是例外狀況要獨立列出而已
                life--;
                Lifes[life].Visible = false;
                if (life == 0){
                    //遊戲結束,初始化遊戲設定
                    timer1.Enabled = false;
                    BGM.Stop();
                    pictureBox1.BackgroundImage = Algor_05.Properties.Resources.presss;
                    pictureBox1.Visible = true;
                    winWindow.Scott = score;
                    PauseLock = false;
                    CanChangeDirection = false;
                    winWindow.ShowDialog();//登錄過關玩家姓名
                    for (int i = 0; i < 3; i++) Lifes[i].Visible = false;
                    ShowLevel.Visible = false;
                    ShowScore.Visible = false;//關閉右側遊戲資訊的顯示
                    Opening = true;
                    BGM = null;
                    BGM = new SoundPlayer("BraveNewWorld.wav");
                    BGM.PlayLooping();
                    return;
                }
                ReLoadLevel();
                return;
            }//檢查是否合法移動



            //================================================ 確認是合法移動 ======================================================================
            hanamaru.FillRectangle(bush, Ryuu.ElementAt(Ryuu.Count - 2).X, Ryuu.ElementAt(Ryuu.Count - 2).Y, width, width);//先把前竜頭的顏色改掉
            hanamaru.FillRectangle(Hid, Ryuu.ElementAt(Ryuu.Count - 1).X, Ryuu.ElementAt(Ryuu.Count - 1).Y, width, width);
            //  2/3,圖面紀錄
            exist[Ryuu.ElementAt(Ryuu.Count - 1).Y / 17][Ryuu.ElementAt(Ryuu.Count - 1).X / 17] = true;
            //3/3,陣列紀錄

            //開始檢查有沒有吃到食物
            if ((Ryuu.ElementAt(Ryuu.Count - 1).X == widow.Location.X) && (Ryuu.ElementAt(Ryuu.Count - 1).Y == widow.Location.Y)){
                //吃到食物,進行相關處理   
                speed += widow.speedDelta;
                timer1.Interval = speed;//速度改變
                value += widow.value;//竜的長度變長
                score += widow.Score;
                ShowScore.Text = "Score : " + score;

                if (value >= LengthToNextLevel[Level]){
                    Level++;
                    if (Level == 13){
                        //過關
                        timer1.Enabled = false;
                        BGM.Stop();
                        pictureBox1.BackgroundImage = Algor_05.Properties.Resources.presss;
                        pictureBox1.Visible = true;
                        score += life * 2000;
                        ShowScore.Text = "Score : " + score;
                        winWindow.Scott = score;
                        PauseLock = false;
                        CanChangeDirection = false;
                        winWindow.ShowDialog();//登錄過關玩家姓名
                        for (int i = 0; i < 3; i++) Lifes[i].Visible = false;
                        ShowLevel.Visible = false;
                        ShowScore.Visible = false;//關閉右側遊戲資訊的顯示
                        Opening = true;
                        BGM = null;
                        BGM = new SoundPlayer("BraveNewWorld.wav");
                        BGM.PlayLooping();
                        return;
                    }
                    else{
                        ReLoadLevel(); //換關
                        return;
                    }
                }
                Eating += widow.value;//消化數量增加
                WidowsAssRnD();//改變位置
            }

            if (Eating > 0) { 
                //正在消化,即不用對竜尾做消除
                Eating--;
            }
            else { 
                //沒吃到東西
                exist[Ryuu.ElementAt(0).Y / 17][Ryuu.ElementAt(0).X / 17] = false;
                //  1/3取消陣列紀錄
                hanamaru.FillRectangle(eraser, Ryuu.ElementAt(0).X, Ryuu.ElementAt(0).Y, width, width);
                //  2/3取消圖面紀錄
                Ryuu.Dequeue();
                //  3/3取消Queue紀錄
            }

            CanChangeDirection = true;//已移動至少一格,現在可以改變方向
        
        } //End

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //重繪事件,若不重繪以下資料,畫面會只剩下屬於物件的果實
            if (!Opening){
                for (int i = 0; i < Ryuu.Count - 1; i++) hanamaru.FillRectangle(bush, Ryuu.ElementAt(i).X, Ryuu.ElementAt(i).Y, width, width);//竜身的繪製
                hanamaru.FillRectangle(Hid, Ryuu.ElementAt(Ryuu.Count - 1).X, Ryuu.ElementAt(Ryuu.Count - 1).Y, width, width);//竜頭的繪製
                ReadMap(Level);//繪製地圖
            }
        } //End

        public void WidowsAssRnD()
        {
            //隨機改變食物位置
            widow.Score = 100;
            int randomX = 0, randomY = 0;
            do
            {
                randomX = RandomNumberProvidor.Next() % 1003;
                randomY = RandomNumberProvidor.Next() % 612;
                randomX -= (randomX % 17);
                randomY -= (randomY % 17);
            } while (exist[randomY / 17][randomX / 17]);

            for (int i = -1; i < 2; i++)
                for (int j = -1; j < 2; j++) { 
                    try{
                        if (exist[randomY / 17 + i][randomX / 17 + j]) widow.Score += 100;
                    }
                    catch { widow.Score += 100; continue; }
                }

            widow.Location = new Point(randomX, randomY);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;//過場結束
            PauseLock = true;    //竜可以開始動作了
            CanChangeDirection = true;  //可以操縱方向了

            for (int i = 0; i < life; i++) Lifes[i].Visible = true;//根據當前生命值來顯示生命值圖示

            ShowLevel.Visible = true;
            ShowScore.Visible = true;//啟動右側遊戲資訊的顯示

            timer2.Enabled = false;
        }//End
    }
}



class Widow : PictureBox {
    public int value=1;//吃了會成長的長度
    public int speedDelta = 0;//速度的變化
    public int Score = 100;
    public Widow() {
        Height = 17;
        Width = 17;
        BackColor = Color.FromArgb(255, 207, 243, 1);
    }
}
