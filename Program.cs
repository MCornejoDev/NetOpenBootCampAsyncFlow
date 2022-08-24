using ExampleAsyncFlow;
using System.Diagnostics;

//Iniciamos un contador de tiempo SÍNCRONA
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
System.Console.WriteLine("\nBienvenidos a la calculadora de hipotecas Sincrona");

int laboralLife = SyncMortgageCalculator.GetLaboralLife();
System.Console.WriteLine($"Años de vida laboral obtenidos : {laboralLife}");
bool typeContract = SyncMortgageCalculator.IsIndeterminateContract();
System.Console.WriteLine($"Tipo de contrato indefinido: {typeContract}");
int cleanSalary = SyncMortgageCalculator.GetCleanSalary();
System.Console.WriteLine($"Sueldo neto : {cleanSalary} euros");
int monthlyExpenses = SyncMortgageCalculator.GetMonthlyExpenses();
System.Console.WriteLine($"Gastos mensuales : {monthlyExpenses} euros");
var hipotecaConcedida = SyncMortgageCalculator.GetInformationForGiveMortgage(
    laboralLife, typeContract, cleanSalary, monthlyExpenses, requestedAmount: 50000, paymentTime: 30);

var resultado = hipotecaConcedida ? "APROBADA" : "DENEGADA";

System.Console.WriteLine($"\nSu solicitud de hipoteca ha sido : {resultado}");

stopwatch.Stop();

System.Console.WriteLine($"\nLa operacion sincrona ha durado : {stopwatch.Elapsed}");

//Reiniciamos un contador de tiempo - ASÍNCRONA
stopwatch.Restart();
System.Console.WriteLine("\n****************************************************");
System.Console.WriteLine("\nBienvenidos a la calculadora de hipotecas Asincrona");
System.Console.WriteLine("\n****************************************************");

Task<int> laboralLifeAsyncTask = AsyncMortgageCalculator.GetLaboralLife();
Task<bool> typeContractAsyncTask = AsyncMortgageCalculator.IsIndeterminateContract();
Task<int> cleanSalaryAsyncTask = AsyncMortgageCalculator.GetCleanSalary();
Task<int> monthlyExpensesAsyncTask = AsyncMortgageCalculator.GetMonthlyExpenses();

var analisisHipotecasTask = new List<Task>
{
    laboralLifeAsyncTask,
    typeContractAsyncTask,
    cleanSalaryAsyncTask,
    monthlyExpensesAsyncTask
};

while (analisisHipotecasTask.Any())
{
    Task tareaFinalizada = await Task.WhenAny(analisisHipotecasTask);

    if (tareaFinalizada == laboralLifeAsyncTask)
    {
        System.Console.WriteLine($"Años de vida laboral obtenidos : {laboralLifeAsyncTask.Result}");
    }
    else if (tareaFinalizada == typeContractAsyncTask)
    {
        System.Console.WriteLine($"Tipo de contrato indefinido: {typeContractAsyncTask.Result}");
    }
    else if (tareaFinalizada == cleanSalaryAsyncTask)
    {
        System.Console.WriteLine($"Sueldo neto : {cleanSalaryAsyncTask.Result} euros");
    }
    else if (tareaFinalizada == monthlyExpensesAsyncTask)
    {
        System.Console.WriteLine($"Gastos mensuales : {monthlyExpensesAsyncTask.Result} euros");
    }

    analisisHipotecasTask.Remove(tareaFinalizada); // Eliminamos de la lista
}

var hipotecaConcedidaAsyncTasnk = SyncMortgageCalculator.GetInformationForGiveMortgage(
    laboralLifeAsyncTask.Result, typeContractAsyncTask.Result, cleanSalaryAsyncTask.Result, monthlyExpensesAsyncTask.Result, requestedAmount: 50000, paymentTime: 30);

var resultadoAsyncTask = hipotecaConcedidaAsyncTasnk ? "APROBADA" : "DENEGADA";

System.Console.WriteLine($"\nSu solicitud de hipoteca ha sido : {resultadoAsyncTask}");

stopwatch.Stop();

System.Console.WriteLine($"\nLa operacion asincrona ha durado : {stopwatch.Elapsed}");

Console.ReadKey();