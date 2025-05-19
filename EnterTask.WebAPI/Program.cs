namespace EnterTask.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddWebServices(builder.Configuration);

            var app = builder.Build();

            app.ConfigureApp();

            app.MapControllers();

            app.Run();
        }
    }
}
