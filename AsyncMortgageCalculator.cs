using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleAsyncFlow
{
    public class AsyncMortgageCalculator
    {
        public static async Task<int> GetLaboralLife()
        {
            System.Console.WriteLine("\nObteniendo años de vida laboral.....");
            await Task.Delay(5000);
            return new Random().Next(1, 35);
        }

        public static async Task<bool> IsIndeterminateContract()
        {
            System.Console.WriteLine("\nVerificando tipo de contrato indefinido.....");
            await Task.Delay(5000);
            return (new Random().Next(1, 10) % 2 == 0);
        }

        public static async Task<int> GetCleanSalary()
        {
            System.Console.WriteLine("\nObteniendo sueldo neto.....");
            await Task.Delay(5000);
            return new Random().Next(800, 6000);
        }

        public static async Task<int> GetMonthlyExpenses()
        {
            System.Console.WriteLine("\nObteniendo gastos mensuales.....");
            await Task.Delay(10000);
            return new Random().Next(200, 1000);
        }

        public static bool GetInformationForGiveMortgage(
           int laboralLife,
           bool typeContract,
           int cleanSalary,
           int monthlyExpenses,
           int requestedAmount,
           int paymentTime)
        {
            System.Console.WriteLine("\nAnalizando información para conceder hipoteca");

            if (laboralLife < 2)
            {
                return false;
            }

            // Obtener cuota mensual a pagar
            var cuota = (requestedAmount / paymentTime) / 12;

            if (cuota >= cleanSalary || cuota > (cleanSalary / 2))
            {
                return false;
            }

            // Obtener porcentaje de gastos sobre el sueldo neto del usuario
            var porcentajesGastosSobreSueldo = ((monthlyExpenses * 100) / 100);
            if (porcentajesGastosSobreSueldo > 30)
            {
                return false;
            }

            if ((cuota + monthlyExpenses) >= cleanSalary)
            {
                return false;
            }

            if (!typeContract)
            {
                if ((cuota + monthlyExpenses) > (cleanSalary / 3))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            //Si no entra en ninguna de las condiciones, si que la concedemos
            return true;
        }
    }
}