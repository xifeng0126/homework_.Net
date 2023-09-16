// See https://aka.ms/new-console-template for more information
//输出用户输入数字的所有素数因子
using System;
Console.Write("请输入一个正整数：");
if (int.TryParse(Console.ReadLine(), out int inputNum) && inputNum > 0)
{
    if (inputNum == 1)
    {
        Console.WriteLine("1没有素数因子！");
        return;
    }
    Console.WriteLine($"数字 {inputNum} 的素数因子为：");
   
    for (int i = 2; i <= inputNum; i++)
    {
        if (inputNum % i == 0 && IsPrime(i))
        {
            Console.WriteLine(i);
            while (inputNum % i == 0)
            {
                inputNum /= i;
            }
        }
    }
}
else
{
    Console.WriteLine("输入无效，请输入一个正整数。");
}

 bool IsPrime(int num)
{
    if (num <= 1)
        return false;

    for (int i = 2; i * i <= num; i++)
    {
        if (num % i == 0)
            return false;
    }

    return true;
}


