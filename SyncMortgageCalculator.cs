using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleAsyncFlow
{
    public class SyncMortgageCalculator
    {
        public static int GetLaboralLife()
        {
            System.Console.WriteLine("\nObteniendo años de vida laboral.....");
            Task.Delay(5000).Wait(); //Esperamos 5 seg
            return new Random().Next(1, 35);
        }

        public static bool IsIndeterminateContract()
        {
            System.Console.WriteLine("\nVerificando tipo de contrato indefinido.....");
            Task.Delay(5000).Wait(); //Esperamos 5 seg
            return (new Random().Next(1, 10) % 2 == 0); // Devolvemos true o false si el valor aleatorio es par/impar
        }

        public static int GetCleanSalary()
        {
            System.Console.WriteLine("\nObteniendo sueldo neto.....");
            Task.Delay(5000).Wait(); //Esperamos 5 seg
            return new Random().Next(800, 6000); // Devolvemos un valor aleatorio entre 800 y 6000
        }

        public static int GetMonthlyExpenses()
        {
            System.Console.WriteLine("\nObteniendo gastos mensuales.....");
            Task.Delay(5000).Wait(); //Esperamos 5 seg
            return new Random().Next(200, 1000); // Devolvemos un valor aleatorio entre 200 y 1000
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