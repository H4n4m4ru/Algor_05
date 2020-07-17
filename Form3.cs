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

namespace Algor_05
{
    public partial class Form3 : Form
    {
        string PlayerName="";
        int mod = 0;
        public int Scott = 0;
        const int maxLength = 10;
        bool haveRegi = false;
        
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e){
            panel1.Visible = false;
            PlayerName = "";
            label1.Text = PlayerName;
            mod = 0;
            haveRegi = false;
            label1.UseMnemonic = false;
            label2.Text = "Score : " + Scott;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e){
            mod = (mod + 1) % 2;
            if (mod == 1) label1.Text = PlayerName;
            else if (mod == 0 && PlayerName.Length < maxLength) label1.Text = PlayerName + "_";//做出一閃一閃的效果
        }
        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            string buffer="";
            //=============================== 倒退鍵的處理 =======================================
            if (e.KeyCode == Keys.Back ) {
                if (PlayerName.Length != 0) PlayerName = PlayerName.Remove(PlayerName.Length - 1);
                return; 
            }
            //================================ 輸入的處理 ========================================
            if (PlayerName.Length < maxLength){ //最多 maxLength 個字,否則不給加
                if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
                {
                    //範圍:字母
                    if (e.Shift) buffer = e.KeyCode.ToString().ToLower();//小寫
                    else buffer = e.KeyCode.ToString();//大寫
                }//字母EnD
                else if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                {
                    //範圍:數字鍵(NumPad)
                    buffer = (e.KeyValue - 96).ToString();
                }//數字鍵(NumPad)EnD
                else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                {
                    //範圍:數字鍵(D)
                    if (!e.Shift) buffer = (e.KeyValue - 48).ToString();
                    else
                    {
                        //shift+上排數字鍵
                        switch (e.KeyCode)
                        {
                            case Keys.D0: buffer = ")"; break;
                            case Keys.D1: buffer = "!"; break;
                            case Keys.D2: buffer = "@"; break;
                            case Keys.D3: buffer = "#"; break;
                            case Keys.D4: buffer = "$"; break;
                            case Keys.D5: buffer = "%"; break;
                            case Keys.D6: buffer = "^"; break;
                            case Keys.D7: buffer = "&"; break;
                            case Keys.D8: buffer = "*"; break;
                            case Keys.D9: buffer = "("; break;
                        }//switchEnD
                    }
                }//數字鍵(D)EnD
                else if (e.KeyCode == Keys.Space) buffer = "_";
                else
                {
                    //範圍:其他
                    switch (e.KeyValue)
                    {
                        case 192:
                            if (!e.Shift) buffer = "`";
                            else buffer = "~"; break;
                        case 189:
                            if (!e.Shift) buffer = "-";
                            else buffer = "_"; break;
                        case 187:
                            if (!e.Shift) buffer = "=";
                            else buffer = "+"; break;
                        case 219:
                            if (!e.Shift) buffer = "[";
                            else buffer = "{"; break;
                        case 221:
                            if (!e.Shift) buffer = "]";
                            else buffer = "}"; break;
                        case 220:
                            if (!e.Shift) buffer = "\\";
                            else buffer = "|"; break;
                        case 186:
                            if (!e.Shift) buffer = ";";
                            else buffer = ":"; break;
                        case 222:
                            if (!e.Shift) buffer = "'";
                            else buffer = "\""; break;
                        case 188:
                            if (!e.Shift) buffer = ",";
                            else buffer = "<"; break;
                        case 190:
                            if (!e.Shift) buffer = ".";
                            else buffer = ">"; break;
                        case 191:
                            if (!e.Shift) buffer = "/";
                            else buffer = "?"; break;
                        case 111: buffer = "/"; break;
                        case 106: buffer = "*"; break;
                        case 109: buffer = "-"; break;
                        case 107: buffer = "+"; break;
                        case 110: buffer = "."; break;
                    }//switchEnD
                }
                PlayerName += buffer;
            }//輸入處理End
        }

         private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            //================================  Enter ============================================
            if (e.KeyCode == Keys.Enter && !haveRegi)
            {
                timer1.Enabled = false;//停止背景的timer1運行
                label3.Text = "";
                label5.Text = "";
                label6.Text = "";//清空顯示內容

                int rows = 0;
                StreamReader CulculateRows = new StreamReader("Score.txt");
                //計算這次的序號要從多少開始
                while (!CulculateRows.EndOfStream){
                    rows++;
                    CulculateRows.ReadLine();
                }
                CulculateRows.Close();
                //登錄這次成績
                StreamWriter WriteScore = new StreamWriter("Score.txt",true);
                WriteScore.Write((rows+1).ToString()+"\t"+PlayerName+"\t"+Scott.ToString()+"\r\n");
                WriteScore.Close();
                
                //============<<<<<<<<<<============== 顯示成績開始 =============>>>>>>>>>>==============
                string[][] Scores = null;    //儲存資料用二維陣列
                char[] spl_signs = { '\t' };    //分割符
                StreamReader ReadScore = new StreamReader("Score.txt");
                Queue<int> Index_of_Top5 = new Queue<int>();

                Scores = new string[rows + 1][];
                for (int i = 0; i < rows + 1; i++)
                    Scores[i] = ReadScore.ReadLine().Split(spl_signs);

                //資料依索引順序分別為 {序號,姓名,分數}
                //開始找前五高分的
                while ((Index_of_Top5.Count < rows+1) && (Index_of_Top5.Count < 5)){
                    //已選出的成績在五個以內並且不超過資料的總數(也許資料不滿五個)
                    //找最大的
                    int StartIndex = 0;
                    while (Index_of_Top5.Contains(StartIndex)) { StartIndex++; }
                    int maxIndex = StartIndex;//找出沒有被選到且最前面的索引做為比較的起點

                    for (int j = StartIndex + 1; j < rows + 1; j++)
                        if (Int32.Parse(Scores[j][2]) > Int32.Parse(Scores[maxIndex][2]) && !Index_of_Top5.Contains(j)) maxIndex = j;
                    Index_of_Top5.Enqueue(maxIndex);
                }//while end,已找出前5名

                //開始顯示成績

                int bound = Index_of_Top5.Count;//取得前n名的數量,dequeue的過程中count屬性會變動所以要先提出來

                for (int k = 0; k < bound; k++){
                    int index = Index_of_Top5.Dequeue();
                    label3.Text += "#" + Scores[index][0] + "\r\n";//序號
                    label5.Text += Scores[index][1] + "\r\n";//姓名
                    label6.Text += Scores[index][2] + "\r\n";//分數
                }//for end
                //============<<<<<<<<<<============== 顯示成績結束 =============>>>>>>>>>>==============
                ReadScore.Close();
                
                haveRegi = true;             
                panel1.Visible = true;
                this.button1.Focus();
            }
        }
         private void button1_Click(object sender, EventArgs e)
         {
             DialogResult = DialogResult.OK;
         }
    }
}
