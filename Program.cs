using cs_oppgave_05.tests.MYSQLconnection;

//      Task: RestAPI with Controllers (MVC)
//
//      1.0 Lag en ny mappe Model
//      1.1 Lag en datamodell klasse
//      1.2 Valgfri (forsøk å legge til støtte for SQL i API-et)
//
//      2.0 Lag en controller som implementerer modellen deres
//      2.1 Lag et GET-endepunkt
//      2.2 Lag et POST-endepunkt

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        MysqlConnectionTester.TestConnection();
    }
}
