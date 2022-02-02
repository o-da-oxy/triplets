using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace triplets
{
    class Program
    {
        static void Main(string[] args)
        {
            //путь к текстовому файлу
            string path = "C:\\Users\\demsk\\Desktop\\РАБОТА\\triplets\\triplets\\triplets\\text.txt";
            string line = File.ReadAllText(path).Replace('\n', ' ');
            string text = Regex.Replace(line, "[,.():]", "");
            string[] words = text.Split(" ");

            //list, элементы - слова текста без знаков препинания
            List<string> list = new List<string>();
            for (int i = 0; i < words.Length; i++)
            {
                list.Add(words[i]);
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            
            Triplets(list);
            
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        public static void Triplets(List<string> list)
        {

            //разобьем words на всевозможные комбинации из 3х идущих подряд букв
            var threeChar = new List<string>();
            string word;
            for (int i = 0; i < list.Count; i++)
            {
                word = list[i];
                for (int j = 0; j < word.Length - 2; j++)
                {
                    threeChar.Add(word.Substring(j, 3));
                }
            }

            //отфильтруем
            var groupBy = threeChar.GroupBy(x => x);
            
            //сортируем триплеты в порядке возрастания count, берем первые 10
            var triplets = groupBy.OrderByDescending(g =>
                g.Count()).Take(10).Select(g => $"{g.Key} - {g.Count()} повторений");

            //вывод триплетов в отдельном потоке
            //9 мс - оптимальный вариант
            new Thread(() => { Console.WriteLine(string.Join(Environment.NewLine, triplets)); }).Start();
            
            //13 мс
            // await Task.Run(() =>
            // {
            //     Console.WriteLine(string.Join(Environment.NewLine, triplets));
            // });
            
            //16 мс
            //Console.WriteLine(string.Join(Environment.NewLine, triplets));

        }
    }
}