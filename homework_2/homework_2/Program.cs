using System;
class Account
{
    public string AccountNumber { get; set; }
    public double Balance { get; set; }

    public Account(string accountNumber, double balance)
    {
        AccountNumber = accountNumber;
        Balance = balance;
    }

    // 存款
    public void Deposit(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("存款金额必须大于零。");
        }
        Balance += amount;
        Console.WriteLine($"账户 {AccountNumber} 存款 {amount} 元。");
    }

    // 取款
    public virtual void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("取款金额必须大于零。");
        }
        if (Balance < amount)
        {
            throw new InvalidOperationException("余额不足。");
        }
        Balance -= amount;
        Console.WriteLine($"账户 {AccountNumber} 取款 {amount} 元。");
    }
}

// 信用账户类
class CreditAccount : Account
{
    public double CreditLimit { get; set; }

    public CreditAccount(string accountNumber, double balance, double creditLimit) : base(accountNumber, balance)
    {
        CreditLimit = creditLimit;
    }

    //重写父类的取款方法
    public override void Withdraw(double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("取款金额必须大于零。");
        }
        if (Balance + CreditLimit < amount)
        {
            throw new InvalidOperationException("信用额度不足，无法取款。");
        }
        Balance -= amount;
        Console.WriteLine($"信用账户 {AccountNumber} 取款 {amount} 元。");
    }
}

//坏钞异常
class BadCashException : Exception
{
    public BadCashException(string message) : base(message)
    {
    }
}

// ATM类
class ATM
{
    // 大笔金额取款事件
    public event Action<Account, double> BigMoneyFetched;

    // 取款
    public void WithdrawMoney(Account account, double amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("取款金额必须大于零。");
        }

        // 模拟坏钞率为30%
        if (new Random().Next(1, 11) <= 3)
        {
            throw new BadCashException("遇到坏的钞票。");
        }

        account.Withdraw(amount);

        // 触发大笔金额取款事件
        if (amount > 10000)
        {
            BigMoneyFetched?.Invoke(account, amount);
        }
    }
}

// 交易类型
enum TransactionType
{
    Deposit,
    Withdraw
}

// 交易日志
interface ITransactionLog
{
    void LogTransaction(string accountNumber, TransactionType transactionType, double amount);
}

// 交易记录
struct TransactionRecord
{
    public string AccountNumber { get; }
    public TransactionType TransactionType { get; }
    public double Amount { get; }
    public DateTime Timestamp { get; }

    public TransactionRecord(string accountNumber, TransactionType transactionType, double amount)
    {
        AccountNumber = accountNumber;
        TransactionType = transactionType;
        Amount = amount;
        Timestamp = DateTime.Now;
    }
}

class Program
{
    static void Main()
    {
        CreditAccount creditAccount = new CreditAccount("123456789", 10000, 5000);

        ATM atm = new ATM();

        atm.BigMoneyFetched += (account, amount) =>
        {
            Console.WriteLine($"警告：账户 {account.AccountNumber} 取款金额超过10000元：{amount} 元");
        };

        // 进行取款操作
        try
        {
            atm.WithdrawMoney(creditAccount, 15000);
        }
        catch (BadCashException ex)
        {
            Console.WriteLine($"发生坏钞异常：{ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"发生异常：{ex.Message}");
        }
    }
}
