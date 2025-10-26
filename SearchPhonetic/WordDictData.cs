using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchPhonetic
{
    internal class WordDictData
    {
        public WordDictData()
        {
        }
        //word,phonetic,definition,translation,pos,collins,oxford,tag,bnc,frq,exchange,detail,audio

        /// <summary>
        /// 单词本身
        /// </summary>
        public string word { get; set; } = "";

        /// <summary>
        /// 音标，以英语英标为主
        /// </summary>
        public string phonetic { get; set; } = "";

        /// <summary>
        /// 单词释义（英文），每行一个释义
        /// </summary>
        public string definition { get; set; } = "";

        /// <summary>
        /// 单词释义（中文），每行一个释义
        /// </summary>
        public string translation { get; set; } = "";

        /// <summary>
        /// 词语位置，用 "/" 分割不同位置
        /// </summary>
        public string pos { get; set; } = "";

        public string collins { get; set; } = "";

        public string oxford { get; set; } = "";

        public string tag { get; set; } = "";

        public string bnc { get; set; } = "";

        public string frq { get; set; } = "";

        public override string ToString()
        {
            return this.word;
        }

        public void CheckData()
        {
            this.Tags = tag.Split(' ').ToHashSet();
            this.IsOxford = this.oxford.Equals("1");
            if (this.IsOxford)
            {
                this.Tags.Add("oxford");
            }
            if (int.TryParse(this.bnc, out var n))
            {
                this.OrderBnc = n;
            }
            if (int.TryParse(this.frq, out n))
            {
                this.OrderFrq = n;
            }
            if (int.TryParse(this.collins, out n))
            {
                this.OrderCollins = n;
            }
        }

        /// <summary>
        /// 字符串标签：zk/中考，gk/高考，cet4/四级 等等标签，空格分割
        /// </summary>

        [Ignore()]
        public HashSet<string> Tags { get; set; } = [];

        /// <summary>
        /// 是否是牛津三千核心词汇
        /// </summary>
        [Ignore()]
        public bool IsOxford { get; set; } = false;

        /// <summary>
        /// 英国国家语料库词频顺序
        /// </summary>
        [Ignore()]
        public int OrderBnc { get; set; } = 0;

        /// <summary>
        ///  当代语料库词频顺序
        /// </summary>
        [Ignore()]
        public int OrderFrq { get; set; } = 0;

        /// <summary>
        /// 柯林斯星级
        /// </summary>
        [Ignore()]
        public int OrderCollins { get; set; } = 0;
    }
}
