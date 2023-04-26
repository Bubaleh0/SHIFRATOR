using System;
using System.Text;

static class TestConsole
{
    static void Main(string[] args)
    {
        string alp = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        alp += alp.ToLower();
        alp += " .:!?,";

        PTable pTable = new PTable(9, 8, alp);
        Console.WriteLine("Таблица:\r\n" + pTable);

        Console.WriteLine("Введите слово для шифровки");
        string text = Console.ReadLine();
        Console.WriteLine("Закодированное слово:" + pTable.encode(text));

        try
        {
            Console.WriteLine("Введите слово для дешифровки");
            text = Console.ReadLine();
            Console.WriteLine("Дешифрованное слово: " + pTable.decode(text));
        }
        catch
        {
            Console.WriteLine("Попались неизвестные символы");
        }

        Console.ReadLine();
    }

}

class PTable
{
    string chars;

    public PTable(int n, int m, string chars)
    {
        N = n; M = m;
        this.chars = chars;
    }

    public int N { get; private set; }
    public int M { get; private set; }

    public char this[int r, int c]
    {
        get { return chars[r * M + c]; }
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int r = 0; r < N; r++)
        {
            for (int c = 0; c < M; c++)
            {
                sb.Append(this[r, c]);
                sb.Append(' ');
            }
            if (r + 1 != N) sb.AppendLine();
        }

        return sb.ToString();
    }

    public string encode(string text)
    {
        int ind;
        StringBuilder sb = new StringBuilder();
        foreach (char c in text)
        {
            if ((ind = chars.IndexOf(c)) != -1)
            {
                sb.Append((int)(ind / M) + 1);
                sb.Append(ind % M + 1);
                sb.Append(' ');
            }
        }
        return sb.ToString();
    }

    public string decode(string text)
    {
        StringBuilder sb = new StringBuilder();
        string[] coords = text.Split(' ');
        foreach (string s in coords)
        {
            if (s != "")
            {
                int r, c;
                if (int.TryParse(s[0].ToString(), out r) && int.TryParse(s[1].ToString(), out c))
                {
                    sb.Append(this[r - 1, c - 1]);
                }
            }
        }
        return sb.ToString();
    }
}