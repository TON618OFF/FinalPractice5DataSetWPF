using JsonCreator.Model;
using Newtonsoft.Json;

Console.WriteLine("Введите сколько элементов вы хотите создать");
int num = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("--------------------------------");

List<dynamic> dynamics = new List<dynamic>();
for (int i = 0; i < num; i++)
{
    Type a = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes()).Where(p => typeof(IToFindModel).IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface).First();
    var someShit = Activator.CreateInstance(a);
    foreach (var item in a.GetProperties())
    {
        Console.WriteLine("Введите значение для свойства " + item.Name);
        item.SetValue(someShit, Convert.ChangeType(Console.ReadLine(), item.PropertyType));
    }
    dynamics.Add(someShit);
}
Console.WriteLine("-------------");
Console.WriteLine("Введите название JSON файла, куда вы хотите сохранить. Файл сохранится на рабочий стол");

string json = JsonConvert.SerializeObject(dynamics);
File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\{Console.ReadLine()}.json", json) ;

